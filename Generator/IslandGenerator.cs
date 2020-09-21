using System.Collections.Generic;

namespace Swindler.IslandGenerator.Generator
{
	public class IslandGenerator
	{

		private int width;
		private int height;
		private int fillPercent;
		private int smoothAmount;
		private int minIslandSize;
		private int borderWidth;
		private System.Random rd;
		private PropsSettings[] propsSettings;

		private int[,] map;

		public IslandGenerator(int width, int height, int fillPercent, int smoothAmount, int minIslandSize, int borderWidth, PropsSettings[] propsSettings, System.Random rd)
		{
			this.width = width;
			this.height = height;
			this.fillPercent = fillPercent;
			this.rd = rd;
			this.smoothAmount = smoothAmount;
			this.minIslandSize = minIslandSize;
			this.borderWidth = borderWidth;
			this.propsSettings = propsSettings;
			map = new int[width, height];
		}

		/// <summary>
		/// Generate a new island from current settings
		/// </summary>
		/// <returns></returns>
		public IslandData Generate(Coord position)
		{
			map = new int[width, height];

			RandomFill();

			for (int i = 0; i < smoothAmount; i++)
				Smooth();

			//Works for now, but maybe consider avoiding looping throught the map 12 times ? (Trail: see Sebastian Lague cave generation system, where he detect the edges and only loop throught edges)
			for (int i = 0; i < 13; i++)
				RemoveSingleTiles();

			CleanRegions();

			IslandData data = new IslandData(position);
			data.AddLayer("island", map);
			data.AddLayer("props", new PropsGenerator(map, rd, propsSettings).Generate());

			return data;
		}

		/// <summary>
		/// Randomly fill the desired percentage of the map with island tile
		/// </summary>
		public void RandomFill()
		{
			int islandsCount = (int)System.Math.Ceiling(map.Length * (fillPercent / 100f));

			for (int i = 0; i < islandsCount; i++)
			{
				Coord coords = FindNextRandomTile(0);
				map[coords.X, coords.Y] = 1;
			}

		}

		/// <summary>
		/// Apply a smoothing pass on the map, removing too isolated tiles (Cellular Automata)
		/// </summary>
		public void Smooth()
		{
			for (int x = 0; x < width; x++)
				for (int y = 0; y < height; y++)
				{
					int neightbours = CountNeighbourIslands(x, y);
					if (neightbours > 4)
						map[x, y] = 1;
					else if (neightbours < 4)
						map[x, y] = 0;
				}

		}

		/// <summary>
		/// Remove all regions that are under minIslandSize
		/// </summary>
		public void CleanRegions()
		{
			List<List<Coord>> regions = DetectRegions(1);

			foreach (List<Coord> region in regions)
				if (region.Count < minIslandSize)
					foreach (Coord c in region)
						map[c.X, c.Y] = 0;

		}

		/// <summary>
		/// Remove protruding tiles of size 1 (less than or equals to 3 neighbours)
		/// </summary>
		private void RemoveSingleTiles()
		{
			//TODO: Replace code to pattern detection (like detct only 3 tiles in one side and 0 in the others, 3 in one side and 1 in the oposite, see generated islands)
			for (int x = 0; x < width; x++)
				for (int y = 0; y < height; y++)
					if (CountNeighbourIslands(x, y) <= 4)
						map[x, y] = 0;

		}

		/// <summary>
		/// Detect all the regions made of a given tile
		/// </summary>
		/// <param name="tileType">Id of the tile to use</param>
		/// <returns></returns>
		List<List<Coord>> DetectRegions(int tileType)
		{
			List<List<Coord>> regions = new List<List<Coord>>();
			int[,] mapFlags = new int[width, height];

			for (int x = 0; x < width; x++)
				for (int y = 0; y < height; y++)
					if (mapFlags[x, y] == 0 && map[x, y] == tileType)
					{
						List<Coord> region = GetRegion(x, y);
						regions.Add(region);

						foreach (Coord c in region)
							mapFlags[c.X, c.Y] = 1;
					}

			return regions;
		}

		/// <summary>
		/// Get all tiles composing a single region (tile type is given by the initial tile)
		/// </summary>
		/// <param name="startX">X axis starting point</param>
		/// <param name="startY">Y axis starting point</param>
		/// <returns></returns>
		List<Coord> GetRegion(int startX, int startY)
		{
			List<Coord> tiles = new List<Coord>();
			int[,] mapFlags = new int[width, height];   //Has tile been looked (1 => true)

			int tileType = map[startX, startY];

			Queue<Coord> queue = new Queue<Coord>();
			queue.Enqueue(new Coord(startX, startY));

			mapFlags[startX, startY] = 1;

			while (queue.Count > 0)
			{
				Coord tile = queue.Dequeue();

				tiles.Add(tile);

				for (int x = tile.X - 1; x <= tile.X + 1; x++)
					for (int y = tile.Y - 1; y <= tile.Y + 1; y++)
						if (IsInBounds(x, y) && (y == tile.Y || x == tile.X))
							if (mapFlags[x, y] == 0 && map[x, y] == tileType)
							{
								mapFlags[x, y] = 1;
								queue.Enqueue(new Coord(x, y));
							}

			}

			return tiles;
		}

		/// <summary>
		/// Search the current area for a new random tile matching a type
		/// </summary>
		/// <param name="tileType">Type of the tile</param>
		/// <returns></returns>
		private Coord FindNextRandomTile(int tileType)
		{
			int x = rd.Next(borderWidth, width - borderWidth);
			int y = rd.Next(borderWidth, height - borderWidth);
			while (map[x, y] != tileType)
			{
				x = rd.Next(borderWidth, width - borderWidth);
				y = rd.Next(borderWidth, height - borderWidth);
			}

			return new Coord(x, y);
		}

		/// <summary>
		/// Count all neigbouring island tiles of a given tile
		/// </summary>
		/// <param name="gridX">Tile X coordinate</param>
		/// <param name="gridY">Tile Y coordinate</param>
		/// <returns></returns>
		private int CountNeighbourIslands(int gridX, int gridY)
		{
			int neightbours = 0;

			for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
				for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
					if (IsInBounds(neighbourX, neighbourY))
						neightbours += map[neighbourX, neighbourY] == 1 ? 1 : 0;    //In case of future addition of tiles

			return neightbours;
		}

		/// <summary>
		/// Check if a given coordinate is in island bounds
		/// </summary>
		/// <param name="x">X coordinate</param>
		/// <param name="y">Y coordinate</param>
		/// <returns></returns>
		private bool IsInBounds(int x, int y)
		{
			return x >= 0 && x < width && y >= 0 && y < height;
		}

		/// <summary>
		/// Check if current tile is an island edge
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		private bool IsEdgeTile(int x, int y)
		{
			int neighbours = CountNeighbourIslands(x, y);
			return neighbours != 9;
		}

	}

}

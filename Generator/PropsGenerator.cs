using System;
using System.Collections.Generic;
using Swindler.IslandGenerator.Generator.Utils;

namespace Swindler.IslandGenerator.Generator
{
	class PropsGenerator
	{

		private int[,] island;
		private int[,] props;

		private int width;
		private int height;

		private Random random;

		private PropsSettings[] propsSettings;

		public PropsGenerator(int[,] island, Random random, PropsSettings[] propsSettings)
		{
			this.propsSettings = propsSettings;
			this.island = island;
			this.random = random;

			width = island.GetLength(0);
			height = island.GetLength(1);
			props = new int[width, height];
		}

		/// <summary>
		/// Place every defined props (from PropsSettings) on this island
		/// </summary>
		/// <returns></returns>
		public int[,] Generate()
		{
			foreach (PropsSettings settings in propsSettings)
				PlaceTile(settings.GetTiles(random), settings.MinProps, settings.MaxProps, settings.FloorType);

			return props;
		}

		/// <summary>
		/// Generate props from settings
		/// </summary>
		/// <param name="tiles">Tiles that compose this props type</param>
		/// <param name="min">Minimal amount of this props</param>
		/// <param name="max">Maximal amount of this props</param>
		/// <param name="floorType">Tile type of the tile that the props will seat on</param>
		private void PlaceTile(WeightedRandomBag<int> tiles, int min, int max, int floorType = 1)
		{
			//TODO: use poisson disc sampling for position (see Utils)
			int count = random.Next(min, max);
			for (int i = 0; i < count; i++)
			{
				//TODO: Avoid having objects too near of each others
				Coord c = FindNextTileOfType(floorType);
				props[c.X, c.Y] = tiles.GetRandom();
			}
		}

		/// <summary>
		/// Return the next tile of a certain type
		/// </summary>
		/// <param name="tileType">Tile id</param>
		/// <returns></returns>
		private Coord FindNextTileOfType(int tileType)
		{
			int x = random.Next(0, width);
			int y = random.Next(0, height);
			while (island[x, y] != tileType)
			{
				x = random.Next(0, width);
				y = random.Next(0, height);
			}

			return new Coord(x, y);
		}

		/// <summary>
		/// Check if a given coordinate is in map bounds
		/// </summary>
		/// <param name="x">X coordinate</param>
		/// <param name="y">Y coordinate</param>
		/// <returns></returns>
		private bool IsInBounds(int x, int y)
		{
			return x >= 0 && x < width && y >= 0 && y < height;
		}

	}

	public class PropsSettings
	{
		/// <summary>
		/// Tiles composing this props and their corresponding weight
		/// </summary>
		public Dictionary<int, int> Tiles { get; set; }
		/// <summary>
		/// Minimal number of this props
		/// </summary>
		public int MinProps { get; set; }
		/// <summary>
		/// Maximal number of this props
		/// </summary>
		public int MaxProps { get; set; }
		/// <summary>
		/// Type of floor for these props
		/// </summary>
		public int FloorType = 1;
		public string Seed { get; set; }

		public PropsSettings()
		{
			Tiles = new Dictionary<int, int>();
		}

		/// <summary>
		/// Get a WeightedRandomBag containing the defined props
		/// </summary>
		/// <param name="rd">Random number generator that will be used by this WeightedRandomBag</param>
		/// <returns></returns>
		public WeightedRandomBag<int> GetTiles(Random rd)
		{
			WeightedRandomBag<int> bag = new WeightedRandomBag<int>(rd);

			foreach (int k in Tiles.Keys)
				bag.AddEntry(k, Tiles[k]);

			return bag;
		}

	}

}

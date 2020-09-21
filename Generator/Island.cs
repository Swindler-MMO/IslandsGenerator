using System.Collections.Generic;

namespace Swindler.IslandGenerator.Generator
{
	public class Island
	{
		/// <summary>
		/// Island top left corner
		/// </summary>
		public Coord TLCorner { get; }
		/// <summary>
		/// Island bottom right corner
		/// </summary>
		public Coord BRCorner { get; }
		/// <summary>
		/// Island center
		/// </summary>
		public Coord Center { get; }
		/// <summary>
		/// Width of this island
		/// </summary>
		public int Width { get; }
		/// <summary>
		/// Height of this island
		/// </summary>
		public int Height { get; }

		public List<IslandLayer> Layers { get; }

		public Island(Coord center, IslandData data)
		{
			Layers = data.GetLayers();
			Width = Layers[0].GetLength(0);
			Height = Layers[0].GetLength(1);

			BRCorner = new Coord(center.X + Width, center.Y + Height);
			TLCorner = center;
			Center = new Coord(center.X + Width / 2, center.Y + Height / 2);
		}

	}

	public class IslandData
	{

		public Coord Position { get; }

		private readonly List<IslandLayer> layers = new List<IslandLayer>();

		public IslandData(Coord position)
		{
			Position = position;
		}

		public int GetLength(int dimension)
		{
			return layers[0].Data.GetLength(dimension);
		}

		public void AddLayer(string name, int[,] data)
		{
			layers.Add(new IslandLayer(name, data));
		}

		public List<IslandLayer> GetLayers()
		{
			return layers;
		}

	}
}

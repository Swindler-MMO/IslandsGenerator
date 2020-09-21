namespace Swindler.IslandGenerator.Generator
{
	public class IslandLayer
	{

		public string Name { get; }
		public int[,] Data { get; }

		public IslandLayer(string name, int[,] data)
		{
			Name = name;
			Data = data;
		}

		public int GetLength(int dimension)
		{
			return Data.GetLength(dimension);
		}

	}
}

namespace Swindler.IslandGenerator.Generator
{
	public class Coord
	{
		public int X { get; set; }
		public int Y { get; set; }

		public Coord()
		{
			X = 0;
			Y = 0;
		}

		public Coord(int x, int y)
		{
			X = x;
			Y = y;
		}

		public float Distance(Coord b)
		{
			float x = X - b.X;
			float y = Y - b.Y;
			return (float)System.Math.Sqrt((x * x) + (y * y));
		}

		public static Coord operator +(Coord a, Coord b)
		{
			return new Coord(a.X + b.X, a.Y + b.Y);
		}

		public static Coord operator +(Coord a, int i)
		{
			return new Coord(a.X + i, a.Y + i);
		}

		public static Coord operator -(Coord a, int i)
		{
			return new Coord(a.X - i, a.Y - i);
		}

		public static Coord operator /(Coord a, Coord b)
		{
			return new Coord(a.X / b.X, a.Y / b.Y);
		}

		public static Coord operator /(Coord a, int i)
		{
			return new Coord(a.X / i, a.Y / i);
		}

		public override string ToString() {
			return "(" + X + ", " + Y + ")";
		}

	}
}

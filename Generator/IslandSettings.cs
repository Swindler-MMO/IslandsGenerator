using Swindler.IslandGenerator.Generator.Utils;

namespace Swindler.IslandGenerator.Generator
{
	public class IslandSettings
	{
		/// <summary>
		/// Width of an island
		/// </summary>
		public int Width { get; set; }
		/// <summary>
		/// Height of an island
		/// </summary>
		public int Height { get; set; }
		/// <summary>
		/// Amount of island tiles before smoothing iterations
		/// </summary>
		public int FillPercent { get; set; }
		/// <summary>
		/// Number of smoothing pass applied
		/// </summary>
		public int SmoothAmount		= 5;
		/// <summary>
		/// Minimal amount of tiles that a region can have, if not region is removed
		/// </summary>
		public int MinIslandSize	= 6;
		/// <summary>
		/// Number of tiles that will be empty around the edges
		/// </summary>
		public int BorderWidth		= 0;
		public PropsSettings[] PropsSettings { get; set; }

		public IslandGenerator GetGenerator(string seed, int x, int y)
		{
			return new IslandGenerator(
				Width,
				Height,
				FillPercent,
				SmoothAmount,
				MinIslandSize,
				BorderWidth,
				PropsSettings,
				new System.Random($"{seed}-{x}-{y}".GetDeterministicHashCode())
			);
		}
	}
}

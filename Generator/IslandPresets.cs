namespace Swindler.IslandGenerator.Generator
{
	public class IslandPresets
	{

		public static IslandSettings SMALL = new IslandSettings
		{
			Width = 32,
			Height = 32,
			FillPercent = 46,
			MinIslandSize = 20,
			SmoothAmount = 2,
			BorderWidth = 1,
			PropsSettings = new PropsSettings[]
			{
				new PropsSettings
				{
					MinProps = 10,
					MaxProps = 22,
					Tiles = new System.Collections.Generic.Dictionary<int, int>
					{
						{8, 2 },
						{9, 1 },
						{10, 3 }
					}
				},
				new PropsSettings
				{
					MinProps = 12,
					MaxProps = 25,
					Tiles = new System.Collections.Generic.Dictionary<int, int>
					{
						{3, 2 },
						{4, 4 },
						{5, 4 },
	  					{7, 2 },
						{8, 4 },
						{6, 4 }
					}
				},
				new PropsSettings
				{
					MinProps = 20,
					MaxProps = 30,
					Tiles = new System.Collections.Generic.Dictionary<int, int>
					{
						{11, 9 },
						{12, 1 }
					}
				}
			}
		};
		public static IslandSettings NORMAL = new IslandSettings
		{
			Width = 40,
			Height = 40,
			FillPercent = 46,
			SmoothAmount = 4,
			MinIslandSize = 14,
			PropsSettings = new PropsSettings[]
			{
				new PropsSettings
				{
					MinProps = 10,
					MaxProps = 22,
					Tiles = new System.Collections.Generic.Dictionary<int, int>
					{
						{8, 2 },
						{9, 1 },
						{10, 3 }
					}
				},
				new PropsSettings
				{
					MinProps = 12,
					MaxProps = 25,
					Tiles = new System.Collections.Generic.Dictionary<int, int>
					{
						{3, 2 },
						{4, 4 },
						{5, 4 },
	  					{7, 2 },
						{8, 4 },
						{6, 4 }
					}
				},
				new PropsSettings
				{
					MinProps = 20,
					MaxProps = 30,
					Tiles = new System.Collections.Generic.Dictionary<int, int>
					{
						{11, 9 },
						{12, 1 }
					}
				}
			}
		};
		public static IslandSettings BIG = new IslandSettings
		{
			Width = 45,
			Height = 45,
			FillPercent = 46,
			MinIslandSize = 13,
			SmoothAmount = 3,
			PropsSettings = new PropsSettings[]
			{
				new PropsSettings
				{
					MinProps = 10,
					MaxProps = 22,
					Tiles = new System.Collections.Generic.Dictionary<int, int>
					{
						{8, 2 },
						{9, 1 },
						{10, 3 }
					}
				},
				new PropsSettings
				{
					MinProps = 12,
					MaxProps = 25,
					Tiles = new System.Collections.Generic.Dictionary<int, int>
					{
						{3, 2 },
						{4, 4 },
						{5, 4 },
	  					{7, 2 },
						{8, 4 },
						{6, 4 }
					}
				},
				new PropsSettings
				{
					MinProps = 20,
					MaxProps = 30,
					Tiles = new System.Collections.Generic.Dictionary<int, int>
					{
						{11, 9 },
						{12, 1 }
					}
				}
			}
		};

	}
}

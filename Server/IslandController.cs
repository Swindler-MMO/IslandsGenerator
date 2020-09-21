using System.Collections.Generic;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using EmbedIO;
using Swindler.IslandGenerator.Generator;

namespace Swindler.IslandGenerator.Server
{
	public class IslandController : WebApiController
	{
		private readonly List<Coord> islands = Utils.LoadJson <List<Coord>>("./islands.json");
		
		[Route(HttpVerbs.Get, "/")]
		public List<Coord> NearestIslands()
		{
			return islands;
		} 
		
		[Route(HttpVerbs.Get, "/{x?}/{y?}")]
		public Island GetIsland(int x, int y)
		{
			IslandData data = IslandPresets.NORMAL.GetGenerator(Config.SEED, x, y).Generate(new Coord(x, y));
			return new Island(new Coord(x, y), data);
		}

	}
}
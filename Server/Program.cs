using EmbedIO;
using EmbedIO.WebApi;
using Swan.Logging;
using System;
using Sentry;

namespace Swindler.IslandGenerator.Server
{
	class Program
	{

		private const string WEBSERVER_URL = "http://+:3000/";

		static void Main(string[] args)
		{
			SentrySdk.Init("https://1eed95455e494f1fae05d6c5612e82d7@o453586.ingest.sentry.io/5442502");
			CreateServer().RunAsync();
			Console.ReadKey(true);
		}

		private static WebServer CreateServer()
		{
			WebServer server = new WebServer(o => o.WithUrlPrefix(WEBSERVER_URL).WithMode(HttpListenerMode.EmbedIO));

			server.WithWebApi("/islands", m => m.WithController<IslandController>());

			server.StateChanged += (s, e) => $"WebServer New State - {e.NewState}".Info();

			return server;
		}

	}
}

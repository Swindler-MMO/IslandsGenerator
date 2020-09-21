using System;
using System.IO;
using Newtonsoft.Json;
using Swan.Logging;

namespace Swindler.IslandGenerator.Server
{
	public class Utils
	{

		public static T LoadJson<T>(string path)
		{
			string text = File.ReadAllText(path);
			return JsonConvert.DeserializeObject<T>(text);
		}
		
	}
}
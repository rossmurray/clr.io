using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace clr.io
{
	public class ConfigurationProvider
	{
		private Configuration config { get; set; }

		public Configuration GetConfig()
		{
			if (this.config == null)
			{
				try
				{
					this.config = LoadConfig();
				}
				catch(Exception ex)
				{
					Console.Error.WriteLine("Can't load config: " + ex);
					this.config = new Configuration();
				}
			}
			return this.config;
		}

		private Configuration LoadConfig()
		{
			var location = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "config.json");
			var json = File.ReadAllText(location);
			return JsonConvert.DeserializeObject<Configuration>(json);
		}
	}
}

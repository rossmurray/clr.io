using Nancy.Hosting.Self;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace clr.io
{
	class Program
	{
		static void Main(string[] args)
		{
			var uri = "http://localhost:8989";
			Console.WriteLine(uri);
			// initialize an instance of NancyHost (found in the Nancy.Hosting.Self package)
			using (var host = new NancyHost(new Uri(uri)))
			{
				host.Start();  // start hosting

				//Under mono if you deamonize a process a Console.ReadLine with cause an EOF 
				//so we need to block another way
				if (args.Any(s => s.Equals("-d", StringComparison.CurrentCultureIgnoreCase)))
				{
					Thread.Sleep(Timeout.Infinite);
				}
				else
				{
					Console.ReadKey();
				}

				host.Stop();  // stop hosting
			}
		}
	}
}

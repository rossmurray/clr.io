using Nancy.Hosting.Self;
using Newtonsoft.Json;
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
			var p = new Program();
			var quitSignal = new ManualResetEventSlim();
			var task = p.Listen(quitSignal);
			Console.WriteLine("Activate! " + p.Uri);
			var interactive = !args.Any(s => s.Equals("-d", StringComparison.CurrentCultureIgnoreCase));
			SignalOnQuit(quitSignal, interactive);
			try
			{
				Task.WaitAll(new[] { task });
			}
			catch(Exception ex)
			{
				Console.Error.WriteLine("Unhandled exception: " + ex);
			}
		}

		static void SignalOnQuit(ManualResetEventSlim quitSignal, bool interactive)
		{
			Task.Factory.StartNew(() =>
			{
				if (!interactive)
				{
					Thread.Sleep(Timeout.Infinite);
				}
				else
				{
					Console.ReadKey();
				}
				quitSignal.Set();
			}, TaskCreationOptions.LongRunning);
		}

		private ConfigurationProvider configProvider;
		public string Uri { get; set; }

		public Program()
		{
			this.configProvider = new ConfigurationProvider();
		}

		public Task Listen(ManualResetEventSlim cancelSignal)
		{
			var config = this.configProvider.GetConfig();
			var uri = "http://localhost:" + config.Port.ToString();
			this.Uri = uri;
			var task = Task.Factory.StartNew(() =>
			{
				using (var host = new NancyHost(new Uri(uri)))
				{
					host.Start();
					cancelSignal.Wait(Timeout.Infinite);
					host.Stop();
				}
			}, TaskCreationOptions.LongRunning);
			return task;
		}
	}
}

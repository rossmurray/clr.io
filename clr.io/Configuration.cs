using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clr.io
{
	public class Configuration
	{
		public int Port { get; set; }

		public Configuration()
		{
			this.Port = 8989;
		}
	}
}

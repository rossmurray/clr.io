using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clr.io.Controllers
{
	public class IndexController : NancyModule
	{
        public IndexController()
		{
			Get["/"] = _ => View["Index"];
		}
	}
}

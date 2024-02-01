using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIMS.UI
{
	public class ErrorController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult NotFound()
		{
			return View();
		}

		public ActionResult AccessDenied()
		{
			return View();
		}

		public ActionResult NoValue()
		{
			return View();
		}
	}
}


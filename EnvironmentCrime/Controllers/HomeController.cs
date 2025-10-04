using EnvironmentCrime.Infrastructure;
using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnvironmentCrime.Controllers
{
	public class HomeController : Controller
	{
		public ViewResult Index()
		{
			Errand? myErrand = HttpContext.Session.Get<Errand>("EnvironmentCrime");
			if (myErrand == null)
			{
				return View();
			}
			else
			{
				return View(myErrand);
			}
		}
		public ViewResult Login()
		{
			return View();
		}
	}
}

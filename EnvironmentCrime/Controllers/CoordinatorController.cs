using EnvironmentCrime.Infrastructure;
using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnvironmentCrime.Controllers
{
	public class CoordinatorController : Controller
	{
		private readonly IERepository repository;
		public CoordinatorController(IERepository repo) => repository = repo;
		public ViewResult CrimeCoordinator(int id)
		{
			// Pass the errandId to the view using ViewBag
			ViewBag.errandId = id;
			return View(repository.Departments);
		}
		public ViewResult ReportCrime()
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

		public ViewResult StartCoordinator()
		{
			return View(repository);
		}

		public ViewResult Thanks()
		{
			/**
       * Spara ny i databasen och visa det nya RefNumber
       */
			HttpContext.Session.Remove("EnvironmentCrime");
			return View();
		}
		/**
     * The Validate action method handles the submission of the errand form.
     * It receives an Errand object populated with user input and returns a view
     * displaying the submitted data for confirmation.
     */
		[HttpPost]
		public ViewResult Validate(Errand errand)
		{
			// Save user input errand to session
			HttpContext.Session.Set("EnvironmentCrime", errand);
			return View(errand);
		}
	}
}

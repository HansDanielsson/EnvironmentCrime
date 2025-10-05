using Microsoft.AspNetCore.Mvc;
using EnvironmentCrime.Models;
using EnvironmentCrime.Infrastructure;

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
      return myErrand == null ? View() : View(myErrand);
    }

    public ViewResult StartCoordinator()
    {
      return View(repository);
    }

    public async Task<ViewResult> Thanks()
    {
      /**
       * Save a new record and display the generated RefNumber
       */
      Errand errand = HttpContext.Session.Get<Errand>("EnvironmentCrime")!;
      ViewBag.RefNumber = await repository.SaveNewErrandAsync(errand);

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

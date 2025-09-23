using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnvironmentCrime.Controllers
{
  public class CoordinatorController : Controller
  {
    private readonly IERepository repository;
    public CoordinatorController(IERepository repo) => repository = repo;
    public ViewResult CrimeCoordinator(string errandid)
    {
      ViewBag.errandId = errandid;
      return View(repository.Departments);
    }
    public ViewResult ReportCrime()
    {
      return View();
    }

    public ViewResult StartCoordinator()
    {
      return View(repository);
    }

    public ViewResult Thanks()
    {
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
      return View(errand);
    }
  }
}

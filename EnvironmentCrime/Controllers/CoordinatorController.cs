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
      var errandDetail = repository.GetErrandDetail(errandid);

      var depatrments = repository.Departments.ToList();

      var viewModel = new CrimeCoordinatorDepartments
      {
        Errand = errandDetail,
        Departments = depatrments
      };

      return View(viewModel);
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
    public ViewResult Validate()
    {
      return View();
    }
  }
}

using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnvironmentCrime.Controllers
{
  public class ManagerController : Controller
  {
    private readonly IERepository repository;
    public ManagerController(IERepository repo) => repository = repo;
    public ViewResult CrimeManager(string errandid)
    {
      var errandDetail = repository.GetErrandDetail(errandid);

      var employees = repository.Employees.ToList();

      var viewModel = new CrimeManagerEmployees
      {
        Errand = errandDetail,
        Employees = employees
        
      };
      return View(viewModel);
    }
    public ViewResult StartManager()
    {
      return View(repository);
    }
  }
}

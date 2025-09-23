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
      ViewBag.errandId = errandid;
      return View(repository.Employees);
    }
    public ViewResult StartManager()
    {
      return View(repository);
    }
  }
}

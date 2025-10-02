using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnvironmentCrime.Controllers
{
  public class ManagerController : Controller
  {
    private readonly IERepository repository;
    public ManagerController(IERepository repo) => repository = repo;
    public ViewResult CrimeManager(int id)
    {
      // Pass the errandId to the view using ViewBag
      ViewBag.errandId = id;
      return View(repository.Employees);
    }
    public ViewResult StartManager()
    {
      return View(repository);
    }
  }
}

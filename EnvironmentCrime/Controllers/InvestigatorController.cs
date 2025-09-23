using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnvironmentCrime.Controllers
{
  public class InvestigatorController : Controller
  {
    private readonly IERepository repository;
    public InvestigatorController(IERepository repo) => repository = repo;
    public ViewResult CrimeInvestigator(string errandid)
    {
      ViewBag.errandId = errandid;
      return View(repository.ErrandStatuses);
    }
    public ViewResult StartInvestigator()
    {
      return View(repository);
    }
  }
}

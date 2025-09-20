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
      var errandDetail = repository.GetErrandDetail(errandid);

      var errandStatus = repository.ErrandStatuses.ToList();

      var viewModel = new CrimeInvestigatorErrandStatuses
      {
        Errand = errandDetail,
        ErrandStatuses = errandStatus
      };
      return View(viewModel);
    }
    public ViewResult StartInvestigator()
    {
      return View(repository);
    }
  }
}

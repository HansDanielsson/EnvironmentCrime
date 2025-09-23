using Microsoft.AspNetCore.Mvc;
using EnvironmentCrime.Models;

namespace EnvironmentCrime.Components
{
  public class CrimeDetailViewComponent : ViewComponent
  { private readonly IERepository repository;
    public CrimeDetailViewComponent(IERepository repo) => repository = repo;
    /**
     * The Invoke method is called when the view component is invoked in a view.
     */
    public IViewComponentResult Invoke()
    {
      var viewModel = repository.GetErrand(ViewBag.errandId);
      return View("CrimeDetail", viewModel);
    }
  }
}

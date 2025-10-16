using EnvironmentCrime.Infrastructure;
using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnvironmentCrime.Components
{
  public class ShowOneCrimeViewComponent : ViewComponent
  {
    private readonly IERepository repository;
    public ShowOneCrimeViewComponent(IERepository repo) => repository = repo;

    /**
     * The InvokeAsync method is called when the view component is invoked in a view.
     */
    public async Task<IViewComponentResult> InvokeAsync(int errandId)
    {
      Errand errand = await repository.GetErrandDetailAsync(errandId);
      /**
       * Save Errand to Session "WorkCrime"
       */
      HttpContext.Session.Set("WorkCrime", errand);
      return View("ShowOneCrime", errand);
    }
  }
}

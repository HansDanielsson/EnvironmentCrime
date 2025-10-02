using Microsoft.AspNetCore.Mvc;
using EnvironmentCrime.Models;

namespace EnvironmentCrime.Components
{
  public class ShowOneCrime : ViewComponent
  {
    private readonly IERepository repository;
    public ShowOneCrime(IERepository repo) => repository = repo;
    /**
     * The InvokeAsync method is called when the view component is invoked in a view.
     */
    public async Task<IViewComponentResult> InvokeAsync(int errandId)
    {
      var viewModel = await repository.GetErrandDetail(errandId);
      return View(viewModel);
    }
  }
}

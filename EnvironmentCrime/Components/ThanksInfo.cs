using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnvironmentCrime.Components
{
  public class ThanksInfo : ViewComponent
  {
    public Task<IViewComponentResult> InvokeAsync(Errand errand)
    {
      return Task.FromResult<IViewComponentResult>(View(errand));
    }
  }
}

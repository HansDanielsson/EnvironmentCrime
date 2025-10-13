using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnvironmentCrime.Components
{
  public class ErrandInput : ViewComponent
  {

    public Task<IViewComponentResult> InvokeAsync(Errand errand)
    {
      return Task.FromResult<IViewComponentResult>(View(errand));
    }
  }
}

using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnvironmentCrime.Components
{
  public class ValidateInputViewComponent : ViewComponent
  {
    public IViewComponentResult Invoke(Errand errand)
    {
      return View("ValidateInput", errand);
    }
  }
}

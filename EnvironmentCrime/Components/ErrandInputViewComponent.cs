using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnvironmentCrime.Components
{
  public class ErrandInputViewComponent : ViewComponent
  {
    public IViewComponentResult Invoke(Errand errand)
    {
      return View("ErrandInput", errand);
    }
  }
}

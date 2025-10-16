using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnvironmentCrime.Components
{
  public class ThanksInfoViewComponent : ViewComponent
  {
    public IViewComponentResult Invoke(Errand errand)
    {
      return View("ThanksInfo", errand);
    }
  }
}

using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnvironmentCrime.Components
{
  public class ThanksInfoViewComponent : ViewComponent
  {
    public IViewComponentResult Invoke()
    {
      return View("ThanksInfo");
    }
  }
}

using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnvironmentCrime.Components
{
  public class StartErrandViewComponent : ViewComponent
  {
    public IViewComponentResult Invoke(List<MyErrand> errands)
    {
      return View("StartErrand", errands);
    }
  }
}

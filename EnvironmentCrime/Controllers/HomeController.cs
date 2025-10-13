using Microsoft.AspNetCore.Mvc;
using EnvironmentCrime.Models;
using EnvironmentCrime.Infrastructure;

namespace EnvironmentCrime.Controllers
{
  public class HomeController : Controller
  {
    public ViewResult Index()
    {
      Errand? myErrand = HttpContext.Session.Get<Errand>("IndexCrime");
      return myErrand == null ? View() : View(myErrand);
    }
  }
}

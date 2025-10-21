using EnvironmentCrime.Infrastructure;
using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnvironmentCrime.Controllers
{
  public class HomeController : Controller
  {
    public ViewResult Index()
    {
      Errand? myErrand = HttpContext.Session.Get<Errand>("IndexCrime");
      return myErrand is null ? View() : View(myErrand);
    }
  }
}

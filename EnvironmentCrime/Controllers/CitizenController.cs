using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnvironmentCrime.Controllers
{
  public class CitizenController : Controller
  {
    public ViewResult Contact()
    {
      return View();
    }
    public ViewResult Faq()
    {
      return View();
    }
    public ViewResult Services()
    {
      return View();
    }
    public ViewResult Thanks()
    {
      return View();
    }
    [HttpPost]
    public ViewResult Validate(Errand errand)
    {
      return View(errand);
    }
  }
}

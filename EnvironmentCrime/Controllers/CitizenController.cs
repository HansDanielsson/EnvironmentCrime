using EnvironmentCrime.Models;
using EnvironmentCrime.Infrastructure;
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
      /**
       * Spara ny i databasen och visa det nya RefNumber
       */
      HttpContext.Session.Remove("EnvironmentCrime");
      return View();
    }
    /**
     * The Validate action method handles the submission of the errand form.
     * It receives an Errand object populated with user input and returns a view
     * displaying the submitted data for confirmation.
     */
    [HttpPost]
    public ViewResult Validate(Errand errand)
    {
      // Save user input errand to session
      HttpContext.Session.Set("EnvironmentCrime", errand);
      return View(errand);
    }
  }
}

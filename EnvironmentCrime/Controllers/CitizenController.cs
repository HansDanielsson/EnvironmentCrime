using EnvironmentCrime.Infrastructure;
using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnvironmentCrime.Controllers
{
  public class CitizenController : Controller
  {
    private readonly IERepository repository;
    public CitizenController(IERepository repo) => repository = repo;
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
    public async Task<ViewResult> Thanks()
    {
      /**
       * Save a new record and display the generated RefNumber
       */
      Errand? errand = HttpContext.Session.Get<Errand>("IndexCrime");
      if (errand is null)
      {
        ViewBag.RefNumber = "Fel med sessionen, registrera ärendet igen!";
      }
      else
      {
        ViewBag.RefNumber = await repository.SaveNewErrandAsync(errand);
      }
      
      HttpContext.Session.Remove("IndexCrime");
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
      HttpContext.Session.Set("IndexCrime", errand);
      return View(errand);
    }
  }
}

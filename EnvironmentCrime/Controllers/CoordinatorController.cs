using EnvironmentCrime.Infrastructure;
using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnvironmentCrime.Controllers
{
  [Authorize(Roles = "Coordinator")]
  public class CoordinatorController : Controller
  {
    private readonly IERepository repository;
    public CoordinatorController(IERepository repo) => repository = repo;
    /*
     * Show errand with id number.
     */
    public ViewResult CrimeCoordinator(int id)
    {
      // Pass the errandId to the view using ViewBag
      ViewBag.errandId = id;
      return View(repository.Departments);
    }
    public ViewResult ReportCrime()
    {
      Errand? myErrand = HttpContext.Session.Get<Errand>("CoordinatorCrime");
      return myErrand == null ? View() : View(myErrand);
    }

    public async Task<ViewResult> StartCoordinator(DropDownViewModel dropDown)
    {
      List<MyErrand> cordinatorList = await repository.GetCoordinatorAsync(dropDown);
      return View(cordinatorList);
    }

    public async Task<ViewResult> Thanks()
    {
      /**
       * Save a new record and display the generated RefNumber
       */
      Errand errand = HttpContext.Session.Get<Errand>("CoordinatorCrime")!;
      ViewBag.RefNumber = await repository.SaveNewErrandAsync(errand);

      HttpContext.Session.Remove("CoordinatorCrime");
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
      if (ModelState.IsValid)
      {
        // Save user input errand to session CoordinatorCrime
        HttpContext.Session.Set("CoordinatorCrime", errand);
      }
      else
      {
        ModelState.AddModelError("", "Nu blev det något fel!");
      }
      return View(errand);
    }
    /**
     * Administrator can update the department key.
     */
    [HttpPost]
    public async Task<IActionResult> SaveDepartment(string DepartmentId)
    {
      if (!string.IsNullOrWhiteSpace(DepartmentId) && DepartmentId != "Välj")
      {
        Errand errand = HttpContext.Session.Get<Errand>("WorkCrime")!;
        errand.DepartmentId = DepartmentId; // Change department
        await repository.SaveErrandAsync(errand);
      }
      return RedirectToAction("StartCoordinator");
    }
    public IActionResult SelectDropDown(DropDownViewModel dropDown)
    {
      return RedirectToAction("StartCoordinator", dropDown);
    }
  }
}

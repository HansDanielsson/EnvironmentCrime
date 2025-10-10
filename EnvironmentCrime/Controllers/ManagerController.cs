using Microsoft.AspNetCore.Mvc;
using EnvironmentCrime.Models;
using EnvironmentCrime.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EnvironmentCrime.Controllers
{
  public class ManagerController : Controller
  {
    private readonly IERepository repository;
    public ManagerController(IERepository repo) => repository = repo;
    public ViewResult CrimeManager(int id)
    {
      // Load Employess from database record
      SaveManagerViewModel viewModel = new()
      {
        Employees = [.. repository.Employees.Select(static e => new SelectListItem
        {
          Value = e.EmployeeId,
          Text = e.EmployeeName
        })]
      };
      // Pass the errandId to the view using ViewBag
      ViewBag.errandId = id;
      return View(viewModel);
    }
    public ViewResult StartManager()
    {
      return View(repository);
    }
    /*
     * Update errand with user input.
     */
    [HttpPost]
    public async Task<IActionResult> SaveManager(SaveManagerViewModel model)
    {
      if (model != null)
      {
        Errand errand = HttpContext.Session.Get<Errand>("WorkCrime")!;
        if (model.NoAction)
        {
          if (!string.IsNullOrEmpty(model.InvestigatorInfo))
          {
            errand.InvestigatorInfo = model.InvestigatorInfo;
            errand.StatusId = "S_B";
            errand.EmployeeId = ""; // remove
          }
        }
        else if (!string.IsNullOrEmpty(model.EmployeeId))
        {
          errand.EmployeeId = model.EmployeeId; // Update
        }
        await repository.SaveErrandAsync(errand);
      }
      return RedirectToAction("StartManager");
    }
  }
}

using EnvironmentCrime.Infrastructure;
using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnvironmentCrime.Controllers
{
  [Authorize(Roles = "Manager")]
  public class ManagerController : Controller
  {
    private readonly IERepository repository;
    public ManagerController(IERepository repo) => repository = repo;
    public ViewResult CrimeManager(int id)
    {
      // Load Employess from database record
      ViewBag.ListOfEmployee = repository.Employees.ToList();
      
      // Pass the errandId to the view using ViewBag
      ViewBag.errandId = id;
      return View();
    }
    public async Task<ViewResult> StartManager()
    {
      List<MyErrand> managerList = await repository.GetManagerAsync();
      return View(managerList);
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

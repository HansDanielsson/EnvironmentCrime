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
    private readonly IHttpContextAccessor contextAcc;
    public ManagerController(IERepository repo, IHttpContextAccessor cont)
    {
      repository = repo;
      contextAcc = cont;
    }

    public async Task<ViewResult> CrimeManager(int id)
    {
      string userName = contextAcc.HttpContext!.User.Identity!.Name!;
      string? userDepartmentId =await repository.Employees.Where(emp => emp.EmployeeId == userName).Select(emp => emp.DepartmentId).FirstOrDefaultAsync();

      // Load Employess from database record
      ViewBag.ListOfEmployee = await repository.Employees.Where(emp => emp.DepartmentId == userDepartmentId).ToListAsync();
      
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
          if (!string.IsNullOrWhiteSpace(model.InvestigatorInfo))
          {
            errand.InvestigatorInfo = model.InvestigatorInfo;
            errand.StatusId = "S_B";
            errand.EmployeeId = ""; // remove
          }
        }
        else if (!string.IsNullOrWhiteSpace(model.EmployeeId) && model.EmployeeId != "Välj")
        {
          errand.EmployeeId = model.EmployeeId; // Update
        }
        await repository.SaveErrandAsync(errand);
      }
      return RedirectToAction("StartManager");
    }
  }
}

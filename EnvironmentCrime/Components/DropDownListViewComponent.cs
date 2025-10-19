using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnvironmentCrime.Components
{
  public class DropDownListViewComponent : ViewComponent
  {
    private readonly IERepository repository;
    private readonly IHttpContextAccessor contextAcc;
    public DropDownListViewComponent(IERepository repo, IHttpContextAccessor cont)
    {
      repository = repo;
      contextAcc = cont;
    }

    public async Task<IViewComponentResult> InvokeAsync(string role)
    {
      DropDownViewModel model = new();
      ViewBag.ErrandStatus = await repository.ErrandStatuses.ToListAsync();
      ViewBag.Department =await repository.Departments.ToListAsync();
      if (role == "Investigator")
      {
        ViewBag.msg1 = "handläggare";
      }
      else if (role == "Manager")
      {
        ViewBag.msg1 = "avdelningschef";
        string userName = contextAcc.HttpContext!.User.Identity!.Name!;
        string? userDepartmentId = await repository.Employees.Where(emp => emp.EmployeeId == userName).Select(emp => emp.DepartmentId).FirstOrDefaultAsync();
        ViewBag.Employee = await repository.Employees.Where(emp => emp.DepartmentId == userDepartmentId && emp.EmployeeId != userName).ToListAsync();
      }
      else
      {
        ViewBag.msg1 = "samordnare";
      }

      return View("DropDownList", model);
    }
  }
}

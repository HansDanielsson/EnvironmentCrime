using Microsoft.AspNetCore.Mvc.Rendering;

namespace EnvironmentCrime.Models;

public class SaveManagerViewModel
{
  public string? EmployeeId { get; set; }
  public bool NoAction { get; set; }
  public string? InvestigatorInfo { get; set; }
  public List<SelectListItem> Employees { get; set; } = [];
}

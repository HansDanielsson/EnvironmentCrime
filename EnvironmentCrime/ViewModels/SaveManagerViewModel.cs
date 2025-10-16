using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace EnvironmentCrime.Models;

public class SaveManagerViewModel
{
  [Display(Name = "Ange handläggare:")]
  public string? EmployeeId { get; set; }
  [Display(Name = "Ingen åtgärd:")]
  public bool NoAction { get; set; }
  public string? InvestigatorInfo { get; set; }
}

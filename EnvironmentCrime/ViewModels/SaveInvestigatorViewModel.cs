using Microsoft.AspNetCore.Mvc.Rendering;

namespace EnvironmentCrime.Models
{
  public class SaveInvestigatorViewModel
  {
    public string? InvestigatorInfo { get; set; }
    public string? InvestigatorAction { get; set; }
    public IFormFile? Sample { get; set; }
    public IFormFile? Picture { get; set; }
    public string? StatusId { get; set; }
    public List<SelectListItem> ErrandStatus { get; set; } = [];
  }
}

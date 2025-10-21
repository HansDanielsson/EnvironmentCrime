using System.ComponentModel.DataAnnotations;

namespace EnvironmentCrime.Models
{
  public class SaveInvestigatorViewModel
  {
    [Display(Name = "Ytterligare information:")]
    public string? InvestigatorInfo { get; set; }
    [Display(Name = "Händelser:")]
    public string? InvestigatorAction { get; set; }
    [Display(Name = "Prover (en i taget, spara):")]
    public IFormFile? Sample { get; set; }
    [Display(Name = "Ladda upp bilder (en i taget, spara):")]
    public IFormFile? Picture { get; set; }
    [Display(Name = "Ändring av status:")]
    public string? StatusId { get; set; }
  }
}

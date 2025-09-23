using System.ComponentModel.DataAnnotations;
namespace EnvironmentCrime.Models
{
  public class Errand
  {
    public required string ErrandId { get; set; }
    [Required(ErrorMessage="Du måste ange platsen!")]
    public string? Place { get; set; }
    [Required(ErrorMessage="Du måste ange typ av brott!")]
    public string? TypeOfCrime { get; set; }
    [Required(ErrorMessage="Du måste ange datum för iakttagelsen!")]
    [DataType(DataType.Date)]
    public DateTime DateOfObservation { get; set; }
    [Required(ErrorMessage="Du måste ange ditt namn!")]
    public string? InformerName { get; set; }
    [Required(ErrorMessage="Du måste ange din telefon!")]
    [RegularExpression(@"^[0]{1}[0-9]{1,3}-[0-9]{5,9}$",ErrorMessage="Formatet är riktnummer-telefonnummer!")]
    public string? InformerPhone { get; set; }
    public string? Observation { get; set; }
    public string? InvestigatorInfo { get; set; }
    public string? InvestigatorAction { get; set; }
    public string? StatusId { get; set; }
    public string? DepartmentId { get; set; }
    public string? EmployeeId { get; set; }
  }
}

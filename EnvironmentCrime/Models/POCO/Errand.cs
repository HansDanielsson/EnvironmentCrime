using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EnvironmentCrime.Models
{
  /**
   * Represents an errand related to environmental crime.
   */
  public class Errand
  {
    /**
     * Unique identifier for the errand.
     */
    public int ErrandId { get; set; }
    public string? RefNumber { get; set; }

    [Display(Name = "Var har brottet skett någonstans?")]
    [Required(ErrorMessage = "Du måste ange platsen!")]
    public required string Place { get; set; }

    [Display(Name = "Vilken typ av brott?")]
    [Required(ErrorMessage = "Du måste ange typ av brott!")]
    public required string TypeOfCrime { get; set; }

    [Display(Name = "När skedde brottet?")]
    [Required(ErrorMessage = "Du måste ange datum för iakttagelsen!")]
    [DataType(DataType.Date)]
    public required DateTime DateOfObservation { get; set; }

    [Display(Name = "Ditt namn (för- och efternamn):")]
    [Required(ErrorMessage = "Du måste ange ditt namn!")]
    public required string InformerName { get; set; }

    [Display(Name = "Din telefon:")]
    [Required(ErrorMessage = "Du måste ange din telefon!")]
    [RegularExpression(@"^[0]{1}[0-9]{1,3}-[0-9]{5,9}$", ErrorMessage = "Formatet är riktnummer-telefonnummer!")]
    public required string InformerPhone { get; set; }

    [Display(Name = "Beskriv din observation (ex. namn på misstänkt person):")]
    public string? Observation { get; set; }

    public string? InvestigatorInfo { get; set; }
    public string? InvestigatorAction { get; set; }
    public string? StatusId { get; set; }
    public string? DepartmentId { get; set; }
    public string? EmployeeId { get; set; }
    [NotMapped]
    public ICollection<Sample>? Samples { get; set; }
    [NotMapped]
    public ICollection<Picture>? Pictures { get; set; }
    [NotMapped]
    public string? StatusName { get; set; }
    [NotMapped]
    public string? DepartmentName { get; set; }
    [NotMapped]
    public string? EmployeeName { get; set; }
  }
}

namespace EnvironmentCrime.Models
{
  public class CrimeInvestigatorErrandStatuses
  {
    public required Errand Errand { get; set; }
    public required List<ErrandStatus> ErrandStatuses { get; set; }
  }
}

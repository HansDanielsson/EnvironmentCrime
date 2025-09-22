namespace EnvironmentCrime.Models
{
  public class CrimeInvestigatorErrandStatuses
  {
    public required ErrandMoreInfo Errand { get; set; }
    public required List<ErrandStatus> ErrandStatuses { get; set; }
  }
}

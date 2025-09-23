namespace EnvironmentCrime.Models
{
  public class ErrandInfo
  {
    public required Errand Errands { get; set; }
    /**
     * These properties are not part of the Errand class, but are included here after lookup on KeyId.
     */
    public required string StatusName { get; set; }
    public required string DepartmentName { get; set; }
    public required string EmployeeName { get; set; }
  }
}

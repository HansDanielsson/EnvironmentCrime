namespace EnvironmentCrime.Models
{
  /**
   * Represents a department within the organization.
   */
  public class Department
  {
    public required string DepartmentId { get; set; }
    public string? DepartmentName { get; set; }
  }
}

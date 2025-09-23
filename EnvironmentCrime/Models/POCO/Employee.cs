namespace EnvironmentCrime.Models
{
  /**
   * Represents an employee in the system.
   */
  public class Employee
  {
    public required string EmployeeId { get; set; }
    public string? EmployeeName { get; set; }
    public string? RoleTitle { get; set; }
    public string? DepartmentId { get; set; }
  }
}

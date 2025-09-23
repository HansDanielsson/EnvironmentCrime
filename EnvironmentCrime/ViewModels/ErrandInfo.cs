namespace EnvironmentCrime.Models
{
  public class ErrandInfo
  {
    public required Errand Errands { get; set; }
    public required string StatusName { get; set; }
    public required string DepartmentName { get; set; }
    public required string EmployeeName { get; set; }
  }
}

namespace EnvironmentCrime.Models
{
  public class CrimeManagerEmployees
  {
    public required ErrandMoreInfo Errand { get; set; }
    public required List<Employee> Employees { get; set; }
  }
}

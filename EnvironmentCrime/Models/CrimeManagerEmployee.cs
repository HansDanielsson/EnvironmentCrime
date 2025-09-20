namespace EnvironmentCrime.Models
{
  public class CrimeManagerEmployee
  {
    public required Errand Errand { get; set; }
    public required List<Employee> Employees { get; set; }
  }
}

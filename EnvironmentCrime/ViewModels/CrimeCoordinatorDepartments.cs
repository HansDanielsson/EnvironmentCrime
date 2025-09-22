namespace EnvironmentCrime.Models
{
  public class CrimeCoordinatorDepartments
  {
    public required ErrandMoreInfo Errand { get; set; }
    public required List<Department> Departments { get; set; }
  }
}

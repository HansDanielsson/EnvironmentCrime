namespace EnvironmentCrime.Models
{
  public interface IERepository
  {
    IQueryable<Errand> Errands { get; }
    IQueryable<Department> Departments { get; }
    IQueryable<ErrandStatus> ErrandStatuses { get; }
    IQueryable<Employee> Employees { get; }
    /**
     * Get single errand with details
     */
    Task<ErrandInfo> GetErrandDetail(string errandid);
    ErrandInfo GetErrand(string errandid);
  }
}

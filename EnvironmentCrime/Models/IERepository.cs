namespace EnvironmentCrime.Models
{
  public interface IERepository
  {
    IQueryable<Department> Departments { get; }
    IQueryable<Employee> Employees { get; }
    IQueryable<Errand> Errands { get; }
    IQueryable<ErrandStatus> ErrandStatuses { get; }
    
    /**
     * Get single errand with details
     */
    Task<ErrandInfo> GetErrandDetail(int errandid);
    ErrandInfo GetErrand(int errandid);
  }
}

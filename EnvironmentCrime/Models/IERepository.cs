namespace EnvironmentCrime.Models
{
  public interface IERepository
  {
		/**
		 * Read:
     * Queryable collections for each entity type.
     */
		IQueryable<Department> Departments { get; }
    IQueryable<Employee> Employees { get; }
    IQueryable<Errand> Errands { get; }
    IQueryable<ErrandStatus> ErrandStatuses { get; }
    IQueryable<Sequence> Sequences { get; }

		/**
     * Get single errand with details
     */
		Task<ErrandInfo> GetErrandDetail(int errandid);
    ErrandInfo GetErrand(int errandid);

		/**
     * Create:
     * Add a new errand to the repository.
     */
    bool SaveNewErrand(Errand errand);

		/**
     * Update:
     * Update an existing item in the repository.
     */
    void UpdateErrand(Errand errand);
    void UpdateSequense(Sequence sequence);
	}
}

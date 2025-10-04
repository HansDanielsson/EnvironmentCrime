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

		/**
     * Create / Update:
     * Add or Update a errand to the repository.
     */
    string SaveErrand(Errand errand);

		/**
     * Update:
     * Update an existing item in the repository.
     */
    void UpdateSequense(Sequence sequence);
	}
}

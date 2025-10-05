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
     * Get single sequense with details
     */
    Task<Sequence> GetSequenceAsync(int seqid);

    /**
     * Create / Update:
     * Add or Update a errand to the repository.
     * Return: True - db insert/update
     *         False - Error
     */
    Task<bool> SaveErrandAsync(Errand errand);
    Task<string> SaveNewErrandAsync(Errand errand);

    /**
     * Update:
     * Update an existing sequence.
     */
    Task<bool> UpdateSequenceAsync(Sequence sequence);
  }
}

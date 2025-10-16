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
    IQueryable<Picture> Pictures { get; }
    IQueryable<Sample> Samples { get; }
    IQueryable<Sequence> Sequences { get; }

    /**
     * Get items in List.
     */
    Task<List<MyErrand>> GetCoordinatorAsync();
    Task<List<MyErrand>> GetInvestigatorAsync();
    Task<List<MyErrand>> GetManagerAsync();
    Task<List<ErrandStatus>> GetErrandStatusAsync();

    /**
     * Get single errand with details
     */
    Task<Errand> GetErrandDetailAsync(int errandId);

    /**
     * Get single sequense with details
     */
    Task<Sequence> GetSequenceAsync(int seqId);

    /**
     * Update: (Not used atm)
     * Update a errand to the repository.
     * Return: True - db insert/update
     *         False - Error
     */
    Task<bool> SaveErrandAsync(Errand errand);
    
    /**
     * Create:
     * Insert an new errand
     * Return: New RefNumber or error message
     */
    Task<string> SaveNewErrandAsync(Errand errand);
    Task<bool> InsertFileAsync(string recordModel, int errandId, string pathFile);

    /**
     * Update: (Not used atm.)
     * Update an existing sequence.
     */
    Task<bool> UpdateSequenceAsync(Sequence sequence);
  }
}

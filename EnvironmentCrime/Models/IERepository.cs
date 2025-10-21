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
    Task<List<MyErrand>> GetErrandsAsync(int model, DropDownViewModel dropDown);

    /**
     * Get single errand with details
     */
    Task<Errand> GetErrandDetailAsync(int errandId);

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
  }
}

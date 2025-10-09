using Microsoft.EntityFrameworkCore;

namespace EnvironmentCrime.Models
{
  public class EFRepository : IERepository
  {
    /**
		 * Read:
     * Queryable collections for each entity type.
     */
    private readonly ApplicationDbContext context;
    public EFRepository(ApplicationDbContext ctx) => context = ctx;
    public IQueryable<Department> Departments => context.Departments;
    public IQueryable<Employee> Employees => context.Employees;
    /** public IQueryable<Errand> Errands => context.Errands.Include(e => e.Samples).Include(e => e.Pictures);*/
    public IQueryable<Errand> Errands => context.Errands;
    public IQueryable<ErrandStatus> ErrandStatuses => context.ErrandStatuses;
    public IQueryable<Picture> Pictures => context.Pictures;
    public IQueryable<Sample> Samples => context.Samples;
    public IQueryable<Sequence> Sequences => context.Sequences;

    /**
     * Get single errand with details
     */
    public async Task<ErrandInfo> GetErrandDetail(int errandid)
    {
      Errand? errand = await Errands.FirstOrDefaultAsync(ed => ed.ErrandId == errandid) ?? throw new InvalidOperationException("Errand not found " + errandid);
      /**
       * Database contains only the Ids for Status, Department and Employee in key fields.
       * lookup the names from the related collections and add to the ViewModel
       */
      ErrandStatus? es = await ErrandStatuses.FirstOrDefaultAsync(st => st.StatusId == errand.StatusId);
      Department? dep = await Departments.FirstOrDefaultAsync(dep => dep.DepartmentId == errand.DepartmentId);
      Employee? emp = await Employees.FirstOrDefaultAsync(emp => emp.EmployeeId == errand.EmployeeId);

      ErrandInfo viewModel = new()
      {
        Errands = errand,
        StatusName = (es == null) ? "Inte angivet" : es.StatusName!,
        DepartmentName = (dep == null) ? "Inte angivet" : dep.DepartmentName!,
        EmployeeName = (emp == null) ? "Inte angivet" : emp.EmployeeName!
      };
      return viewModel;
    }
    /**
     * Get single sequense with details
     */
    public async Task<Sequence> GetSequenceAsync(int seqid)
    {
      return await Sequences.FirstOrDefaultAsync(seq => seq.Id == seqid) ?? throw new InvalidOperationException("Sequence not found " + seqid);
    }
    /**
     * Update:
     * Update a errand to the repository.
     * Return: True - db insert/update
     *         False - Error
     */
    public async Task<bool> SaveErrandAsync(Errand errand)
    {
      try
      {
        Errand? dbEntry = await context.Errands.FirstOrDefaultAsync(ed => ed.ErrandId == errand.ErrandId);
        if (dbEntry == null)
        {
          await context.Errands.AddAsync(errand);
        }
        else
        { // Only change special info.
          dbEntry.InvestigatorInfo = errand.InvestigatorInfo;
          dbEntry.InvestigatorAction = errand.InvestigatorAction;
          dbEntry.StatusId = errand.StatusId;
          dbEntry.DepartmentId = errand.DepartmentId;
          dbEntry.EmployeeId = errand.EmployeeId;
        }
        await context.SaveChangesAsync();
        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }
    /**
     * Create:
     * Insert an new errand
     * Return: New RefNumber or error message
     */
    public async Task<string> SaveNewErrandAsync(Errand errand)
    {
      try
      {
        if (errand != null && errand.ErrandId == 0)
        {
          Sequence? dbSeq = await Sequences.FirstOrDefaultAsync(seq => seq.Id == 1);
          int CurrentValue = dbSeq!.CurrentValue;
          errand.RefNumber = DateTime.Now.Year + "-45-" + CurrentValue;
          errand.InvestigatorInfo = "";
          errand.InvestigatorAction = "";
          errand.StatusId = "S_A";
          errand.DepartmentId = "";
          errand.EmployeeId = "";
          await context.Errands.AddAsync(errand);

          dbSeq.CurrentValue++;
          await context.SaveChangesAsync();

          return errand.RefNumber;
        }
      }
      catch (Exception)
      {
        // All error is ignored
      }
      return "Error in SaveNewErrandAsync";
    }
    public async Task<bool> InsertFileAsync(string recordModel, int errandId, string pathFile)
    {
      if (string.IsNullOrWhiteSpace(recordModel) || string.IsNullOrWhiteSpace(pathFile))
        return false;

      try
      {
        object? entity = recordModel.ToLowerInvariant() switch
        {
          "sample" => new Sample { ErrandId = errandId, SampleName = pathFile },
          "picture" => new Picture { ErrandId = errandId, PictureName = pathFile },
          _ => null
        };

        if (entity == null)
          return false;

        context.Add(entity);
        await context.SaveChangesAsync();
        return true;
      }
      catch
      {
        return false;
      }
    }
    /**
     * Update: (Not used atm.)
     * Update an existing sequence.
     */
    public async Task<bool> UpdateSequenceAsync(Sequence sequence)
    {
      try
      {
        Sequence? dbEntry = await context.Sequences.FirstOrDefaultAsync(seq => seq.Id == sequence.Id);
        if (dbEntry != null)
        {
          dbEntry.CurrentValue = sequence.CurrentValue;
        }
        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }
  }
}

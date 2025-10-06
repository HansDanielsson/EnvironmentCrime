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
    public IQueryable<Errand> Errands => context.Errands;
    public IQueryable<ErrandStatus> ErrandStatuses => context.ErrandStatuses;
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
      var es = await ErrandStatuses.FirstOrDefaultAsync(st => st.StatusId == errand.StatusId);
      string sName = es == null ? "Inte angivet" : es.StatusName!;

      var dep = await Departments.FirstOrDefaultAsync(dep => dep.DepartmentId == errand.DepartmentId);
      string dName = dep == null ? "Inte angivet" : dep.DepartmentName!;

      var emp = await Employees.FirstOrDefaultAsync(emp => emp.EmployeeId == errand.EmployeeId);
      string eName = emp == null ? "Inte angivet" : emp.EmployeeName!;

      ErrandInfo viewModel = new()
      {
        Errands = errand,
        StatusName = sName,
        DepartmentName = dName,
        EmployeeName = eName
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
     * Update: (Not used atm)
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

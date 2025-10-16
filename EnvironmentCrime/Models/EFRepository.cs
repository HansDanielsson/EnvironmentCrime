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
    private readonly IHttpContextAccessor contextAcc;
    public EFRepository(ApplicationDbContext ctx, IHttpContextAccessor cont)
    {
      context = ctx;
      contextAcc = cont;
    }

    public IQueryable<Department> Departments => context.Departments;
    public IQueryable<Employee> Employees => context.Employees;
    public IQueryable<Errand> Errands => context.Errands;
    /**
      .Include(e => e.Samples).Include(e => e.Pictures);
    */
    public IQueryable<ErrandStatus> ErrandStatuses => context.ErrandStatuses;
    public IQueryable<Picture> Pictures => context.Pictures;
    public IQueryable<Sample> Samples => context.Samples;
    public IQueryable<Sequence> Sequences => context.Sequences;

    private static bool CheckString(string? kol)
    {
      if (string.IsNullOrWhiteSpace(kol)) return false;
      if (string.Equals(kol, "Välj alla", StringComparison.OrdinalIgnoreCase)) return false;
      return true;
    }
    /**
     * Read:
     */
    /**
     * Get an List of MyErrand with all errands
     */
    public async Task<List<MyErrand>> GetCoordinatorAsync(DropDownViewModel dropDown)
    {
      var sqlselect = "SELECT e.DateOfObservation, e.ErrandId, e.RefNumber, e.TypeOfCrime, s.StatusName, CASE WHEN e.DepartmentId IS NULL OR LTRIM(RTRIM(e.DepartmentId)) = '' THEN 'ej tillsatt' ELSE d.DepartmentName END AS DepartmentName, CASE WHEN e.EmployeeId IS NULL OR LTRIM(RTRIM(e.EmployeeId)) = '' THEN 'ej tillsatt' ELSE emp.EmployeeName END AS EmployeeName";
      sqlselect += " FROM Errands e";
      sqlselect += " JOIN ErrandStatuses s ON e.StatusId = s.StatusId";
      sqlselect += " LEFT JOIN Departments d ON e.DepartmentId = d.DepartmentId";
      sqlselect += " LEFT JOIN Employees emp ON e.EmployeeId = emp.EmployeeId";
      if (CheckString(dropDown.RefNumber))
      {
        sqlselect += " WHERE e.RefNumber = '" + dropDown.RefNumber + "'";
      }
      else if (CheckString(dropDown?.StatusId) && CheckString(dropDown?.DepartmentId))
      {
        sqlselect += " WHERE e.StatusId = '" + dropDown!.StatusId + "' AND e.DepartmentId = '" + dropDown.DepartmentId + "'";
      }
      else if (CheckString(dropDown?.StatusId))
      {
        sqlselect += " WHERE e.StatusId = '" + dropDown!.StatusId + "'";
      }
      else if (CheckString(dropDown?.DepartmentId))
      {
        sqlselect += " WHERE e.DepartmentId = '" + dropDown!.DepartmentId + "'";
      }
      
      sqlselect += " ORDER BY e.RefNumber DESC";
      List<MyErrand> errandList = await context.MyErrands.FromSqlRaw(sqlselect).ToListAsync();

      return errandList;
    }
    /**
     * For Investigator: Get an List of MyErrand on employeet unit
     */
    public async Task<List<MyErrand>> GetInvestigatorAsync(DropDownViewModel dropDown)
    {
      string userName = contextAcc.HttpContext!.User.Identity!.Name!;

      List<MyErrand> errandList;
      if (!string.IsNullOrWhiteSpace(dropDown?.RefNumber))
      {
        errandList = await (from err in Errands
                            where err.EmployeeId == userName && err.RefNumber == dropDown.RefNumber
                            join stat in ErrandStatuses on err.StatusId equals stat.StatusId
                            join dep in Departments on err.DepartmentId equals dep.DepartmentId into departmentErrand
                            from deptE in departmentErrand.DefaultIfEmpty()
                            join em in Employees on err.EmployeeId equals em.EmployeeId into employeeErrand
                            from empE in employeeErrand.DefaultIfEmpty()
                            orderby err.RefNumber descending
                            select new MyErrand
                            {
                              DateOfObservation = err.DateOfObservation,
                              ErrandId = err.ErrandId,
                              RefNumber = err.RefNumber!,
                              TypeOfCrime = err.TypeOfCrime,
                              StatusName = stat.StatusName!,
                              DepartmentName = string.IsNullOrWhiteSpace(err.DepartmentId) ? "ej tillsatt" : deptE.DepartmentName,
                              EmployeeName = string.IsNullOrWhiteSpace(err.EmployeeId) ? "ej tillsatt" : empE.EmployeeName
                            }).ToListAsync();
      }
      else if (!string.IsNullOrWhiteSpace(dropDown?.StatusId))
      {
        errandList = await (from err in Errands
                            where err.EmployeeId == userName && err.StatusId == dropDown.StatusId
                            join stat in ErrandStatuses on err.StatusId equals stat.StatusId
                            join dep in Departments on err.DepartmentId equals dep.DepartmentId into departmentErrand
                            from deptE in departmentErrand.DefaultIfEmpty()
                            join em in Employees on err.EmployeeId equals em.EmployeeId into employeeErrand
                            from empE in employeeErrand.DefaultIfEmpty()
                            orderby err.RefNumber descending
                            select new MyErrand
                            {
                              DateOfObservation = err.DateOfObservation,
                              ErrandId = err.ErrandId,
                              RefNumber = err.RefNumber!,
                              TypeOfCrime = err.TypeOfCrime,
                              StatusName = stat.StatusName!,
                              DepartmentName = string.IsNullOrWhiteSpace(err.DepartmentId) ? "ej tillsatt" : deptE.DepartmentName,
                              EmployeeName = string.IsNullOrWhiteSpace(err.EmployeeId) ? "ej tillsatt" : empE.EmployeeName
                            }).ToListAsync();
      }
      else
      {
        errandList = await (from err in Errands
                            where err.EmployeeId == userName
                            join stat in ErrandStatuses on err.StatusId equals stat.StatusId
                            join dep in Departments on err.DepartmentId equals dep.DepartmentId into departmentErrand
                            from deptE in departmentErrand.DefaultIfEmpty()
                            join em in Employees on err.EmployeeId equals em.EmployeeId into employeeErrand
                            from empE in employeeErrand.DefaultIfEmpty()
                            orderby err.RefNumber descending
                            select new MyErrand
                            {
                              DateOfObservation = err.DateOfObservation,
                              ErrandId = err.ErrandId,
                              RefNumber = err.RefNumber!,
                              TypeOfCrime = err.TypeOfCrime,
                              StatusName = stat.StatusName!,
                              DepartmentName = string.IsNullOrWhiteSpace(err.DepartmentId) ? "ej tillsatt" : deptE.DepartmentName,
                              EmployeeName = string.IsNullOrWhiteSpace(err.EmployeeId) ? "ej tillsatt" : empE.EmployeeName
                            }).ToListAsync();
      }
      return errandList;
    }
    /**
     * For Manager: Get an List of MyErrand on department unit
     */
    public async Task<List<MyErrand>> GetManagerAsync(DropDownViewModel dropDown)
    {
      string userName = contextAcc.HttpContext!.User.Identity!.Name!;
      string? userDepartmentId = await Employees.Where(emp => emp.EmployeeId == userName).Select(emp => emp.DepartmentId).FirstOrDefaultAsync();

      List<MyErrand> errandList;
      if (!string.IsNullOrWhiteSpace(dropDown?.RefNumber))
      {
        errandList = await (from err in Errands
                            where err.DepartmentId == userDepartmentId && err.RefNumber == dropDown.RefNumber
                            join stat in ErrandStatuses on err.StatusId equals stat.StatusId
                            join dep in Departments on err.DepartmentId equals dep.DepartmentId into departmentErrand
                            from deptE in departmentErrand.DefaultIfEmpty()
                            join em in Employees on err.EmployeeId equals em.EmployeeId into employeeErrand
                            from empE in employeeErrand.DefaultIfEmpty()
                            orderby err.RefNumber descending
                            select new MyErrand
                            {
                              DateOfObservation = err.DateOfObservation,
                              ErrandId = err.ErrandId,
                              RefNumber = err.RefNumber!,
                              TypeOfCrime = err.TypeOfCrime,
                              StatusName = stat.StatusName!,
                              DepartmentName = string.IsNullOrWhiteSpace(err.DepartmentId) ? "ej tillsatt" : deptE.DepartmentName,
                              EmployeeName = string.IsNullOrWhiteSpace(err.EmployeeId) ? "ej tillsatt" : empE.EmployeeName
                            }).ToListAsync();
      }
      else if (!(string.IsNullOrWhiteSpace(dropDown?.StatusId) || string.IsNullOrWhiteSpace(dropDown?.EmployeeId)))
      {
        errandList = await (from err in Errands
                            where err.DepartmentId == userDepartmentId && err.StatusId == dropDown.StatusId && err.EmployeeId == dropDown.EmployeeId
                            join stat in ErrandStatuses on err.StatusId equals stat.StatusId
                            join dep in Departments on err.DepartmentId equals dep.DepartmentId into departmentErrand
                            from deptE in departmentErrand.DefaultIfEmpty()
                            join em in Employees on err.EmployeeId equals em.EmployeeId into employeeErrand
                            from empE in employeeErrand.DefaultIfEmpty()
                            orderby err.RefNumber descending
                            select new MyErrand
                            {
                              DateOfObservation = err.DateOfObservation,
                              ErrandId = err.ErrandId,
                              RefNumber = err.RefNumber!,
                              TypeOfCrime = err.TypeOfCrime,
                              StatusName = stat.StatusName!,
                              DepartmentName = string.IsNullOrWhiteSpace(err.DepartmentId) ? "ej tillsatt" : deptE.DepartmentName,
                              EmployeeName = string.IsNullOrWhiteSpace(err.EmployeeId) ? "ej tillsatt" : empE.EmployeeName
                            }).ToListAsync();
      }
      else if (!string.IsNullOrWhiteSpace(dropDown?.StatusId))
      {
        errandList = await (from err in Errands
                            where err.DepartmentId == userDepartmentId && err.StatusId == dropDown.StatusId
                            join stat in ErrandStatuses on err.StatusId equals stat.StatusId
                            join dep in Departments on err.DepartmentId equals dep.DepartmentId into departmentErrand
                            from deptE in departmentErrand.DefaultIfEmpty()
                            join em in Employees on err.EmployeeId equals em.EmployeeId into employeeErrand
                            from empE in employeeErrand.DefaultIfEmpty()
                            orderby err.RefNumber descending
                            select new MyErrand
                            {
                              DateOfObservation = err.DateOfObservation,
                              ErrandId = err.ErrandId,
                              RefNumber = err.RefNumber!,
                              TypeOfCrime = err.TypeOfCrime,
                              StatusName = stat.StatusName!,
                              DepartmentName = string.IsNullOrWhiteSpace(err.DepartmentId) ? "ej tillsatt" : deptE.DepartmentName,
                              EmployeeName = string.IsNullOrWhiteSpace(err.EmployeeId) ? "ej tillsatt" : empE.EmployeeName
                            }).ToListAsync();
      }
      else if (!string.IsNullOrWhiteSpace(dropDown?.EmployeeId))
      {
        errandList = await (from err in Errands
                            where err.DepartmentId == userDepartmentId && err.EmployeeId == dropDown.EmployeeId
                            join stat in ErrandStatuses on err.StatusId equals stat.StatusId
                            join dep in Departments on err.DepartmentId equals dep.DepartmentId into departmentErrand
                            from deptE in departmentErrand.DefaultIfEmpty()
                            join em in Employees on err.EmployeeId equals em.EmployeeId into employeeErrand
                            from empE in employeeErrand.DefaultIfEmpty()
                            orderby err.RefNumber descending
                            select new MyErrand
                            {
                              DateOfObservation = err.DateOfObservation,
                              ErrandId = err.ErrandId,
                              RefNumber = err.RefNumber!,
                              TypeOfCrime = err.TypeOfCrime,
                              StatusName = stat.StatusName!,
                              DepartmentName = string.IsNullOrWhiteSpace(err.DepartmentId) ? "ej tillsatt" : deptE.DepartmentName,
                              EmployeeName = string.IsNullOrWhiteSpace(err.EmployeeId) ? "ej tillsatt" : empE.EmployeeName
                            }).ToListAsync();
      }
      else
      {
        errandList = await (from err in Errands
                            where err.DepartmentId == userDepartmentId
                            join stat in ErrandStatuses on err.StatusId equals stat.StatusId
                            join dep in Departments on err.DepartmentId equals dep.DepartmentId into departmentErrand
                            from deptE in departmentErrand.DefaultIfEmpty()
                            join em in Employees on err.EmployeeId equals em.EmployeeId into employeeErrand
                            from empE in employeeErrand.DefaultIfEmpty()
                            orderby err.RefNumber descending
                            select new MyErrand
                            {
                              DateOfObservation = err.DateOfObservation,
                              ErrandId = err.ErrandId,
                              RefNumber = err.RefNumber!,
                              TypeOfCrime = err.TypeOfCrime,
                              StatusName = stat.StatusName!,
                              DepartmentName = string.IsNullOrWhiteSpace(err.DepartmentId) ? "ej tillsatt" : deptE.DepartmentName,
                              EmployeeName = string.IsNullOrWhiteSpace(err.EmployeeId) ? "ej tillsatt" : empE.EmployeeName
                            }).ToListAsync();
      }
      return errandList;
    }
    /**
     * Return List of ErrandStatus
     */
    public async Task<List<ErrandStatus>> GetErrandStatusAsync()
    {
      return await ErrandStatuses.ToListAsync();
    }

    /**
     * Get single errand with details
     */
    public async Task<Errand> GetErrandDetailAsync(int errandId)
    {
      Errand? errand = await (from err in Errands
                              where err.ErrandId == errandId
                              join stat in ErrandStatuses on err.StatusId equals stat.StatusId
                              join dep in Departments on err.DepartmentId equals dep.DepartmentId into departmentErrand
                              from deptE in departmentErrand.DefaultIfEmpty()
                              join em in Employees on err.EmployeeId equals em.EmployeeId into employeeErrand
                              from empE in employeeErrand.DefaultIfEmpty()
                              select new Errand
                              {
                                ErrandId = err.ErrandId,
                                RefNumber = err.RefNumber,
                                Place = err.Place,
                                TypeOfCrime = err.TypeOfCrime,
                                DateOfObservation = err.DateOfObservation,
                                InformerName = err.InformerName,
                                InformerPhone = err.InformerPhone,
                                Observation = err.Observation,
                                InvestigatorInfo = err.InvestigatorInfo,
                                InvestigatorAction = err.InvestigatorAction,
                                StatusId = err.StatusId,
                                DepartmentId = err.DepartmentId,
                                EmployeeId = err.EmployeeId,
                                Samples = err.Samples,
                                Pictures = err.Pictures,
                                StatusName = stat.StatusName,
                                DepartmentName = string.IsNullOrWhiteSpace(err.DepartmentId) ? "ej tillsatt" : deptE.DepartmentName,
                                EmployeeName = string.IsNullOrWhiteSpace(err.EmployeeId) ? "ej tillsatt" : empE.EmployeeName
                              }).FirstOrDefaultAsync();

      if (errand == null)
      {
        throw new InvalidOperationException("Errand not found " + errandId);
      }

      return errand;
    }
    /**
     * Get single sequense with details
     */
    public async Task<Sequence> GetSequenceAsync(int seqId)
    {
      Sequence? seq = await Sequences.FirstOrDefaultAsync(seq => seq.Id == seqId);
      if (seq == null)
      {
        throw new InvalidOperationException("Sequence not found " + seqId);
      }

      return seq;
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
          await context.Errands.AddAsync(errand); // Insert new record.
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
        // Ignore errors
      }
      return false;
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
        // Ignore errors
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

        context.Add(entity); // Insert new record in db.
        await context.SaveChangesAsync();
        return true;
      }
      catch
      {
        // Ignore errors
      }
      return false;
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
        // Ignore errors
      }
      return false;
    }
  }
}

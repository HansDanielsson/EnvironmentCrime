namespace EnvironmentCrime.Models
{
	public class EFRepository : IERepository
	{
		private ApplicationDbContext context;
		public EFRepository(ApplicationDbContext ctx) => context = ctx;
		public IQueryable<Department> Departments => context.Departments;
		public IQueryable<Employee> Employees => context.Employees;
		public IQueryable<Errand> Errands => context.Errands;
		public IQueryable<ErrandStatus> ErrandStatuses => context.ErrandStatuses;
		public IQueryable<Sequence> Sequences => context.Sequences;
		public Task<ErrandInfo> GetErrandDetail(int errandid)
		{
			return Task.Run(() =>
			{
				Errand? errand = Errands.FirstOrDefault(ed => ed.ErrandId == errandid) ?? throw new InvalidOperationException("Errand not found " + errandid);
				ErrandInfo viewModel = new()
				{
					Errands = errand!,
					/**
           * Database contains only the Ids for Status, Department and Employee in key fields.
           * lookup the names from the related collections and add to the ViewModel
           */
					StatusName = ErrandStatuses.FirstOrDefault(st => st.StatusId == errand!.StatusId)?.StatusName ?? "Inte angivet",
					DepartmentName = Departments.FirstOrDefault(dep => dep.DepartmentId == errand!.DepartmentId)?.DepartmentName ?? "Inte angivet",
					EmployeeName = Employees.FirstOrDefault(emp => emp.EmployeeId == errand!.EmployeeId)?.EmployeeName ?? "Inte angivet"
				};
				return viewModel;
			});
		}
		public ErrandInfo GetErrand(int errandid)
		{
			Errand? errand = Errands.FirstOrDefault(ed => ed.ErrandId == errandid) ?? throw new InvalidOperationException("Errand not found " + errandid);
			var viewModel = new ErrandInfo
			{
				Errands = errand!,
				/**
				 * Database contains only the Ids for Status, Department and Employee in key fields.
				 * lookup the names from the related collections and add to the ViewModel
				 */
				StatusName = ErrandStatuses.FirstOrDefault(st => st.StatusId == errand!.StatusId)?.StatusName ?? "Inte angivet",
				DepartmentName = Departments.FirstOrDefault(dep => dep.DepartmentId == errand!.DepartmentId)?.DepartmentName ?? "Inte angivet",
				EmployeeName = Employees.FirstOrDefault(emp => emp.EmployeeId == errand!.EmployeeId)?.EmployeeName ?? "Inte angivet"
			};
			return viewModel;
		}
		/**
		 * Create:
		 * Add a new errand to the repository.
		 */
		public bool SaveNewErrand(Errand errand)
		{
			try
			{
				context.Errands.Add(errand);
				context.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
		/**
		 * Update:
		 * Update an existing item in the repository.
		 * ----> INFO: NOT TESTED <----
		 */
		public void UpdateErrand(Errand errand)
		{
			Errand? dbEntry = context.Errands.FirstOrDefault(ed => ed.ErrandId == errand.ErrandId);
			if (dbEntry != null)
			{
				dbEntry.InvestigatorInfo = errand.InvestigatorInfo;
				dbEntry.InvestigatorAction = errand.InvestigatorAction;
				dbEntry.StatusId = errand.StatusId;
				dbEntry.DepartmentId = errand.DepartmentId;
				dbEntry.EmployeeId = errand.EmployeeId;
				context.SaveChanges();
			}
		}
		public void UpdateSequense(Sequence sequence)
		{
			Sequence? dbEntry = context.Sequences.FirstOrDefault(sq => sq.Id == sequence.Id);
			if (dbEntry != null)
			{
				dbEntry.CurrentValue = sequence.CurrentValue;
				context.SaveChanges();
			}
		}
	}
}

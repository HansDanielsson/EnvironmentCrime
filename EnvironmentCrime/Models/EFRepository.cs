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
		public Task<ErrandInfo> GetErrandDetail(int errandid)
		{
			return Task.Run(() =>
			{
				Errand? errand = Errands.FirstOrDefault(ed => ed.ErrandId == errandid);
				if (errand == null)
				{
					throw new InvalidOperationException("Errand not found " + errandid);
				}
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
			});
		}
		public ErrandInfo GetErrand(int errandid)
		{
			Errand? errand = Errands.FirstOrDefault(ed => ed.ErrandId == errandid);
			if (errand == null)
			{
				throw new InvalidOperationException("Errand not found " + errandid);
			}
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
	}
}

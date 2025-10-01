namespace EnvironmentCrime.Models
{
	public class EFRepository : IERepository
	{
		private ApplicationDbContext context;
		public EFRepository(ApplicationDbContext ctx)
		{
			context = ctx;
		}
		public IQueryable<Errand> Errands => context.Errands;
		public IQueryable<ErrandStatus> ErrandStatuses => context.ErrandStatuses;

	}
}

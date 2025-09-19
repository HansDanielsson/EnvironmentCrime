namespace EnvironmentCrime.Models
{
  public interface IERepository
  {
    IQueryable<Errand> Errands { get; }
  }
}

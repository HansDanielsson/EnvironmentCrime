using Microsoft.EntityFrameworkCore;

namespace EnvironmentCrime.Models
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Errand> Errands { get; set; }
    public DbSet<ErrandStatus> ErrandStatuses { get; set; }
    public DbSet<Picture> Pictures { get; set; }
    public DbSet<Sample> Samples { get; set; }
    public DbSet<Sequence> Sequences { get; set; }
    // DTO/query-objekt
    public DbSet<MyErrand> MyErrands { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      // Eftersom MyErrand inte är en tabell
      modelBuilder.Entity<MyErrand>().HasNoKey();        // <- krävs!
      modelBuilder.Entity<MyErrand>().ToView(null); // <- säg att det inte är en vy heller
    }
  }
}

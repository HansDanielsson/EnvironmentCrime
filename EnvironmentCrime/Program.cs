using EnvironmentCrime.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IERepository, EFRepository>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSession();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
  var services = scope.ServiceProvider;
  DBInitializer.EnsurePopulated(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();   // Konfiguration för sessions

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Investigator}/{action=CrimeInvestigator}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Manager}/{action=CrimeManager}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Coordinator}/{action=CrimeCoordinator}/{id?}");

// Await RunAsync instead of Run to fix S6966
await app.RunAsync();

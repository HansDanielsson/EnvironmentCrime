using EnvironmentCrime.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IERepository, FakeRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

app.UseAuthorization();

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

// Await RunAsync instead of Run to fix S6966
await app.RunAsync();

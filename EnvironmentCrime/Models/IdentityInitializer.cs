using Microsoft.AspNetCore.Identity;

namespace EnvironmentCrime.Models
{
  public static class IdentityInitializer
  {
    public static async Task EnsurePopulated(IServiceProvider services)
    {
      var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
      var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

      await CreateRoles(roleManager);
      await CreateUser(userManager);
    }

    private static async Task CreateRoles(RoleManager<IdentityRole> rManager)
    {
      if (!await rManager.RoleExistsAsync("Coordinator"))
      {
        await rManager.CreateAsync(new IdentityRole("Coordinator"));
      }
      if (!await rManager.RoleExistsAsync("Manager"))
      {
        await rManager.CreateAsync(new IdentityRole("Manager"));
      }
      if (!await rManager.RoleExistsAsync("Investigator"))
      {
        await rManager.CreateAsync(new IdentityRole("Investigator"));
      }
    }
    private static async Task CreateUser(UserManager<IdentityUser> uManager)
    {
      IdentityUser E001 = new("E001");
      IdentityUser E100 = new("E100");
      IdentityUser E101 = new("E101");
      IdentityUser E102 = new("E102");
      IdentityUser E103 = new("E103");
      IdentityUser E200 = new("E200");
      IdentityUser E201 = new("E201");
      IdentityUser E202 = new("E202");
      IdentityUser E203 = new("E203");
      IdentityUser E300 = new("E300");
      IdentityUser E301 = new("E301");
      IdentityUser E302 = new("E302");
      IdentityUser E303 = new("E303");
      IdentityUser E400 = new("E400");
      IdentityUser E401 = new("E401");
      IdentityUser E402 = new("E402");
      IdentityUser E403 = new("E403");
      IdentityUser E500 = new("E500");
      IdentityUser E501 = new("E501");
      IdentityUser E502 = new("E502");
      IdentityUser E503 = new("E503");

      await uManager.CreateAsync(E001, "Pass01?");
      await uManager.CreateAsync(E100, "Pass02?");
      await uManager.CreateAsync(E101, "Pass03?");
      await uManager.CreateAsync(E102, "Pass04?");
      await uManager.CreateAsync(E103, "Pass05?");
      await uManager.CreateAsync(E200, "Pass06?");
      await uManager.CreateAsync(E201, "Pass07?");
      await uManager.CreateAsync(E202, "Pass08?");
      await uManager.CreateAsync(E203, "Pass09?");
      await uManager.CreateAsync(E300, "Pass10?");
      await uManager.CreateAsync(E301, "Pass11?");
      await uManager.CreateAsync(E302, "Pass12?");
      await uManager.CreateAsync(E303, "Pass13?");
      await uManager.CreateAsync(E400, "Pass14?");
      await uManager.CreateAsync(E401, "Pass15?");
      await uManager.CreateAsync(E402, "Pass16?");
      await uManager.CreateAsync(E403, "Pass17?");
      await uManager.CreateAsync(E500, "Pass18?");
      await uManager.CreateAsync(E501, "Pass19?");
      await uManager.CreateAsync(E502, "Pass20?");
      await uManager.CreateAsync(E503, "Pass21?");

      await uManager.AddToRoleAsync(E001, "Coordinator");
      await uManager.AddToRoleAsync(E100, "Manager");
      await uManager.AddToRoleAsync(E101, "Investigator");
      await uManager.AddToRoleAsync(E102, "Investigator");
      await uManager.AddToRoleAsync(E103, "Investigator");
      await uManager.AddToRoleAsync(E200, "Manager");
      await uManager.AddToRoleAsync(E201, "Investigator");
      await uManager.AddToRoleAsync(E202, "Investigator");
      await uManager.AddToRoleAsync(E203, "Investigator");
      await uManager.AddToRoleAsync(E300, "Manager");
      await uManager.AddToRoleAsync(E301, "Investigator");
      await uManager.AddToRoleAsync(E302, "Investigator");
      await uManager.AddToRoleAsync(E303, "Investigator");
      await uManager.AddToRoleAsync(E400, "Manager");
      await uManager.AddToRoleAsync(E401, "Investigator");
      await uManager.AddToRoleAsync(E402, "Investigator");
      await uManager.AddToRoleAsync(E403, "Investigator");
      await uManager.AddToRoleAsync(E500, "Manager");
      await uManager.AddToRoleAsync(E501, "Investigator");
      await uManager.AddToRoleAsync(E502, "Investigator");
      await uManager.AddToRoleAsync(E503, "Investigator");
    }
  }
}

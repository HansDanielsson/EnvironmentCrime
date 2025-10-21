using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EnvironmentCrime.Controllers
{
  [Authorize]
  public class AccountController : Controller
  {
    private readonly UserManager<IdentityUser> userManager;
    private readonly SignInManager<IdentityUser> signInManager;

    public AccountController(UserManager<IdentityUser> userMgr, SignInManager<IdentityUser> signInMgr)
    {
      userManager = userMgr;
      signInManager = signInMgr;
    }
    [AllowAnonymous]
    public ViewResult Login(string returnUrl = "")
    {
      return View(new LoginModel
      {
        ReturnUrl = returnUrl
      });
    }
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginModel loginModel)
    {
      if (ModelState.IsValid)
      {
        IdentityUser? user = await userManager.FindByNameAsync(loginModel.UserName!);
        if (user is not null)
        {
          await signInManager.SignOutAsync();
          if ((await signInManager.PasswordSignInAsync(user, loginModel.Password!, false, false)).Succeeded)
          {
            if (await userManager.IsInRoleAsync(user,"Coordinator"))
            {
              return Redirect("/Coordinator/StartCoordinator");
            }
            if (await userManager.IsInRoleAsync(user, "Manager"))
            {
              return Redirect("/Manager/StartManager");
            }
            if (await userManager.IsInRoleAsync(user, "Investigator"))
            {
              return Redirect("/Investigator/StartInvestigator");
            }
          }
        }
      }
      ModelState.AddModelError("", "Felaktigt användarnamn eller lösenord");
      return View(loginModel);
    }
    public async Task<RedirectResult> Logout(string returnUrl = "/")
    {
      await signInManager.SignOutAsync();
      return Redirect(returnUrl);
    }
    public async Task<IActionResult> AccessDenied()
    {
      await signInManager.SignOutAsync();
      return View();
    }
  }
}

using Microsoft.AspNetCore.Mvc;

namespace EnvironmentCrime.Controllers
{
  public class CoordinatorController : Controller
  {
    /** Sets the user level in TempData.
     * @param level The user level to set. Not used in current implementation.
     */
    private void SetUserLevel(int level)
    {
      TempData["User"] = "samordnare";
    }
    public ViewResult CrimeCoordinator()
    {
      SetUserLevel(1);
      return View();
    }
    public ViewResult ReportCrime()
    {
      SetUserLevel(2);
      return View();
    }

    public ViewResult StartCoordinator()
    {
      SetUserLevel(3);
      return View();
    }

    public ViewResult Thanks()
    {
      SetUserLevel(4);
      return View();
    }
    public ViewResult Validate()
    {
      SetUserLevel(5);
      return View();
    }
  }
}

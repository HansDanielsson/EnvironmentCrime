using Microsoft.AspNetCore.Mvc;

namespace EnvironmentCrime.Controllers
{
  public class InvestigatorController : Controller
  {
    /** Sets the user level in TempData.
     * @param level The user level to set. Not used in current implementation.
     */
    private void SetUserLevel(int level)
    {
      TempData["User"] = "handläggare";
    }
    public ViewResult CrimeInvestigator()
    {
      SetUserLevel(1);
      return View();
    }
    public ViewResult StartInvestigator()
    {
      SetUserLevel(2);
      return View();
    }
  }
}

using Microsoft.AspNetCore.Mvc;

namespace EnvironmentCrime.Controllers
{
  public class ManagerController : Controller
  {
    /** Sets the user level in TempData.
     * @param level The user level to set. Not used in current implementation.
     */
    private void SetUserLevel(int level)
    {
      TempData["User"] = "avdelningschef";
    }
    public ViewResult CrimeManager()
    {
      SetUserLevel(1);
      return View();
    }

    public ViewResult StartManager()
    {
      SetUserLevel(2);
      return View();
    }
  }
}

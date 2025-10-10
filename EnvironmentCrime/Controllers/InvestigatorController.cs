using Microsoft.AspNetCore.Mvc;
using EnvironmentCrime.Models;
using EnvironmentCrime.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EnvironmentCrime.Controllers
{
  public class InvestigatorController : Controller
  {
    private readonly IERepository repository;
    private readonly IWebHostEnvironment environment;
    public InvestigatorController(IERepository repo, IWebHostEnvironment env)
    {
      repository = repo;
      environment = env;
    }
    /*
     * Show errand with id number.
     */
    public ViewResult CrimeInvestigator(int id)
    {
      SaveInvestigatorViewModel viewModel = new()
      {
        ErrandStatus = [.. repository.ErrandStatuses
        .Where(static e => e.StatusId == "S_C" || e.StatusId == "S_D") // Only "påbörjad" and "klar"
        .Select(static e => new SelectListItem
        {
          Value = e.StatusId,
          Text = e.StatusName
        })]
      };
      // Pass the errandId to the view using ViewBag
      ViewBag.errandId = id;
      return View(viewModel);
    }
    public ViewResult StartInvestigator()
    {
      return View(repository);
    }

    /**
     * Private help function to save upload files to correct dir.
     */
    private async Task SaveFileAsync(int errandId, string dirPath, IFormFile? file)
    {
      if (file is not { FileName: not null, Length: > 0 })
        return;

      // Uniq file name
      string uniqueFileSample = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";

      // Path to save file
      string fullPath = Path.Combine(environment.WebRootPath, dirPath, uniqueFileSample);

      await using var stream = new FileStream(fullPath, FileMode.Create);
      await file.CopyToAsync(stream);
      await repository.InsertFileAsync(dirPath, errandId, uniqueFileSample); // Save to database
    }

    /**
     * Update record after edit
     * 
     */
    [HttpPost]
    public async Task<IActionResult> SaveInvestigator(SaveInvestigatorViewModel model)
    {
      if (model != null)
      {
        Errand errand = HttpContext.Session.Get<Errand>("WorkCrime")!;

        if (!string.IsNullOrEmpty(model.InvestigatorInfo))
        {
          errand.InvestigatorInfo += " " + model.InvestigatorInfo; // Add text
        }

        if (!string.IsNullOrEmpty(model.InvestigatorAction))
        {
          errand.InvestigatorAction += " " + model.InvestigatorAction; // Add text
        }

        await SaveFileAsync(errand.ErrandId, "sample", model.Sample);
        await SaveFileAsync(errand.ErrandId, "picture", model.Picture);

        if (!string.IsNullOrEmpty(model.StatusId))
        {
          errand.StatusId = model.StatusId;
        }
        await repository.SaveErrandAsync(errand);
      }
      return RedirectToAction("StartInvestigator");
    }
  }
}

using EnvironmentCrime.Infrastructure;
using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnvironmentCrime.Controllers
{
  [Authorize(Roles = "Investigator")]
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
    public async Task<ViewResult> CrimeInvestigator(int id)
    {
      ViewBag.ListOfStatus = await repository.ErrandStatuses
        .Where(e => e.StatusId == "S_C" || e.StatusId == "S_D").ToListAsync(); // Only "påbörjad" and "klar"

      // Pass the errandId to the view using ViewBag
      ViewBag.errandId = id;
      return View();
    }
    public async Task<ViewResult> StartInvestigator(DropDownViewModel dropDown)
    {
      List<MyErrand> investigatorList = await repository.GetErrandsAsync(2, dropDown);
      return View(investigatorList);
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
      if (model is not null)
      {
        Errand errand = HttpContext.Session.Get<Errand>("WorkCrime")!;

        if (!string.IsNullOrWhiteSpace(model.InvestigatorInfo))
        {
          errand.InvestigatorInfo += " " + model.InvestigatorInfo; // Add text
        }

        if (!string.IsNullOrWhiteSpace(model.InvestigatorAction))
        {
          errand.InvestigatorAction += " " + model.InvestigatorAction; // Add text
        }

        await SaveFileAsync(errand.ErrandId, "sample", model.Sample);
        await SaveFileAsync(errand.ErrandId, "picture", model.Picture);

        if (!string.IsNullOrWhiteSpace(model.StatusId) && model.StatusId != "Välj")
        {
          errand.StatusId = model.StatusId;
        }
        await repository.SaveErrandAsync(errand);
      }
      return RedirectToAction("StartInvestigator");
    }
    public IActionResult SelectDropDown(DropDownViewModel dropDown)
    {
      
      return RedirectToAction("StartInvestigator", dropDown);
    }
  }
}

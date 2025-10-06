using Microsoft.EntityFrameworkCore;

namespace EnvironmentCrime.Models
{
  public class FakeRepository : IERepository
  {
    /**
     * Using in-memory collections to simulate a database.
     * Simulate some errand of crime reports
     * Changed StatusId, DepartmentId and EmployeeId to correct database keys
     */
    public IQueryable<Errand> Errands =>
      new List<Errand>
      {
        new() { RefNumber = "2025-45-0001", Place = "Skogslunden vid Jensens gård", TypeOfCrime = "Sopor", DateOfObservation = new DateTime(2025,04,24,0,0,0,DateTimeKind.Local), Observation = "Anmälaren var på promenad i skogslunden när hon upptäckte soporna", InvestigatorInfo = "Undersökning har gjorts och bland soporna hittades bl.a ett brev till Gösta Olsson", InvestigatorAction = "Brev har skickats till Gösta Olsson om soporna och anmälan har gjorts till polisen 2025-05-01", InformerName = "Ada Bengtsson", InformerPhone = "0432-5545522", StatusId = "S_D", DepartmentId = "D03", EmployeeId ="E302"},
        new() { RefNumber = "2025-45-0002", Place = "Småstadsjön", TypeOfCrime = "Oljeutsläpp", DateOfObservation = new DateTime(2025,04,29,0,0,0,DateTimeKind.Local), Observation = "Jag såg en oljefläck på vattnet när jag var där för att fiska", InvestigatorInfo = "Undersökning har gjorts på plats, ingen fläck har hittas", InvestigatorAction = "", InformerName = "Bengt Svensson", InformerPhone = "0432-5152255", StatusId = "S_B", DepartmentId = "D02", EmployeeId = "E202"},
        new() { RefNumber = "2025-45-0003", Place = "Ödehuset", TypeOfCrime = "Skrot", DateOfObservation = new DateTime(2025,05,02,0,0,0,DateTimeKind.Local), Observation ="Anmälaren körde förbi ödehuset och upptäcker ett antal bilar och annat skrot", InvestigatorInfo = "Undersökning har gjorts och bilder har tagits", InvestigatorAction = "", InformerName = "Olle Pettersson", InformerPhone = "0432-5255522", StatusId="S_C", DepartmentId="D01", EmployeeId ="E103"},
        new() { RefNumber = "2025-45-0004", Place = "Restaurang Krögaren", TypeOfCrime = "Buller", DateOfObservation = new DateTime(2025,06,04,0,0,0,DateTimeKind.Local), Observation = "Restaurangen hade för högt ljud på så man inte kunde sova", InvestigatorInfo = "Bullermätning har gjorts. Man håller sig inom riktvärden", InvestigatorAction = "Meddelat restaurangen att tänka på ljudet i fortsättning", InformerName = "Roland Jönsson", InformerPhone = "0432-5322255", StatusId = "S_D", DepartmentId = "D01", EmployeeId = "E102"},
        new() { RefNumber = "2025-45-0005", Place = "Torget", TypeOfCrime = "Klotter", DateOfObservation = new DateTime(2025,07,10,0,0,0,DateTimeKind.Local), Observation = "Samtliga skräpkorgar och bänkar är nedklottrade", InvestigatorInfo = "", InvestigatorAction = "", InformerName = "Peter Svensson", InformerPhone = "0432-5322555", StatusId = "S_A", DepartmentId = "DXX", EmployeeId = "EXXX"}
      }.AsQueryable();
    /**
     * Simulate some departments
     * Added a fictive department "Ej tillsatt" with id "DXX" to be used when no department is assigned
     */
    public IQueryable<Department> Departments =>
      new List<Department>
      {
        new() { DepartmentId = "DXX", DepartmentName = "Ej tillsatt"},
        new() { DepartmentId = "D00", DepartmentName = "Småstads kommun"},
        new() { DepartmentId = "D01", DepartmentName = "IT-avdelningen"},
        new() { DepartmentId = "D02", DepartmentName = "Lek och Skoj"},
        new() { DepartmentId = "D03", DepartmentName = "Miljöskydd"}
      }.AsQueryable();
    /**
     * Simulate some errand statuses
     */
    public IQueryable<ErrandStatus> ErrandStatuses =>
      new List<ErrandStatus>
      {
        new() { StatusId = "S_A", StatusName = "Rapporterad"},
        new() { StatusId = "S_B", StatusName = "Ingen åtgärd"},
        new() { StatusId = "S_C", StatusName = "Startad"},
        new() { StatusId = "S_D", StatusName = "Färdig"}
      }.AsQueryable();
    /**
     * Simulate some employees
     * Added a fictive employee "Ej tillsatt" with id "EXXX" to be used when no employee is assigned
     */
    public IQueryable<Employee> Employees =>
      new List<Employee>
      {
        new() { EmployeeId = "EXXX", EmployeeName = "Ej tillsatt", RoleTitle = "investigator", DepartmentId = "DXX"},
        new() { EmployeeId = "E102", EmployeeName = "Martin Bäck", RoleTitle = "investigator", DepartmentId = "D01"},
        new() { EmployeeId = "E103", EmployeeName = "Lena Kristersson", RoleTitle = "investigator", DepartmentId = "D01"},
        new() { EmployeeId = "E202", EmployeeName = "Oskar Jansson", RoleTitle = "investigator", DepartmentId = "D02"},
        new() { EmployeeId = "E302", EmployeeName = "Susanne Strid", RoleTitle = "investigator", DepartmentId = "D03"}
      }.AsQueryable();
    public IQueryable<Sequence> Sequences =>
      new List<Sequence>
      {
        new() { Id = 1, CurrentValue = 200 }
      }.AsQueryable();

    /**
     * Get single errand with details
     */
    public async Task<ErrandInfo> GetErrandDetail(int errandid)
    {

      Errand? errand = await Errands.FirstOrDefaultAsync(ed => ed.ErrandId == errandid) ?? throw new InvalidOperationException("Errand not found " + errandid);
      /**
       * Database contains only the Ids for Status, Department and Employee in key fields.
       * * lookup the names from the related collections and add to the ViewModel
       */
      var es = await ErrandStatuses.FirstOrDefaultAsync(st => st.StatusId == errand.StatusId);
      string sName = es == null ? "Inte angivet" : es.StatusName!;

      var dep = await Departments.FirstOrDefaultAsync(dep => dep.DepartmentId == errand.DepartmentId);
      string dName = dep == null ? "Inte angivet" : dep.DepartmentName!;

      var emp = await Employees.FirstOrDefaultAsync(emp => emp.EmployeeId == errand.EmployeeId);
      string eName = emp == null ? "Inte angivet" : emp.EmployeeName!;

      ErrandInfo viewModel = new()
      {
        Errands = errand,
        StatusName = sName,
        DepartmentName = dName,
        EmployeeName = eName
      };
      return viewModel;
    }
    /**
     * Get single sequense with details
     */
    public async Task<Sequence> GetSequenceAsync(int seqid)
    {
      return await Sequences.FirstOrDefaultAsync(seq => seq.Id == seqid) ?? throw new InvalidOperationException("Sequence not found " + seqid);
    }
    /**
     * Update: (Not used atm)
     * Update a errand to the repository.
     * Return: True - db insert/update
     *         False - Error
     */
    public async Task<bool> SaveErrandAsync(Errand errand)
    {
      return await Task.FromResult(true);
    }
    /**
     * Create:
     * Insert an new errand
     * Return: New RefNumber or error message
     */
    public async Task<string> SaveNewErrandAsync(Errand errand)
    {
      return await Task.FromResult("Not implemented");
    }
    /**
     * Update: (Not used atm.)
     * Update an existing sequence.
     */
    public async Task<bool> UpdateSequenceAsync(Sequence sequence)
    {
      return await Task.FromResult(true);
    }
  }
}

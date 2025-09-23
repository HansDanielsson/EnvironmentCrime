namespace EnvironmentCrime.Models
{
  public class FakeRepository : IERepository
  {
    public IQueryable<Errand> Errands =>
      new List<Errand>
      {
        new() { ErrandId = "2025-45-0001", Place = "Skogslunden vid Jensens gård", TypeOfCrime = "Sopor", DateOfObservation = new DateTime(2025,04,24,0,0,0,DateTimeKind.Local), Observation = "Anmälaren var på promenad i skogslunden när hon upptäckte soporna", InvestigatorInfo = "Undersökning har gjorts och bland soporna hittades bl.a ett brev till Gösta Olsson", InvestigatorAction = "Brev har skickats till Gösta Olsson om soporna och anmälan har gjorts till polisen 2025-05-01", InformerName = "Ada Bengtsson", InformerPhone = "0432-5545522", StatusId = "S_D", DepartmentId = "D03", EmployeeId ="E302"},
        new() { ErrandId = "2025-45-0002", Place = "Småstadsjön", TypeOfCrime = "Oljeutsläpp", DateOfObservation = new DateTime(2025,04,29,0,0,0,DateTimeKind.Local), Observation = "Jag såg en oljefläck på vattnet när jag var där för att fiska", InvestigatorInfo = "Undersökning har gjorts på plats, ingen fläck har hittas", InvestigatorAction = "", InformerName = "Bengt Svensson", InformerPhone = "0432-5152255", StatusId = "S_B", DepartmentId = "D02", EmployeeId = "E202"},
        new() { ErrandId = "2025-45-0003", Place = "Ödehuset", TypeOfCrime = "Skrot", DateOfObservation = new DateTime(2025,05,02,0,0,0,DateTimeKind.Local), Observation ="Anmälaren körde förbi ödehuset och upptäcker ett antal bilar och annat skrot", InvestigatorInfo = "Undersökning har gjorts och bilder har tagits", InvestigatorAction = "", InformerName = "Olle Pettersson", InformerPhone = "0432-5255522", StatusId="S_C", DepartmentId="D01", EmployeeId ="E103"},
        new() { ErrandId = "2025-45-0004", Place = "Restaurang Krögaren", TypeOfCrime = "Buller", DateOfObservation = new DateTime(2025,06,04,0,0,0,DateTimeKind.Local), Observation = "Restaurangen hade för högt ljud på så man inte kunde sova", InvestigatorInfo = "Bullermätning har gjorts. Man håller sig inom riktvärden", InvestigatorAction = "Meddelat restaurangen att tänka på ljudet i fortsättning", InformerName = "Roland Jönsson", InformerPhone = "0432-5322255", StatusId = "S_D", DepartmentId = "D01", EmployeeId = "E102"},
        new() { ErrandId = "2025-45-0005", Place = "Torget", TypeOfCrime = "Klotter", DateOfObservation = new DateTime(2025,07,10,0,0,0,DateTimeKind.Local), Observation = "Samtliga skräpkorgar och bänkar är nedklottrade", InvestigatorInfo = "", InvestigatorAction = "", InformerName = "Peter Svensson", InformerPhone = "0432-5322555", StatusId = "S_A", DepartmentId = "DXX", EmployeeId = "EXXX"}
      }.AsQueryable();
    public IQueryable<Department> Departments =>
      new List<Department>
      {
        new() { DepartmentId = "DXX", DepartmentName = "Ej tillsatt"},
        new() { DepartmentId = "D00", DepartmentName = "Småstads kommun"},
        new() { DepartmentId = "D01", DepartmentName = "IT-avdelningen"},
        new() { DepartmentId = "D02", DepartmentName = "Lek och Skoj"},
        new() { DepartmentId = "D03", DepartmentName = "Miljöskydd"}
      }.AsQueryable();
    public IQueryable<ErrandStatus> ErrandStatuses =>
      new List<ErrandStatus>
      {
        new() { StatusId = "S_A", StatusName = "Rapporterad"},
        new() { StatusId = "S_B", StatusName = "Ingen åtgärd"},
        new() { StatusId = "S_C", StatusName = "Startad"},
        new() { StatusId = "S_D", StatusName = "Färdig"}
      }.AsQueryable();
    public IQueryable<Employee> Employees =>
      new List<Employee>
      {
        new() { EmployeeId = "EXXX", EmployeeName = "Ej tillsatt", RoleTitle = "investigator", DepartmentId = "DXX"},
        new() { EmployeeId = "E102", EmployeeName = "Martin Bäck", RoleTitle = "investigator", DepartmentId = "D01"},
        new() { EmployeeId = "E103", EmployeeName = "Lena Kristersson", RoleTitle = "investigator", DepartmentId = "D01"},
        new() { EmployeeId = "E202", EmployeeName = "Oskar Jansson", RoleTitle = "investigator", DepartmentId = "D02"},
        new() { EmployeeId = "E302", EmployeeName = "Susanne Strid", RoleTitle = "investigator", DepartmentId = "D03"}
      }.AsQueryable();

    public Task<ErrandInfo> GetErrandDetail(string errandid)
    {
      return Task.Run(() =>
      {
        var errand = Errands.FirstOrDefault(ed => ed.ErrandId == errandid);
        var viewModel = new ErrandInfo
        {
          Errands = errand!,
          /**
           * Database contains only the Ids for Status, Department and Employee in key fields.
           * lookup the names from the related collections and add to the ViewModel
           */
          StatusName = ErrandStatuses.FirstOrDefault(st => st.StatusId == errand!.StatusId)!.StatusName!,
          DepartmentName = Departments.FirstOrDefault(dep => dep.DepartmentId == errand!.DepartmentId)!.DepartmentName!,
          EmployeeName = Employees.FirstOrDefault(emp => emp.EmployeeId == errand!.EmployeeId)!.EmployeeName!
        };
        return viewModel;
      });
    }
    public ErrandInfo GetErrand(string errandid)
    {
      var errand = Errands.FirstOrDefault(ed => ed.ErrandId == errandid);
      var viewModel = new ErrandInfo
      {
        Errands = errand!,
        /**
         * Database contains only the Ids for Status, Department and Employee in key fields.
         * lookup the names from the related collections and add to the ViewModel
         */
        StatusName = ErrandStatuses.FirstOrDefault(st => st.StatusId == errand!.StatusId)!.StatusName!,
        DepartmentName = Departments.FirstOrDefault(dep => dep.DepartmentId == errand!.DepartmentId)!.DepartmentName!,
        EmployeeName = Employees.FirstOrDefault(emp => emp.EmployeeId == errand!.EmployeeId)!.EmployeeName!
      };
      return viewModel;
    }
  }
}

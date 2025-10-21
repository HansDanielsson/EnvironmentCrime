namespace EnvironmentCrime.Models
{
  public class MyErrand
  {
    public required DateTime DateOfObservation { get; set; }
    public int ErrandId { get; set; }
    public required string RefNumber { get; set; }
    public required string TypeOfCrime { get; set; }
    public required string StatusName { get; set; }
    public string? DepartmentName { get; set; }
    public string? EmployeeName { get; set; }
  }
}

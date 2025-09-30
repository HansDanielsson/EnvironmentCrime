using System.ComponentModel.DataAnnotations;

namespace EnvironmentCrime.Models
{
  /**
   * Represents the status of an errand.
   */
  public class ErrandStatus
  {
    [Key]
    public required string StatusId { get; set; }
    public string? StatusName { get; set; }
  }
}

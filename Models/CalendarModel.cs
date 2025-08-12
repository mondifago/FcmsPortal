using System.ComponentModel.DataAnnotations;

namespace FcmsPortal.Models;

public class CalendarModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Calendar name is required.")]
    [StringLength(30, ErrorMessage = "Calendar name must be 30 characters or fewer.")]
    public string Name { get; set; } = string.Empty;
    public List<ScheduleEntry> ScheduleEntries { get; set; } = new List<ScheduleEntry>();
    public int? SchoolId { get; set; }
    public School? School { get; set; }
}
namespace FcmsPortal.Models;

public class CalendarModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<ScheduleEntry> ScheduleEntries { get; set; } = new List<ScheduleEntry>();
}
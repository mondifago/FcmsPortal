namespace FcmsPortal;

public class Calendar
{
    public int Id { get; set; }
    public string Name { get; set; } // e.g., "School Calendar", "Admin Calendar"
    public List<ScheduleEntry> ScheduleEntries { get; set; } = new List<ScheduleEntry>();
}
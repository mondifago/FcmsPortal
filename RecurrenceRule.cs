using FcmsPortal.Enums;

namespace FcmsPortal;

public class RecurrenceRule
{
    public int Id { get; set; }
    public RecurrencePattern Pattern { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<DayOfWeek>? DaysOfWeek { get; set; }  // For weekly patterns
    public int? DayOfMonth { get; set; }  // For monthly patterns
}
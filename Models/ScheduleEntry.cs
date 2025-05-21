using FcmsPortal.Enums;

namespace FcmsPortal.Models
{
    public class ScheduleEntry
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public TimeSpan Duration { get; set; }
        public string Venue { get; set; }
        public ClassSession ClassSession { get; set; }
        public string Title { get; set; }
        public string Event { get; set; }
        public string Meeting { get; set; }
        public bool IsRecurring { get; set; } = false; // Flag for recurrence
        public RecurrenceType? RecurrencePattern { get; set; } // Daily, Weekly, Monthly
        public List<DayOfWeek> DaysOfWeek { get; set; }  // For weekly patterns
        public int? DayOfMonth { get; set; }  // For monthly patterns
        public int RecurrenceInterval { get; set; } = 1; // Interval between recurrences
        public DateTime? EndDate { get; set; } // End date for recurrence


        public ScheduleType GetScheduleType()
        {
            if (ClassSession != null) return ScheduleType.ClassSession;
            if (!string.IsNullOrEmpty(Event)) return ScheduleType.Event;
            return ScheduleType.Meeting;
        }
    }
}

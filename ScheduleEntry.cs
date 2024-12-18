using FcmsPortal.Enums;

namespace FcmsPortal
{
    public class ScheduleEntry
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public TimeSpan Duration { get; set; }
        public string Venue { get; set; }
        public ClassSession? ClassSession { get; set; }
        public string Title { get; set; }
        public string? Event { get; set; }
        public string? Meeting { get; set; }
        public string? Notes { get; set; }
        public ScheduleType GetScheduleType()
        {
            if (ClassSession != null) return ScheduleType.ClassSession;
            if (!string.IsNullOrEmpty(Event)) return ScheduleType.Event;
            if (!string.IsNullOrEmpty(Meeting)) return ScheduleType.Meeting;
            return ScheduleType.General;
        }
    }
}

﻿namespace FcmsPortal
{
    public class ScheduleEntry
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public TimeSpan Duration { get; set; }
        public ClassSession ClassSession { get; set; }
    }
}

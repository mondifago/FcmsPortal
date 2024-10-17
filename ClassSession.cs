﻿namespace FcmsPortal
{
    public class ClassSession
    {
        public int ID;
        public string Course { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public Staff Teacher { get; set; }
        public List<ClassAttendanceLogEntry> AttendanceLog { get; set; } = new List<ClassAttendanceLogEntry>();
        public Grade Grade { get; set; }
    }


}

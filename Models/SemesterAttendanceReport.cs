namespace FcmsPortal.Models
{
    public class SemesterAttendanceReport
    {
        public int LearningPathId { get; set; }
        public string LearningPathName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<DateTime> AttendanceDates { get; set; } = new();
        public List<StudentSemesterAttendance> Students { get; set; } = new();
    }
    public class StudentSemesterAttendance
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public Dictionary<DateTime, bool> AttendanceByDate { get; set; } = new();
        public int TotalPresentDays => AttendanceByDate.Values.Count(s => s);
        public int TotalAbsentDays => AttendanceByDate.Values.Count(s => !s);
        public double AttendanceRate => AttendanceByDate.Count > 0 ?
            (double)TotalPresentDays / AttendanceByDate.Count * 100 : 0;
    }
}

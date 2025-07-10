namespace FcmsPortal.Models
{
    public class SemesterAttendanceReport
    {
        public int LearningPathId { get; set; }
        public string LearningPathName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<DateTime> AttendanceDates { get; set; } = new();
        public List<StudentSemesterAttendance> Students { get; set; } = new();
    }
}

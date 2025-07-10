using FcmsPortal.Constants;

namespace FcmsPortal.Models
{
    public class StudentSemesterAttendance
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public Dictionary<DateTime, bool> AttendanceByDate { get; set; } = new();
        public int TotalPresentDays => AttendanceByDate.Values.Count(s => s);
        public int TotalAbsentDays => AttendanceByDate.Values.Count(s => !s);
        public double AttendanceRate => AttendanceByDate.Count > 0 ?
            (double)TotalPresentDays / AttendanceByDate.Count * FcmsConstants.PERCENTAGE_MULTIPLIER : 0;

    }
}

namespace FcmsPortal.Models
{
    public class LearningPathPaymentReportEntry
    {
        public string AcademicYear { get; set; } = string.Empty;
        public string Semester { get; set; } = string.Empty;
        public string LearningPathName { get; set; } = string.Empty;
        public DateTime SemesterStartDate { get; set; }
        public DateTime SemesterEndDate { get; set; }
        public DateTime ReportGeneratedDateAndTime { get; set; }
        public int TotalStudentsInPath { get; set; }
        public double TotalFeesForPath { get; set; }
        public double TotalPaidForPath { get; set; }
        public double OutstandingForPath { get; set; }
        public double LearningPathPaymentCompletionRate { get; set; }
        public double AverageStudentPaymentCompletionRateInPath { get; set; }
        public double LearningPathTimelyCompletionRateInPath { get; set; }
        public double AverageStudentTimelyCompletionRate { get; set; }
    }
}

using FcmsPortal.Enums;

namespace FcmsPortal.Models
{
    public class ArchivedStudentPayment
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public int LearningPathId { get; set; }
        public string LearningPathName { get; set; } = string.Empty;
        public EducationLevel EducationLevel { get; set; }
        public ClassLevel ClassLevel { get; set; }
        public Semester Semester { get; set; }
        public string AcademicYear { get; set; } = string.Empty;
        public double TotalFees { get; set; }
        public double TotalPaid { get; set; }
        public double OutstandingBalance { get; set; }
        public double PaymentCompletionRate { get; set; }
        public double TimelyCompletionRate { get; set; }
        public string PaymentDetailsJson { get; set; } = string.Empty;
        public DateTime ArchivedDate { get; set; }
    }
}

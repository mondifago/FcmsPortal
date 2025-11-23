using FcmsPortal.Enums;

namespace FcmsPortal.Models
{
    public class ArchivedSchoolPaymentSummary
    {
        public int Id { get; set; }
        public string AcademicYear { get; set; } = string.Empty;
        public Semester Semester { get; set; }

        // Aggregated Financial Data
        public int TotalLearningPaths { get; set; }
        public int TotalStudents { get; set; }
        public int FullyPaidStudents { get; set; }
        public int StudentsWithBalance { get; set; }
        public double TotalExpectedRevenue { get; set; }
        public double TotalAmountReceived { get; set; }
        public double TotalOutstandingBalance { get; set; }

        // School-wide Analytics
        public double SchoolWidePaymentCompletionRate { get; set; }
        public double SchoolWideTimelyCompletionRate { get; set; }
        public double AverageStudentPaymentCompletionRateInSchool { get; set; }
        public double AverageStudentTimelyCompletionRateInSchool { get; set; }

        // Metadata
        public DateTime SemesterStartDate { get; set; }
        public DateTime SemesterEndDate { get; set; }
        public DateTime ArchivedDate { get; set; }
    }
}

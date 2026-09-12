using FcmsPortal.Enums;

namespace FcmsPortal.Models
{
    public class ArchivedSchoolPaymentSummary
    {
        public int Id { get; set; }
        public string AcademicYear { get; set; } = string.Empty;
        public Semester Semester { get; set; }
        public int TotalLearningPaths { get; set; }
        public int TotalStudents { get; set; }
        public int FullyPaidStudents { get; set; }
        public int StudentsWithBalance { get; set; }
        public double TotalExpectedRevenue { get; set; }
        public double TotalAmountReceived { get; set; }
        public double TotalOutstandingBalance { get; set; }
        public double TotalBroughtForwardOutstanding { get; set; }
        public double SchoolWidePaymentCompletionRate { get; set; }
        public double SchoolWideTimelyCompletionRate { get; set; }
        public DateTime SemesterStartDate { get; set; }
        public DateTime SemesterEndDate { get; set; }
        public DateTime ArchivedDate { get; set; }
    }
}

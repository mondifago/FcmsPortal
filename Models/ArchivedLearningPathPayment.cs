using FcmsPortal.Enums;

namespace FcmsPortal.Models
{
    public class ArchivedLearningPathPayment
    {
        public int Id { get; set; }
        public int LearningPathId { get; set; }
        public string LearningPathName { get; set; } = string.Empty;
        public EducationLevel EducationLevel { get; set; }
        public ClassLevel ClassLevel { get; set; }
        public Semester Semester { get; set; }
        public string AcademicYear { get; set; } = string.Empty;

        // Financial Data
        public int TotalStudentsInPath { get; set; }
        public double FeePerStudent { get; set; }
        public double LearningPathExpectedRevenue { get; set; }
        public double TotalPaid { get; set; }
        public double Outstanding { get; set; }

        // Analytics/Rates
        public double LearningPathPaymentCompletionRate { get; set; }
        public double AverageStudentPaymentCompletionRateInPath { get; set; }
        public double LearningPathTimelyCompletionRate { get; set; }
        public double AverageStudentTimelyCompletionRateInPath { get; set; }

        // Metadata
        public DateTime SemesterStartDate { get; set; }
        public DateTime SemesterEndDate { get; set; }
        public DateTime ArchivedDate { get; set; }
    }
}

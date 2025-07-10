namespace FcmsPortal.Models
{
    public class SchoolPaymentReportEntry
    {
        public string AcademicYear { get; set; } = string.Empty;
        public string Semester { get; set; } = string.Empty;
        public DateTime SemesterStartDate { get; set; }
        public DateTime SemesterEndDate { get; set; }
        public DateTime DateAndTimeReportGenerated { get; set; }
        public int TotalStudents { get; set; }
        public double TotalSchoolFeesAmount { get; set; }
        public double TotalAmountPaid { get; set; }
        public double TotalOutstanding { get; set; }
        public double SchoolPaymentCompletionRate { get; set; }
        public double AverageStudentPaymentCompletionRateInSchool { get; set; }
        public double AverageStudentTimelyCompletionRate { get; set; }

    }
}

namespace FcmsPortal.Models
{
    public class SchoolPaymentSummary
    {
        public int TotalLearningPaths { get; set; }
        public int TotalStudents { get; set; }

        public int FullyPaidStudents { get; set; }
        public int StudentsWithBalance { get; set; }

        public double TotalExpectedRevenue { get; set; }
        public double TotalAmountReceived { get; set; }
        public double TotalOutstanding { get; set; }

        public double PaymentCompletionRate { get; set; }
        public double TimelyCompletionRate { get; set; }
    }
}

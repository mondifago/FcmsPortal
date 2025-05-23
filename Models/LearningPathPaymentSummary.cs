namespace FcmsPortal.Models
{
    public class LearningPathPaymentSummary
    {
        public double ExpectedRevenue { get; set; }
        public double TotalPaid { get; set; }
        public double Outstanding { get; set; }
        public double PaymentCompletionRate { get; set; }
        public double TimelyCompletionRate { get; set; }
        public DateTime? LastPaymentDate { get; set; }
        public int StudentCount { get; set; }
        public double FeePerSemester { get; set; }
    }
}

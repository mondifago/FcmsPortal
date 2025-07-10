namespace FcmsPortal.Models;

public class StudentPaymentReportEntry
{
    public DateTime DateAndTimeReportGenerated { get; set; }
    public string StudentFullName { get; set; } = string.Empty;
    public string StudentAddress { get; set; } = string.Empty;
    public string LearningPathName { get; set; } = string.Empty;
    public double TotalFees { get; set; }
    public double TotalPaid { get; set; }
    public double OutstandingBalance { get; set; }
    public List<PaymentDetails> PaymentDetails { get; set; } = new();
    public string AcademicYear { get; set; } = string.Empty;
    public string Semester { get; set; } = string.Empty;
    public double StudentTimelyCompletionRate { get; set; }
    public double StudentPaymentCompletionRate { get; set; }
}
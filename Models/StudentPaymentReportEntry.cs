namespace FcmsPortal.Models;

public class StudentPaymentReportEntry
{
    public DateTime DateAndTimeReportGenerated { get; set; }
    public string StudentFullName { get; set; }
    public string StudentAddress { get; set; }
    public string LearningPathName { get; set; }
    public double TotalFees { get; set; }
    public double TotalPaid { get; set; }
    public double OutstandingBalance { get; set; }
    public List<PaymentDetails> PaymentDetails { get; set; }
    public string AcademicYear { get; set; }
    public string Semester { get; set; }
    public double StudentTimelyCompletionRate { get; set; }
    public double StudentPaymentCompletionRate { get; set; }
}
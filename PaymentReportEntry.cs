namespace FcmsPortal;

public class PaymentReportEntry
{
    public string StudentName { get; set; }
    public double TotalFees { get; set; }
    public double TotalPaid { get; set; }
    public double OutstandingBalance { get; set; }
    public List<PaymentDetails> PaymentDetails { get; set; }
}
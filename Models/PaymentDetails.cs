namespace FcmsPortal.Models;

public class PaymentDetails
{
    public DateTime Date { get; set; }
    public double Amount { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public int Reference { get; set; }
}
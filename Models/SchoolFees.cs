namespace FcmsPortal.Models
{
    public class SchoolFees
    {
        public double TotalAmount { get; set; }
        public double Balance => TotalAmount - TotalPaid;
        public double TotalPaid => Payments.Sum(payment => payment.Amount);
        public List<Payment> Payments { get; set; } = new List<Payment>();
    }
}

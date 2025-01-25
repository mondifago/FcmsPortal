using FcmsPortal.Enums;

namespace FcmsPortal
{
    public class Payment
    {
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public int Reference { get; set; }
    }
}

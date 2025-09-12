using FcmsPortal.Enums;

namespace FcmsPortal.Models
{
    public class ArchivedPaymentDetail
    {
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public int Reference { get; set; }
    }
}

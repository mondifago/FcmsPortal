using FcmsPortal.Enums;

namespace FcmsPortal.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public int Reference { get; set; }
    }
}

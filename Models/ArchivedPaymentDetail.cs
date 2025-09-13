using FcmsPortal.Enums;

namespace FcmsPortal.Models
{
    public class ArchivedPaymentDetail
    {
        public int Id { get; set; }
        public int ArchivedStudentPaymentId { get; set; }
        public ArchivedStudentPayment ArchivedStudentPayment { get; set; } = null!;
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string Reference { get; set; } = string.Empty;
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace FcmsPortal.Models
{
    public class SchoolFees
    {
        public int Id { get; set; }
        public int PersonId { get; set; }

        [ForeignKey(nameof(PersonId))]
        public Person Person { get; set; } = null!;
        public double TotalAmount { get; set; }
        public double Balance => TotalAmount - TotalPaid;
        public double TotalPaid => Payments.Sum(payment => payment.Amount);
        public List<Payment> Payments { get; set; } = new List<Payment>();
    }
}
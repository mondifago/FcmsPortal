using System.ComponentModel.DataAnnotations.Schema;

namespace FcmsPortal.Models
{
    public class SchoolFees
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int LearningPathId { get; set; }
        public LearningPath LearningPath { get; set; } = null!;
        public List<Payment> Payments { get; set; } = new List<Payment>();
        [NotMapped] public double TotalAmount => LearningPath?.FeePerSemester ?? 0;
        [NotMapped] public double TotalPaid => Payments.Sum(payment => payment.Amount);
        [NotMapped] public double Balance => TotalAmount - TotalPaid;
    }
}
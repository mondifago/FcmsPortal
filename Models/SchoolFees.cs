using FcmsPortal.Enums;
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
        public List<FeeAdjustment> Adjustments { get; set; } = new();
        [NotMapped]
        public double TotalAdjustments => Adjustments.Sum(ResolveAdjustment);

        [NotMapped]
        public double TotalAmount => (LearningPath?.FeePerSemester ?? 0) - TotalAdjustments;

        public double ResolveAdjustment(FeeAdjustment adjustment)
        {
            return adjustment.Mode == FeeAdjustmentMode.Percentage
                ? (LearningPath?.FeePerSemester ?? 0) * adjustment.Value / 100
                : adjustment.Value;
        }
        [NotMapped] public double TotalPaid => Payments.Sum(payment => payment.Amount);
        [NotMapped] public double Balance => TotalAmount - TotalPaid;
    }
}
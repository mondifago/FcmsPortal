using FcmsPortal.Constants;
using FcmsPortal.Enums;
using System.ComponentModel.DataAnnotations;

namespace FcmsPortal.Models
{
    public class FeeAdjustment
    {
        public int Id { get; set; }

        public int SchoolFeesId { get; set; }
        public SchoolFees SchoolFees { get; set; } = null!;

        public FeeAdjustmentType Type { get; set; }

        public double? Amount { get; set; }
        public double? Percentage { get; set; }

        public DateTime Date { get; set; }

        [StringLength(FcmsConstants.MAX_ADJUSTMENT_REASON_LENGTH)]
        public string Reason { get; set; } = string.Empty;

        public int? AuthorizedById { get; set; }
    }
}

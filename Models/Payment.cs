using FcmsPortal.Enums;
using System.ComponentModel.DataAnnotations;

namespace FcmsPortal.Models
{
    public class Payment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        public double Amount { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        public DateTime Date { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        [Required(ErrorMessage = "Reference is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Reference must be a positive number.")]
        public int Reference { get; set; }

        public int SchoolFeesId { get; set; }
    }
}
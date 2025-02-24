using FcmsPortal.Enums;
using System.ComponentModel.DataAnnotations;

namespace FcmsPortal.Models
{
    public class Address
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Street is required.")]
        public string Street { get; set; } = string.Empty;

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; } = string.Empty;

        public string? State { get; set; }

        [StringLength(10, MinimumLength = 5, ErrorMessage = "Postal Code must be between 5 and 10 characters.")]
        public string? PostalCode { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        public string Country { get; set; } = string.Empty;

        public AddressType AddressType { get; set; }
    }
}
using FcmsPortal.Enums;
using System.ComponentModel.DataAnnotations;

namespace FcmsPortal.Models
{
    public class Address
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Street is required.")]
        [RegularExpression(@"^[A-Za-z0-9 .,-]+$", ErrorMessage = "Postal Address can only contain letters, numbers, spaces, periods, commas and dashes.")]
        public string Street { get; set; } = string.Empty;

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "State is required.")]
        public string State { get; set; } = string.Empty;

        [StringLength(30, MinimumLength = 4, ErrorMessage = "Postal Code must be between 4 and 30 characters.")]
        public string? PostalCode { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        public string Country { get; set; } = string.Empty;

        public AddressType AddressType { get; set; }
    }
}
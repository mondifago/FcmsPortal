using FcmsPortal.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FcmsPortal.Models
{
    public class Person
    {
        public int Id { get; set; }
        [Url(ErrorMessage = "Invalid URL format.")]
        public string? ProfilePictureUrl { get; set; }
        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(30, MinimumLength = 2)]
        public string FirstName { get; set; } = string.Empty;
        [StringLength(30)]
        public string? MiddleName { get; set; }
        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(30, MinimumLength = 2)]
        public string LastName { get; set; } = string.Empty;
        public Gender Sex { get; set; }
        [Required]
        [Column(TypeName = "date")]
        public DateTime DateOfEnrollment { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [NotMapped]
        public int Age
        {
            get
            {
                var today = DateTime.Today;
                int age = today.Year - DateOfBirth.Year;
                if (today < DateOfBirth.AddYears(age))
                {
                    age--;
                }
                return age;
            }
        }

        [Required]
        [StringLength(50, ErrorMessage = "State of Origin cannot exceed 50 characters.")]
        public string StateOfOrigin { get; set; } = string.Empty;
        [Required]
        [StringLength(50, ErrorMessage = "LGA of Origin cannot exceed 50 characters.")]
        public string LgaOfOrigin { get; set; } = string.Empty;
        [Required]
        public List<Address> Addresses { get; set; } = new List<Address>();
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;
        public CalendarModel PersonalCalendar { get; set; } = new CalendarModel();
        public EducationLevel EducationLevel { get; set; }
        public ClassLevel ClassLevel { get; set; }
        public SchoolFees? SchoolFees { get; set; }
        [StringLength(100, ErrorMessage = "Emergency contact details cannot exceed 100 characters.")]
        [Required(ErrorMessage = "Emergency contact number is required")]
        [Phone(ErrorMessage = "Please enter a valid phone number")]
        public string EmergencyContact { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}

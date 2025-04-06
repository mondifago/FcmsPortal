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
        public Calendar PersonalCalendar { get; set; } = new Calendar();
        public EducationLevel EducationLevel { get; set; }

        public ClassLevel ClassLevel { get; set; }
        /*{
            get => _classLevel;
            set
            {
                if (!IsValidClassLevelForEducationLevel(EducationLevel, value))
                {
                    throw new ArgumentException("Invalid ClassLevel for the current EducationLevel.");
                }
                _classLevel = value;
            }
        }*/
        public SchoolFees? SchoolFees { get; set; }
        [StringLength(100, ErrorMessage = "Emergency contact details cannot exceed 100 characters.")]
        [Required(ErrorMessage = "Emergency contact number is required")]
        [Phone(ErrorMessage = "Please enter a valid phone number")]
        public string EmergencyContact { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        /*private ClassLevel _classLevel;
        private bool IsValidClassLevelForEducationLevel(EducationLevel educationLevel, ClassLevel classLevel)
        {
            return educationLevel switch
            {
                EducationLevel.Kindergarten => classLevel is ClassLevel.KG_Daycare or ClassLevel.KG_PlayGroup or ClassLevel.KG_PreNursery or ClassLevel.KG_Nursery,
                EducationLevel.Primary => classLevel is ClassLevel.PRI_1 or ClassLevel.PRI_2 or ClassLevel.PRI_3 or ClassLevel.PRI_4 or ClassLevel.PRI_5 or ClassLevel.PRI_6,
                EducationLevel.JuniorCollege => classLevel is ClassLevel.JC_1 or ClassLevel.JC_2 or ClassLevel.JC_3,
                EducationLevel.SeniorCollege => classLevel is ClassLevel.SC_1 or ClassLevel.SC_2 or ClassLevel.SC_3,
                _ => false
            };
        }*/
    }
}

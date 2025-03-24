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
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First Name must be between 2 and 50 characters.")]
        public string FirstName { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "Middle Name cannot exceed 50 characters.")]
        public string? MiddleName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last Name must be between 2 and 50 characters.")]
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

        [Required(ErrorMessage = "State of Origin is required.")]
        [StringLength(50, ErrorMessage = "State of Origin cannot exceed 50 characters.")]
        public string StateOfOrigin { get; set; } = string.Empty;

        [Required(ErrorMessage = "LGA of Origin is required.")]
        [StringLength(50, ErrorMessage = "LGA of Origin cannot exceed 50 characters.")]
        public string LgaOfOrigin { get; set; } = string.Empty;
        [Required]
        public List<Address> Addresses { get; set; } = new List<Address>();

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; } = string.Empty;


        public Calendar PersonalCalendar { get; set; } = new Calendar();

        public EducationLevel EducationLevel { get; set; }

        public ClassLevel ClassLevel
        {
            get => _classLevel;
            set
            {
                if (!IsValidClassLevelForEducationLevel(EducationLevel, value))
                {
                    throw new ArgumentException("Invalid ClassLevel for the current EducationLevel.");
                }
                _classLevel = value;
            }
        }

        public SchoolFees? SchoolFees { get; set; }
        [StringLength(100, ErrorMessage = "Emergency contact details cannot exceed 100 characters.")]
        public string? EmergencyContact { get; set; }
        public bool IsActive { get; set; }
        private ClassLevel _classLevel;

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
        }
    }
}

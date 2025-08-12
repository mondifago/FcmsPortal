using System.ComponentModel.DataAnnotations;

namespace FcmsPortal.Models
{
    public class School
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "School name is required.")]
        [StringLength(100, ErrorMessage = "Name must be 100 characters or fewer.")]
        public string Name { get; set; } = string.Empty;

        public string? LogoUrl { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Website URL is required.")]
        [Url(ErrorMessage = "Invalid URL format.")]
        public string WebsiteUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = "School address is required.")]
        public int AddressId { get; set; }
        public Address Address { get; set; } = null!;
        public List<Staff> Staff { get; set; } = new();
        public List<Student> Students { get; set; } = new();
        public List<Guardian> Guardians { get; set; } = new();
        public List<LearningPath> LearningPaths { get; set; } = new();

        [Required]
        public List<CalendarModel> SchoolCalendar { get; set; } = new();
    }
}

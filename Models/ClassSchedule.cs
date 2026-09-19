using FcmsPortal.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FcmsPortal.Models
{
    public class ClassSchedule
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Class level is required.")]
        public ClassLevel ClassLevel { get; set; }

        [Required(ErrorMessage = "Term is required.")]
        public Semester Semester { get; set; }

        public DateTime DateTime { get; set; }

        public TimeSpan Duration { get; set; }

        [Required(ErrorMessage = "Venue is required.")]
        [StringLength(50, ErrorMessage = "Venue must be 50 characters or fewer.")]
        public string Venue { get; set; } = string.Empty;

        public int? ClassSessionId { get; set; }

        [ForeignKey(nameof(ClassSessionId))]
        public ClassSession? ClassSession { get; set; }
    }
}

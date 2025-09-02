using FcmsPortal.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FcmsPortal.Models
{
    public class ScheduleEntry
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Date and time are required.")]
        public DateTime DateTime { get; set; }

        [Required(ErrorMessage = "Duration is required.")]
        public TimeSpan Duration { get; set; }

        [Required(ErrorMessage = "Venue is required.")]
        [StringLength(50, ErrorMessage = "Venue must be 50 characters or fewer.")]
        public string Venue { get; set; } = string.Empty;
        public int? ClassSessionId { get; set; }
        [ForeignKey("ClassSessionId")]
        public ClassSession? ClassSession { get; set; }
        public int? LearningPathId { get; set; }
        [ForeignKey("LearningPathId")]
        public LearningPath? LearningPath { get; set; }

        [StringLength(50, ErrorMessage = "Title must be 50 characters or fewer.")]
        public string Title { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "Event name must be 50 characters or fewer.")]
        public string? Event { get; set; }

        [StringLength(50, ErrorMessage = "Meeting name must be 50 characters or fewer.")]
        public string? Meeting { get; set; }

        public bool IsRecurring { get; set; } = false;

        public RecurrenceType? RecurrencePattern { get; set; }

        public List<DayOfWeek> DaysOfWeek { get; set; } = new();

        public int? DayOfMonth { get; set; }

        [Range(1, 365, ErrorMessage = "Recurrence interval must be at least 1.")]
        public int RecurrenceInterval { get; set; } = 1;

        public DateTime? EndDate { get; set; }
    }
}

using FcmsPortal.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FcmsPortal.Models
{
    public class AcademicPeriod
    {
        public int Id { get; set; }

        [Required]
        public int SchoolId { get; set; }
        public School School { get; set; } = null!;

        [Required]
        public DateTime AcademicYearStart { get; set; }

        [Required]
        public Semester Semester { get; set; }

        [Required]
        public DateTime SemesterStartDate { get; set; }

        [Required]
        public DateTime SemesterEndDate { get; set; }

        public DateTime? ExamsStartDate { get; set; }

        public bool IsActive { get; set; } = false;
        public bool IsArchived { get; set; } = false;

        [NotMapped]
        public string AcademicYear => $"{AcademicYearStart.Year}-{AcademicYearStart.Year + 1}";

        [NotMapped]
        public string DisplayName => $"{AcademicYear} - {Semester} Semester";

        public List<LearningPath> LearningPaths { get; set; } = new();
    }
}

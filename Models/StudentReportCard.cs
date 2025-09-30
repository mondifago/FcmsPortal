using System.ComponentModel.DataAnnotations;

namespace FcmsPortal.Models
{
    public class StudentReportCard
    {
        public int Id { get; set; }
        [Required]
        public int StudentId { get; set; }
        public Student? Student { get; set; }
        [Required]
        public int LearningPathId { get; set; }
        public LearningPath? LearningPath { get; set; }
        public string TeacherRemarks { get; set; } = string.Empty;
        public string PrincipalRemarks { get; set; } = string.Empty;
        public double SemesterOverallGrade { get; set; }
        public double PromotionGrade { get; set; }
        public int StudentRank { get; set; }
        public int PresentDays { get; set; }
        public int TotalDays { get; set; }
        public double AttendanceRate { get; set; }
        public bool IsPromoted { get; set; }
        public string PromotionStatus { get; set; } = string.Empty;
        public DateTime DateGenerated { get; set; } = DateTime.Now;
        public DateTime? DateFinalized { get; set; }
        public bool IsFinalized { get; set; } = false;
        public int? GeneratedByTeacherId { get; set; }
        public Staff? GeneratedByTeacher { get; set; }
        public int? FinalizedByPrincipalId { get; set; }
        public Staff? FinalizedByPrincipal { get; set; }
    }
}

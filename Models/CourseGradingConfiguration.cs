using FcmsPortal.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FcmsPortal.Models
{
    public class CourseGradingConfiguration
    {
        public int Id { get; set; }
        public string Course { get; set; } = string.Empty;
        public int LearningPathId { get; set; }
        public EducationLevel EducationLevel { get; set; }
        public ClassLevel ClassLevel { get; set; }
        public Semester Semester { get; set; }
        public DateTime AcademicYearStart { get; set; }
        [NotMapped]
        public string AcademicYear => $"{AcademicYearStart.Year}-{AcademicYearStart.Year + 1}";

        [Range(0, 100, ErrorMessage = "Homework weight must be between 0 and 100")]
        public double HomeworkWeightPercentage { get; set; }

        [Range(0, 100, ErrorMessage = "Quiz weight must be between 0 and 100")]
        public double QuizWeightPercentage { get; set; }

        [Range(0, 100, ErrorMessage = "Exam weight must be between 0 and 100")]
        public double FinalExamWeightPercentage { get; set; }
    }
}

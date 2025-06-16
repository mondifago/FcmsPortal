using System.ComponentModel.DataAnnotations;

namespace FcmsPortal.Models
{
    public class CourseGradingConfiguration
    {
        public int Id { get; set; }
        public string Course { get; set; }
        public int LearningPathId { get; set; }
        [Range(0, 100, ErrorMessage = "Homework weight must be between 0 and 100")]
        public double HomeworkWeightPercentage { get; set; }

        [Range(0, 100, ErrorMessage = "Quiz weight must be between 0 and 100")]
        public double QuizWeightPercentage { get; set; }

        [Range(0, 100, ErrorMessage = "Exam weight must be between 0 and 100")]
        public double FinalExamWeightPercentage { get; set; }
        public LearningPath LearningPath { get; set; }
    }
}

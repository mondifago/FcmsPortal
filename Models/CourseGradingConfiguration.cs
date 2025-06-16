namespace FcmsPortal.Models
{
    public class CourseGradingConfiguration
    {
        public int Id { get; set; }
        public string Course { get; set; }
        public int LearningPathId { get; set; }
        public double HomeworkWeightPercentage { get; set; }
        public double QuizWeightPercentage { get; set; }
        public double FinalExamWeightPercentage { get; set; }
        public LearningPath LearningPath { get; set; }
    }
}

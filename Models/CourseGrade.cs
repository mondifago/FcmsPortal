namespace FcmsPortal.Models
{
    public class CourseGrade
    {
        public int Id { get; set; }
        public string Course { get; set; }
        public List<TestGrade> TestGrades { get; set; } = new();
        public CourseGradingConfiguration GradingConfiguration { get; set; }
        public double TotalGrade { get; set; }
        public string FinalGradeCode { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int LearningPathId { get; set; }
        public LearningPath LearningPath { get; set; }
        public bool IsFinalized { get; set; } = false;
    }
}

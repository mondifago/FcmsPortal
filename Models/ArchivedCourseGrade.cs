namespace FcmsPortal.Models
{
    public class ArchivedCourseGrade
    {
        public int Id { get; set; }
        public string Course { get; set; } = string.Empty;
        public double HomeworkWeightPercentage { get; set; }
        public double QuizWeightPercentage { get; set; }
        public double FinalExamWeightPercentage { get; set; }
        public double CumulatedHomeworkGrade { get; set; }
        public double CumulatedQuizGrade { get; set; }
        public double CumulatedExamGrade { get; set; }
        public double TotalGrade { get; set; }
        public string FinalGradeCode { get; set; } = string.Empty;
        public List<ArchivedTestGrade> TestGrades { get; set; } = new();
        public int ArchivedSemesterGradeId { get; set; }
    }
}

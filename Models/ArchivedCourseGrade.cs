namespace FcmsPortal.Models
{
    public class ArchivedCourseGrade
    {
        public int Id { get; set; }
        public int ArchivedStudentGradeId { get; set; }
        public ArchivedStudentGrade? ArchivedStudentGrade { get; set; }
        public string Course { get; set; } = string.Empty;
        public double TotalGrade { get; set; }
        public string FinalGradeCode { get; set; } = string.Empty;
        public double HomeworkWeightPercentage { get; set; }
        public double QuizWeightPercentage { get; set; }
        public double FinalExamWeightPercentage { get; set; }
        public double HomeworkAverage { get; set; }
        public double QuizAverage { get; set; }
        public double ExamAverage { get; set; }
        public List<ArchivedTestGrade> TestGrades { get; set; } = new List<ArchivedTestGrade>();
    }
}

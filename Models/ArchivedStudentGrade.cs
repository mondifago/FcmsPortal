namespace FcmsPortal.Models
{
    public class ArchivedStudentGrade
    {
        public int Id { get; set; }
        public int ArchivedLearningPathGradeId { get; set; }
        public ArchivedLearningPathGrade? ArchivedLearningPathGrade { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public double SemesterOverallGrade { get; set; }
        public int StudentRank { get; set; }
        public double PromotionGrade { get; set; }
        public int PresentDays { get; set; }
        public int TotalDays { get; set; }
        public double AttendanceRate { get; set; }
        public bool IsPromoted { get; set; }
        public string PromotionStatus { get; set; } = string.Empty;
        public List<ArchivedCourseGrade> CourseGrades { get; set; } = new List<ArchivedCourseGrade>();
        public StudentReportCard? ArchivedReportCard { get; set; }

    }
}

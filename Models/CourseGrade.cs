namespace FcmsPortal.Models
{
    public class CourseGrade
    {
        public int Id { get; set; }
        public string Course { get; set; }
        public List<TestGrade> TestGrades { get; set; } = new();
        public double? AttendancePercentage { get; set; }
        public double TotalGrade { get; set; }
        public string FinalGradeCode { get; set; }
        public List<FileAttachment> FinalResultAttachments { get; set; } = new();
    }
}

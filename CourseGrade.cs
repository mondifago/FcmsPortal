namespace FcmsPortal
{
    public class CourseGrade
    {
        public List<TestGrade> TestGrades { get; set; } = new();
        public double? AttendancePercentage { get; set; }
        public double TotalGrade { get; set; }
        public string FinalGradeCode { get; set; }
        public List<FileAttachment> FinalResultAttachments { get; set; } = new();
    }
}

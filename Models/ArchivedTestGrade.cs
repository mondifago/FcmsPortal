namespace FcmsPortal.Models
{
    public class ArchivedTestGrade
    {
        public int Id { get; set; }

        public string TestType { get; set; } = string.Empty;
        public double Score { get; set; }
        public double MaxScore { get; set; }
        public DateTime DateRecorded { get; set; }
        public int ArchivedCourseGradeId { get; set; }
    }
}

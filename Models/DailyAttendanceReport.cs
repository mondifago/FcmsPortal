namespace FcmsPortal.Models
{
    public class DailyAttendanceReport
    {
        public DateTime Date { get; set; }
        public int LearningPathId { get; set; }
        public string LearningPathName { get; set; }
        public List<Student> PresentStudents { get; set; } = new();
        public List<Student> AbsentStudents { get; set; } = new();
    }
}

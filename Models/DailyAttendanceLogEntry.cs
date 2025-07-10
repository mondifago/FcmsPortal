namespace FcmsPortal.Models
{
    public class DailyAttendanceLogEntry
    {
        public int Id { get; set; }
        public int LearningPathId { get; set; }
        public LearningPath? LearningPath { get; set; }
        public int TeacherId { get; set; }
        public Staff? Teacher { get; set; }
        public List<Student> PresentStudents { get; set; } = new();
        public List<Student> AbsentStudents { get; set; } = new();
        public DateTime TimeStamp { get; set; }
    }
}
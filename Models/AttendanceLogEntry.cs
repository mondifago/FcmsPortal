namespace FcmsPortal.Models
{
    public class AttendanceLogEntry
    {
        public int Id { get; set; }
        public int learningPathId { get; set; }
        public LearningPath learningPath { get; set; }
        public int TeacherId { get; set; }
        public Staff Teacher { get; set; }
        public List<Student> Attendees { get; set; } = new();
        public List<Student> AbsentStudents { get; set; } = new();
        public DateTime TimeStamp { get; set; }
    }
}
namespace FcmsPortal
{
    public class ClassSession
    {
        public int Id { get; set; }
        public string Course { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public string LessonNote { get; set; }
        public Staff Teacher { get; set; }
        public List<ClassAttendanceLogEntry> AttendanceLog { get; set; } = new List<ClassAttendanceLogEntry>();
        public Homework HomeworkDetails { get; set; }
        public string TeacherRemarks { get; set; }
    }
}

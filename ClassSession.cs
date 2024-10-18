namespace FcmsPortal
{
    public class ClassSession
    {
        public int ID;
        public string Course { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public string LessonNote { get; set; }
        public string HomeWork { get; set; }
        public Staff Teacher { get; set; }
        public List<ClassAttendanceLogEntry> AttendanceLog { get; set; } = new List<ClassAttendanceLogEntry>();
        public CourseGrade Grade { get; set; }
        public string TeacherRemarks { get; set; }
    }
}

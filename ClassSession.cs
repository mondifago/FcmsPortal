namespace FcmsPortal
{
    public class ClassSession
    {
        public int ID;
        public Course Course { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public Staff Teacher { get; set; }
        public List<Student> Students { get; set; } = new List<Student>();
        public List<ClassAttendanceLogEntry> AttendanceLog { get; set; } = new List<ClassAttendanceLogEntry>();
        public Grade Grade { get; set; }
    }


}

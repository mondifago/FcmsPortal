namespace FcmsPortal
{
    public class ClassSession
    {
        public int ID;
        public Course Course { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public EducationLevel EducationLevel { get; set; }
        public ClassLevel ClassLevel { get; set; }
        public Semester Semester { get; set; }
        public Staff Teacher { get; set; }
        public List<Student> Students { get; set; } = new List<Student>();
        public TimeSpan Duration { get; set; }
        public List<ClassAttendanceLogEntry> AttendanceLog { get; set; } = new List<ClassAttendanceLogEntry>();
        public Grade Grade { get; set; }
    }


}

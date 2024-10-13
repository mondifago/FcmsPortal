namespace FcmsPortal
{
    public class ClassSession
    {
        public int ID;
        public Course Name { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public EducationLevel EducationLevel { get; set; }
        public ClassLevel ClassLevel { get; set; }
        public int Semester { get; set; }
        public Staff Teacher { get; set; }
        public List<Student> Students { get; set; } = new List<Student>();
        public TimeSpan Duration { get; set; }
        public List<ClassAttendenceLogEntry> AttendenceLog { get; set; } = new List<ClassAttendenceLogEntry>();
        public Grade Grade { get; set; }
    }


}

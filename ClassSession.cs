namespace FcmsPortal
{
    public class ClassSession
    {
        public int ID;
        public string Name { get; set; }
        public string Description { get; set; }
        public Person Teacher { get; set; }
        public List<Person> Students { get; set; } = new List<Person>();
        public List<ClassAttendenceLogEntry> AttendenceLog { get; set; } = new List<ClassAttendenceLogEntry>();
        public EducationLevel Level { get; set; }
    }


}

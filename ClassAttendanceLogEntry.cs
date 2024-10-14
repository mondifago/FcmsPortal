namespace FcmsPortal
{
    public class ClassAttendanceLogEntry
    {
        public int Id { get; set; }
        public Staff Teacher { get; set; }
        public List<Student> Attendees { get; set; } = new();
        public DateTime TimeStamp { get; set; }
    }
}

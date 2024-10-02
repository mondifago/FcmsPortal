namespace FcmsPortal
{
    public class ClassAttendenceLogEntry
    {
        public int Id { get; set; }
        public Staff Teacher { get; set; }
        public List<Student> Atendees { get; set; } = new();
        public DateTime TimeStamp { get; set; }
    }
}

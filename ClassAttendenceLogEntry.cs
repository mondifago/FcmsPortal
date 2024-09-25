namespace FcmsPortal
{
    public class ClassAttendenceLogEntry
    {
        public int Id { get; set; }
        public Person Teacher { get; set; }
        public List<Person> Atendees { get; set; } = new();
        public DateTime TimeStamp { get; set; }
    }
}

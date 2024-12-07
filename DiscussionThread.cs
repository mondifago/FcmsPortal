namespace FcmsPortal
{
    public class DiscussionThread
    {
        public int Id { get; set; }
        public Person Author { get; set; }
        public string Comment { get; set; } // The question or comment
        public DateTime Timestamp { get; set; }
    }
}

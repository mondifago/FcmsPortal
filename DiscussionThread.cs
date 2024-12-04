namespace FcmsPortal
{
    public class DiscussionThread
    {
        public int Id { get; set; }
        public Student Student { get; set; }
        public string Comment { get; set; } // The question or comment
        public DateTime Timestamp { get; set; }
        public List<TeacherReply> Replies { get; set; } = new(); // Replies from the teacher
    }
}

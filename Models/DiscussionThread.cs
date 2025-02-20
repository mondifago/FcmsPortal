namespace FcmsPortal.Models
{
    public class DiscussionThread
    {
        public int Id { get; set; }
        public DiscussionPost FirstPost { get; set; }
        public bool IsPrivate { get; set; }
        public List<DiscussionPost> Replies { get; set; } = new List<DiscussionPost>();
        public List<FileAttachment> Attachments { get; set; } = new List<FileAttachment>();
    }

    public class DiscussionPost
    {
        public int Id { get; set; }
        public Person Author { get; set; }
        public string Comment { get; set; }
        public DateTime Timestamp { get; set; }
    }
}

namespace FcmsPortal.Models
{
    public class DiscussionThread
    {
        public int Id { get; set; }
        public DiscussionPost FirstPost { get; set; } = new();
        public List<DiscussionPost> Replies { get; set; } = new();
        public List<FileAttachment>? Attachments { get; set; } = new();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;

        public void UpdateLastUpdated()
        {
            LastUpdatedAt = DateTime.UtcNow;
        }
    }
}
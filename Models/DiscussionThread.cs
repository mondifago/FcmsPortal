namespace FcmsPortal.Models
{
    public class DiscussionThread
    {
        public int Id { get; set; }
        public int ClassSessionId { get; set; }
        public ClassSession ClassSession { get; set; } = null!;

        public FirstPost FirstPost { get; set; } = null!;
        public List<Reply> Replies { get; set; } = new();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
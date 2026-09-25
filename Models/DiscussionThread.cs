namespace FcmsPortal.Models
{
    public class DiscussionThread
    {
        public int Id { get; set; }
        public int ClassSessionRecordId { get; set; }
        public ClassSessionRecord ClassSessionRecord { get; set; } = null!;

        public FirstPost FirstPost { get; set; } = null!;
        public List<Reply> Replies { get; set; } = new();

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastUpdatedAt { get; set; } = DateTime.Now;
    }
}
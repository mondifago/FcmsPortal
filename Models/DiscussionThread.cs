using System.ComponentModel.DataAnnotations.Schema;

namespace FcmsPortal.Models
{
    public class DiscussionThread
    {
        public int Id { get; set; }
        public int ClassSessionId { get; set; }
        public ClassSession ClassSession { get; set; } = null!;
        public int FirstPostId { get; set; }
        [ForeignKey(nameof(FirstPostId))]
        public DiscussionPost FirstPost { get; set; } = null!;
        public List<DiscussionPost> Replies { get; set; } = new();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;

        public void UpdateLastUpdated()
        {
            LastUpdatedAt = DateTime.UtcNow;
        }
    }
}
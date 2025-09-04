using System.ComponentModel.DataAnnotations;

namespace FcmsPortal.Models
{
    public class DiscussionPost
    {
        public int Id { get; set; }
        public int DiscussionThreadId { get; set; }
        public DiscussionThread DiscussionThread { get; set; } = null!;
        public int PersonId { get; set; }
        public Person Author { get; set; } = null!;

        [Required(ErrorMessage = "Comment is required.")]
        [StringLength(50, ErrorMessage = "Comment must be 50 characters or fewer.")]
        public string Comment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? EditedAt { get; set; }

        public void MarkAsEdited()
        {
            EditedAt = DateTime.UtcNow;
        }
    }
}
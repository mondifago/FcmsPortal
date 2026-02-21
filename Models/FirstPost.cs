using System.ComponentModel.DataAnnotations;

namespace FcmsPortal.Models
{
    public class FirstPost
    {
        public int Id { get; set; }
        public int DiscussionThreadId { get; set; }
        public DiscussionThread DiscussionThread { get; set; } = null!;

        public int PersonId { get; set; }
        public Person Author { get; set; } = null!;

        [Required]
        [StringLength(500, ErrorMessage = "your message cannot exceed 500 characters.")]
        public string Comment { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}

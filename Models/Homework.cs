using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FcmsPortal.Models
{
    public class Homework
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(50, ErrorMessage = "Title must be 50 characters or fewer.")]
        public string Title { get; set; } = string.Empty;

        public DateTime AssignedDate { get; set; }

        public DateTime DueDate { get; set; }

        public int ClassSessionId { get; set; }

        [ForeignKey(nameof(ClassSessionId))]
        public ClassSession? ClassSession { get; set; }

        [Required(ErrorMessage = "Question is required.")]
        [StringLength(2000, ErrorMessage = "Question must be 2000 characters or fewer.")]
        public string Question { get; set; } = string.Empty;

        public List<HomeworkSubmission> Submissions { get; set; } = new();
    }
}

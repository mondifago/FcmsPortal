using FcmsPortal.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FcmsPortal.Models
{
    public class Homework
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(FcmsConstants.MAX_HOMEWORK_TITLE_LENGTH, ErrorMessage = "Title must be {1} characters or fewer.")]
        public string Title { get; set; } = string.Empty;

        public DateTime AssignedDate { get; set; }

        public DateTime DueDate { get; set; }
        public int MaxScore { get; set; } = (int)FcmsConstants.TOTAL_SCORE;

        public int ClassSessionRecordId { get; set; }

        [ForeignKey(nameof(ClassSessionRecordId))]
        public ClassSessionRecord? ClassSessionRecord { get; set; }

        [Required(ErrorMessage = "Question is required.")]
        [StringLength(FcmsConstants.MAX_HOMEWORK_QUESTION_LENGTH, ErrorMessage = "Question must be {1} characters or fewer.")]
        public string Question { get; set; } = string.Empty;

        public List<HomeworkSubmission> Submissions { get; set; } = new();
    }
}

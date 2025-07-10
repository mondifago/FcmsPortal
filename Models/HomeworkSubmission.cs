using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FcmsPortal.Models
{
    public class HomeworkSubmission
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Student is required.")]
        public int StudentId { get; set; }

        [ForeignKey(nameof(StudentId))]
        public Student? Student { get; set; }

        [Required(ErrorMessage = "Answer is required.")]
        [StringLength(2000, ErrorMessage = "Answer must be 2000 characters or fewer.")]
        public string Answer { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime SubmissionDate { get; set; }

        public bool IsGraded { get; set; } = false;

        [StringLength(100, ErrorMessage = "Feedback comment must be 100 characters or fewer.")]
        public string? FeedbackComment { get; set; }

        public int? HomeworkGradeId { get; set; }

        [ForeignKey(nameof(HomeworkGradeId))]
        public TestGrade? HomeworkGrade { get; set; }

        [Required(ErrorMessage = "Homework reference is required.")]
        public int HomeworkId { get; set; }

        [ForeignKey(nameof(HomeworkId))]
        public Homework? Homework { get; set; }
    }
}
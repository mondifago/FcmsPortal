using System.ComponentModel.DataAnnotations;

namespace FcmsPortal.Models
{
    public class ClassSession
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Course name is required.")]
        [StringLength(50, ErrorMessage = "Course name must be 50 characters or fewer.")]
        public string Course { get; set; } = string.Empty;

        [Required(ErrorMessage = "Topic is required.")]
        [StringLength(50, ErrorMessage = "Topic must be 50 characters or fewer.")]
        public string Topic { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "Description must be 100 characters or fewer.")]
        public string Description { get; set; } = string.Empty;

        public string LessonPlan { get; set; } = string.Empty;

        [Required(ErrorMessage = "Teacher is required.")]
        public Staff Teacher { get; set; } = new();

        public Homework? HomeworkDetails { get; set; }

        [StringLength(500, ErrorMessage = "Teacher remarks must be 500 characters or fewer.")]
        public string TeacherRemarks { get; set; } = string.Empty;

        public List<FileAttachment> StudyMaterials { get; set; } = new();
        public List<DiscussionThread> DiscussionThreads { get; set; } = new();
    }
}

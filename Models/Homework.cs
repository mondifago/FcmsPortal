using System.ComponentModel.DataAnnotations.Schema;

namespace FcmsPortal.Models
{
    public class Homework
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime AssignedDate { get; set; }
        public DateTime DueDate { get; set; }
        public int ClassSessionId { get; set; }
        [ForeignKey(nameof(ClassSessionId))]
        public ClassSession ClassSession { get; set; }

        public List<string> Questions { get; set; } = new();
        public List<FileAttachment> Attachments { get; set; } = new List<FileAttachment>();
        public List<HomeworkSubmission> Submissions { get; set; } = new List<HomeworkSubmission>();
        public List<DiscussionThread> Discussions { get; set; } = new();
    }
}

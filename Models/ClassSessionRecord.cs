using FcmsPortal.Constants;
using System.ComponentModel.DataAnnotations;

namespace FcmsPortal.Models
{
    public class ClassSessionRecord
    {
        public int Id { get; set; }

        public int ClassSessionId { get; set; }
        public ClassSession? ClassSession { get; set; }

        public int AcademicPeriodId { get; set; }
        public AcademicPeriod? AcademicPeriod { get; set; }

        [StringLength(FcmsConstants.MAX_SESSION_REMARK_LENGTH, ErrorMessage = "Teacher remarks must be {1} characters or fewer.")]
        public string TeacherRemarks { get; set; } = string.Empty;
        public string RemarksSubmittedByName { get; set; } = string.Empty;
        public DateTime? RemarksSubmittedAt { get; set; }

        public DateTime? ClosedAt { get; set; }
        public string ClosedByName { get; set; } = string.Empty;

        public Homework? HomeworkDetails { get; set; }
        public List<DiscussionThread> DiscussionThreads { get; set; } = new();
    }
}

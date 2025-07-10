using FcmsPortal.Enums;

namespace FcmsPortal.Models
{
    public class GradesReport
    {
        public int LearningPathId { get; set; }

        public string LearningPathName { get; set; } = string.Empty;

        public DateTime DateSubmitted { get; set; } = DateTime.Now;

        public string SubmittedBy { get; set; } = string.Empty;

        public int NumberOfStudents { get; set; }

        public PrincipalApprovalStatus Status { get; set; } = PrincipalApprovalStatus.Pending;
    }
}

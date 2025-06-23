using FcmsPortal.Enums;

namespace FcmsPortal.Models
{
    public class GradesReport
    {
        public int LearningPathId { get; set; }
        public string LearningPathName { get; set; }
        public DateTime DateSubmitted { get; set; }
        public string SubmittedBy { get; set; }
        public int NumberOfStudents { get; set; }
        public PrincipalApprovalStatus Status { get; set; } = PrincipalApprovalStatus.Pending;
    }
}

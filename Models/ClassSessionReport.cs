namespace FcmsPortal.Models
{
    public class ClassSessionReport
    {
        public int ClassSessionId { get; set; }

        public string LearningPathName { get; set; } = string.Empty;

        public string Course { get; set; } = string.Empty;

        public string Topic { get; set; } = string.Empty;

        public string SubmittedBy { get; set; } = string.Empty;

        public DateTime TimeSubmitted { get; set; } = DateTime.Now;
    }
}

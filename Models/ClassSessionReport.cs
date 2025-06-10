namespace FcmsPortal.Models
{
    public class ClassSessionReport
    {
        public int ClassSessionId { get; set; }
        public string LearningPathName { get; set; }
        public string Course { get; set; }
        public string Topic { get; set; }
        public string SubmittedBy { get; set; }
        public DateTime TimeSubmitted { get; set; } = DateTime.Now;
    }
}

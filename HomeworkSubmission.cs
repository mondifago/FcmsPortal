namespace FcmsPortal
{
    public class HomeworkSubmission
    {
        public int Id { get; set; }
        public Student Student { get; set; }
        public string Answer { get; set; }
        public DateTime SubmissionDate { get; set; }
    }
}

namespace FcmsPortal
{
    public class Homework
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime AssignedDate { get; set; }
        public DateTime DueDate { get; set; }
        public ClassSession ClassSession { get; set; } // Reference to the session
        public List<HomeworkSubmission> Submissions { get; set; } = new List<HomeworkSubmission>();
        public List<DiscussionThread> Discussions { get; set; } = new();
    }
}

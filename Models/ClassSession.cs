namespace FcmsPortal.Models
{
    public class ClassSession
    {
        public int Id { get; set; }
        public string Course { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public string LessonPlan { get; set; }
        public Staff Teacher { get; set; }
        public Homework HomeworkDetails { get; set; }
        public string TeacherRemarks { get; set; }
        public List<FileAttachment> StudyMaterials { get; set; } = new List<FileAttachment>();
        public List<DiscussionThread> DiscussionThreads { get; set; } = new List<DiscussionThread>();
    }
}

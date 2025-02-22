namespace FcmsPortal.Models
{
    public class ClassSession
    {
        public int Id { get; set; }
        public string Course { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public string LessonNote { get; set; }
        public Staff Teacher { get; set; }
        public List<ClassAttendanceLogEntry> AttendanceLog { get; set; } = new List<ClassAttendanceLogEntry>();
        public List<Homework> HomeworkDetails { get; set; } = new();
        public string TeacherRemarks { get; set; }
        public List<FileAttachment> StudyMaterials { get; set; } = new List<FileAttachment>();
        public List<DiscussionThread> DiscussionThreads { get; set; } = new List<DiscussionThread>();
    }
}

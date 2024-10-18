namespace FcmsPortal
{
    public class SchoolCalendar
    {
        public int Id { get; set; }
        public string AcademicYear { get; set; }
        public List<LearningPath> LearningPaths { get; set; } = new List<LearningPath>();
    }
}

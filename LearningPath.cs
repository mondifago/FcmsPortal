namespace FcmsPortal
{
    public class LearningPath
    {
        public int Id { get; set; }
        public Course Name { get; set; } //e.g Biology 
        public EducationLevel EducationLevel { get; set; }
        public ClassLevel ClassLevel { get; set; }
        public int Semester { get; set; }
        public List<ClassSession> ClassInfos { get; set; } = new List<ClassSession>();
        public List<ScheduleEntry> Schedule { get; set; } = new List<ScheduleEntry>();
    }
}

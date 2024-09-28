namespace FcmsPortal
{
    public class LearningPath
    {
        public int Id { get; set; }
        public string Name { get; set; } //e.g Biology 
        public int Year { get; set; }
        public int Semester { get; set; }
        public List<ClassSession> ClassInfos { get; set; } = new List<ClassSession>();
        public List<ScheduleEntry> Schedule { get; set; } = new List<ScheduleEntry>();
    }
}

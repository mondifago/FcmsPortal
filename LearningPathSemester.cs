namespace FcmsPortal
{
    public class LearningPathSemester
    {
        string Name;
        int Semester;
        public Dictionary<DayOfWeek, List<ClassSession>> ClassInfosForDay { get; set; }
    }
}

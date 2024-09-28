namespace FcmsPortal
{
    public class School
    {
        public List<Person> Staff { get; set; } = new();
        public List<Person> Students { get; set; } = new();
        public List<LearningPath> LearningPaths { get; set; } = new();
        public List<Grade> Grades { get; set; } = new();
    }
}

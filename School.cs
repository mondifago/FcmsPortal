namespace FcmsPortal
{
    public class School
    {
        public string Name { get; set; }
        public Address SchoolAddress { get; set; }
        public List<Staff> Staff { get; set; } = new();
        public List<Student> Students { get; set; } = new();
        public List<Guardian> Guardians { get; set; } = new();
        public List<LearningPath> LearningPaths { get; set; } = new();
        public List<Grade> Grades { get; set; } = new();
    }
}

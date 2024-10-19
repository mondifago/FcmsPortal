namespace FcmsPortal
{
    public class Curriculum
    {
        public int Id { get; set; }
        public EducationLevel EducationLevel { get; set; }
        public ClassLevel ClassLevel { get; set; }
        public List<ClassSession> ClassSessions { get; set; } = new List<ClassSession>();

    }
}

using FcmsPortal.Enums;

namespace FcmsPortal.ViewModel
{
    public class Curriculum
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int Semester { get; set; }
        public EducationLevel EducationLevel { get; set; }
        public ClassLevel ClassLevel { get; set; }
        public List<ClassSession> ClassSessions { get; set; } = new List<ClassSession>();
    }
}
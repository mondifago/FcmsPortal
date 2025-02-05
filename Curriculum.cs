using FcmsPortal.Enums;

namespace FcmsPortal
{
    public class Curriculum
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public EducationLevel EducationLevel { get; set; }
        public ClassLevel ClassLevel { get; set; }
        public List<SemesterCurriculum> Semesters { get; set; } = new();
        public string Course { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public string LessonNote { get; set; }
    }
}
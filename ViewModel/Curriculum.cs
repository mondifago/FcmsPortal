using FcmsPortal.Enums;

namespace FcmsPortal.ViewModel
{
    public class Curriculum
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public EducationLevel EducationLevel { get; set; }
        public ClassLevel ClassLevel { get; set; }
        public List<SemesterCurriculum> Semesters { get; set; } = new();
    }
}
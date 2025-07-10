using FcmsPortal.Enums;

namespace FcmsPortal.Models
{
    public class Curriculum
    {
        public string AcademicYear { get; set; } = string.Empty;
        public EducationLevel EducationLevel { get; set; }
        public ClassLevel ClassLevel { get; set; }
        public List<SemesterCurriculum> Semesters { get; set; } = new();
    }
}
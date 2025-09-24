using FcmsPortal.Enums;

namespace FcmsPortal.Models
{
    public class AttendanceArchive
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public int LearningPathId { get; set; }
        public string LearningPathName { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public bool IsPresent { get; set; }
        public DateTime AcademicYearStart { get; set; }
        public Semester Semester { get; set; }
        public EducationLevel EducationLevel { get; set; }
        public ClassLevel ClassLevel { get; set; }
        public DateTime ArchivedDate { get; set; }
    }
}

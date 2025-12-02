using FcmsPortal.Enums;

namespace FcmsPortal.Models
{
    public class ArchivedLearningPathGrade
    {
        public int Id { get; set; }
        public int LearningPathId { get; set; }
        public string LearningPathName { get; set; } = string.Empty;
        public string AcademicYear { get; set; } = string.Empty;
        public EducationLevel EducationLevel { get; set; }
        public ClassLevel ClassLevel { get; set; }
        public Semester Semester { get; set; }
        public DateTime SemesterStartDate { get; set; }
        public DateTime SemesterEndDate { get; set; }
        public int TotalStudentsInPath { get; set; }
        public double AverageClassGrade { get; set; }
        public DateTime ArchivedDate { get; set; } = DateTime.Now;
        public List<ArchivedStudentGrade> StudentGrades { get; set; } = new List<ArchivedStudentGrade>();
    }
}

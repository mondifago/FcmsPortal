using FcmsPortal.Enums;

namespace FcmsPortal.Models
{
    public class LearningPathGradeReport
    {
        public int Id { get; set; }
        public LearningPath? LearningPath { get; set; }
        public Semester Semester { get; set; }
        public Dictionary<Student, double> StudentSemesterGrades { get; set; } = new();
        public List<StudentGradeSummary> RankedStudents { get; set; } = new();
        public bool IsFinalized { get; set; }
    }
}

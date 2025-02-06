using FcmsPortal.Enums;

namespace FcmsPortal
{
    public class LearningPathGradeReport
    {
        public int Id { get; set; }
        public LearningPath LearningPath { get; set; }
        public Semester Semester { get; set; }
        public Dictionary<Student, double> StudentSemesterGrades { get; set; } = new();
        public List<StudentGradeSummary> RankedStudents { get; set; } = new();
        public bool IsFinalized { get; set; }
    }

    public class StudentGradeSummary
    {
        public Student Student { get; set; }
        public double SemesterOverallGrade { get; set; }
    }
}

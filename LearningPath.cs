using FcmsPortal.Enums;

namespace FcmsPortal
{
    public class LearningPath
    {
        public int Id { get; set; }
        public EducationLevel EducationLevel { get; set; }
        public ClassLevel ClassLevel { get; set; }
        public Semester Semester { get; set; }
        public double FeePerSemester { get; set; }
        public Dictionary<Student, CourseGrade> GradesList { get; set; } = new();
        public List<ScheduleEntry> Schedule { get; set; } = new List<ScheduleEntry>();
        public List<Student> Students { get; set; } = new List<Student>();
        public List<Student> StudentsPaymentSuccessful { get; set; } = new List<Student>();
        public PrincipalApprovalStatus ApprovalStatus { get; set; } = PrincipalApprovalStatus.Pending;

    }
}

using FcmsPortal.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace FcmsPortal.Models
{
    public class LearningPath
    {
        public int Id { get; set; }
        public EducationLevel EducationLevel { get; set; }
        public ClassLevel ClassLevel { get; set; }
        public Semester Semester { get; set; }
        public DateTime AcademicYearStart { get; set; }
        public DateTime SemesterStartDate { get; set; }
        public DateTime SemesterEndDate { get; set; }
        public DateTime ExamsStartDate { get; set; }

        [NotMapped]
        public string AcademicYear
        {
            get
            {
                int startYear = AcademicYearStart.Year;
                int endYear = startYear + 1;
                return $"{startYear}-{endYear}";
            }
        }
        public double FeePerSemester { get; set; }
        public List<ScheduleEntry> Schedule { get; set; } = new List<ScheduleEntry>();
        public List<Student> Students { get; set; } = new List<Student>();
        public List<Student> StudentsWithAccess { get; set; } = new List<Student>();
        public PrincipalApprovalStatus ApprovalStatus { get; set; } = PrincipalApprovalStatus.Pending;

    }
}

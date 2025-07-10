using FcmsPortal.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FcmsPortal.Models
{
    public class LearningPath
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Education level is required.")]
        public EducationLevel EducationLevel { get; set; }

        [Required(ErrorMessage = "Class level is required.")]
        public ClassLevel ClassLevel { get; set; }

        [Required(ErrorMessage = "Semester is required.")]
        public Semester Semester { get; set; }

        [Required(ErrorMessage = "Academic year start date is required.")]
        public DateTime AcademicYearStart { get; set; }

        [Required(ErrorMessage = "Semester start date is required.")]
        public DateTime SemesterStartDate { get; set; }

        [Required(ErrorMessage = "Semester end date is required.")]
        public DateTime SemesterEndDate { get; set; }

        [Required(ErrorMessage = "Exams start date is required.")]
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
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

        [Required(ErrorMessage = "Fee per semester is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Fee must be a positive value.")]
        public double FeePerSemester { get; set; }

        public List<ScheduleEntry> Schedule { get; set; } = new List<ScheduleEntry>();

        public List<Student> Students { get; set; } = new List<Student>();

        public List<Student> StudentsWithAccess { get; set; } = new List<Student>();

        public List<DailyAttendanceLogEntry> AttendanceLog { get; set; } = new List<DailyAttendanceLogEntry>();

        public List<CourseGradingConfiguration> CourseGradingConfigurations { get; set; } = new List<CourseGradingConfiguration>();

        public PrincipalApprovalStatus ApprovalStatus { get; set; } = PrincipalApprovalStatus.Pending;

        public bool IsTemplate { get; set; } = false;

        [StringLength(100)]
        public string? TemplateKey { get; set; }
    }
}
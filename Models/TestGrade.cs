using FcmsPortal.Enums;

namespace FcmsPortal.Models
{
    public class TestGrade
    {
        public int Id { get; set; }
        public Staff Teacher { get; set; }
        public double Score { get; set; }
        public DateTime Date { get; set; }
        public GradeType GradeType { get; set; }
        public string TeacherRemark { get; set; } = string.Empty;
        public int CourseGradeId { get; set; }
        public CourseGrade CourseGrade { get; set; }
    }
}

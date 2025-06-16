using FcmsPortal.Enums;

namespace FcmsPortal.Models
{
    public class TestGrade
    {
        public int Id { get; set; }
        public string Course { get; set; } = string.Empty;
        public Staff Teacher { get; set; }
        public double Score { get; set; }
        public DateTime Date { get; set; }
        public Semester Semester { get; set; }
        public GradeType GradeType { get; set; }
        public string TeacherRemark { get; set; } = string.Empty;
    }
}

using FcmsPortal.Enums;

namespace FcmsPortal
{
    public class TestGrade
    {
        public string Course { get; set; }
        public Staff Teacher { get; set; }
        public double Score { get; set; }
        public DateTime Date { get; set; }
        public int Semester { get; set; }
        public GradeType GradeType { get; set; }
    }
}

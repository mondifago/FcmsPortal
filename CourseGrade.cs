namespace FcmsPortal
{
    public class CourseGrade
    {
        public List<TestGrade> TestGrades { get; set; } = new List<TestGrade>();
        public double AttendancePercentage { get; set; }
        public double TotalGrade { get; private set; }
    }
}

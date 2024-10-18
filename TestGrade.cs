namespace FcmsPortal
{
    public class TestGrade
    {
        public string Course { get; set; }  // Reference to the course
        public Staff Teacher { get; set; }  // Reference to the teacher
        public double Score { get; set; }   // Score for the test/quiz
        public DateTime Date { get; set; }  // Date of the test
        public GradeType GradeType { get; set; }  // Enum to distinguish Quiz, Homework, etc.
    }

    public enum GradeType
    {
        Quiz,
        Homework,
        FinalExam,
    }
}

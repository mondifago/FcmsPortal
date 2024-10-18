namespace FcmsPortal
{
    public class Grade
    {
        public int ClassSessionId { get; private set; }
        public Dictionary<Student, TestGrade> Scores { get; private set; } = new Dictionary<Student, TestGrade>();
        public GradingFormula Formula { get; private set; }

    }
}

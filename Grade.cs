namespace FcmsPortal
{
    public class Grade
    {
        public int ClassSessionId { get; private set; }
        public Dictionary<Student, Score> Scores { get; private set; } = new Dictionary<Student, Score>();
        public GradingFormula Formula { get; private set; }

    }
}

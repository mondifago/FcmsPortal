namespace FcmsPortal
{
    public class Grade
    {
        public Student Student { get; private set; }
        public Course Course { get; private set; }
        public List<double> HomeworkScores { get; private set; } = new List<double>();
        public List<double> QuizScores { get; private set; } = new List<double>();
        public double AttendanceScore { get; private set; }
        public double ExamScore { get; private set; }
        public GradingFormula Formula { get; private set; }
    }
}

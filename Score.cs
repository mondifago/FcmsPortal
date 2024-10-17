namespace FcmsPortal
{
    public class Score
    {
        public List<double> HomeworkScores { get; set; } = new List<double>();
        public List<double> QuizScores { get; set; } = new List<double>();
        public double AttendanceScore { get; set; }
        public double ExamScore { get; set; }
    }
}

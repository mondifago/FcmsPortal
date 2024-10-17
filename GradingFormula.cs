namespace FcmsPortal
{
    public class GradingFormula
    {
        public double HomeworkWeight { get; set; }
        public double QuizWeight { get; set; }
        public double AttendanceWeight { get; set; }
        public double ExamWeight { get; set; }

        public GradingFormula(double homeworkWeight, double quizWeight, double attendanceWeight, double examWeight)
        {
            if (Math.Abs((homeworkWeight + quizWeight + attendanceWeight + examWeight) - 100) > 0.001)
                throw new ArgumentException("Weights must sum to 100%");

            HomeworkWeight = homeworkWeight / 100;
            QuizWeight = quizWeight / 100;
            AttendanceWeight = attendanceWeight / 100;
            ExamWeight = examWeight / 100;
        }
    }
}

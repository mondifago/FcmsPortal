namespace FcmsPortal
{
    public class Grade
    {
        public int Id { get; set; }
        public Course Course { get; set; }
        public Staff GradingTeacher { get; set; }
        public Student Student { get; set; }
        public int QuizScore { get; set; }
    }
}

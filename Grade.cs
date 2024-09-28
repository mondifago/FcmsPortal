namespace FcmsPortal
{
    public class Grade
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Person GradingTeacher { get; set; }
        public Person Student { get; set; }
        public int QuizScore { get; set; }
    }
}

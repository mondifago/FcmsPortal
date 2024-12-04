namespace FcmsPortal
{
    public class HomeworkResponse
    {
        public int Id { get; set; }
        public Homework Homework { get; set; }
        public Staff Teacher { get; set; }
        public string GeneralFeedback { get; set; } // Teacher's general comments/answers for the class
        public DateTime ReviewDate { get; set; }
    }

}

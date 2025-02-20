namespace FcmsPortal.Models
{
    public class HomeworkResponse
    {
        public int Id { get; set; }
        public Homework Homework { get; set; }
        public Staff Teacher { get; set; }
        public string GeneralFeedback { get; set; } 
        public DateTime ReviewDate { get; set; }
    }

}

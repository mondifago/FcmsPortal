namespace FcmsPortal.Models
{
    public class DiscussionThreadLike
    {
        public int Id { get; set; }
        public int DiscussionThreadId { get; set; }
        public DiscussionThread DiscussionThread { get; set; } = null!;
        public int PersonId { get; set; }
        public Person? Person { get; set; }
    }
}

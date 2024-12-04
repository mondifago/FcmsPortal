namespace FcmsPortal
{
    public class TeacherReply
    {
        public int Id { get; set; }
        public Staff Teacher { get; set; } // The teacher responding
        public string Response { get; set; } // The response content
        public DateTime Timestamp { get; set; }
    }

}

﻿namespace FcmsPortal
{
    public class DiscussionThread
    {
        public int Id { get; set; }
        public Person Author { get; set; }
        public string Comment { get; set; } 
        public DateTime Timestamp { get; set; }
        public List<DiscussionThread> Replies { get; set; } = new List<DiscussionThread>();
    }
}

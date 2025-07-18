﻿using System.ComponentModel.DataAnnotations;

namespace FcmsPortal.Models
{
    public class DiscussionPost
    {
        public int Id { get; set; }
        public int DiscussionThreadId { get; set; }
        public Person Author { get; set; } = new();

        [Required(ErrorMessage = "Comment is required.")]
        [StringLength(100, ErrorMessage = "Comment must be 100 characters or fewer.")]
        public string Comment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? EditedAt { get; set; }

        public void MarkAsEdited()
        {
            EditedAt = DateTime.UtcNow;
        }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FcmsPortal.Models
{
    public class Announcement
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Announcement message is required.")]
        [StringLength(100, ErrorMessage = "Announcement cannot exceed 100 characters.")]
        public string Message { get; set; } = string.Empty;
        public DateTime StartDate { get; set; } = DateTime.Today;
        public DateTime EndDate { get; set; } = DateTime.Today.AddDays(7);
        public DateTime PostedAt { get; set; } = DateTime.Now;
        public int? PostedById { get; set; }
        public Person? PostedBy { get; set; }
        [NotMapped]
        public bool IsActive => DateTime.Today >= StartDate.Date && DateTime.Today <= EndDate.Date;
    }
}

using System.ComponentModel.DataAnnotations;

namespace FcmsPortal.Models
{
    public class Quote
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Quote text is required.")]
        [StringLength(500, ErrorMessage = "Quote cannot exceed 500 characters.")]
        public string Text { get; set; } = string.Empty;
        [StringLength(50, ErrorMessage = "Author name cannot exceed 50 characters.")]
        public string Author { get; set; } = string.Empty;
        public DateTime DateAdded { get; set; } = DateTime.Now;
        public int? AddedById { get; set; }
        public Person? AddedBy { get; set; }
    }
}

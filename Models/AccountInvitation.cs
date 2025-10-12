using System.ComponentModel.DataAnnotations;

namespace FcmsPortal.Models
{
    public class AccountInvitation
    {
        public int Id { get; set; }
        public int PersonId { get; set; }

        [Required]
        public string Token { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiryDate { get; set; } = DateTime.UtcNow.AddDays(7);
        public bool IsUsed { get; set; } = false;
        public int? SentByAccountId { get; set; }
    }
}

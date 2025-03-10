using FcmsPortal.Enums;
using System.ComponentModel.DataAnnotations;
namespace FcmsPortal.Models

{
    public class Guardian
    {
        public int Id { get; set; }
        [Required]
        public Person Person { get; set; } = new Person();

        public Relationship RelationshipToStudent { get; set; }

        [Required(ErrorMessage = "Occupation is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Occupation must be between 2 and 50 characters.")]
        public string Occupation { get; set; } = string.Empty;
    }
}

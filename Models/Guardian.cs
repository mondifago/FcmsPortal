using FcmsPortal.Enums;
using System.ComponentModel.DataAnnotations;

namespace FcmsPortal.Models
{
    public class Guardian
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Person details are required.")]
        public Person Person { get; set; } = new Person();

        [Required(ErrorMessage = "Relationship to student is required.")]
        public Relationship RelationshipToStudent { get; set; }

        [Required(ErrorMessage = "Occupation is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Occupation must be between 2 and 50 characters.")]
        public string Occupation { get; set; } = string.Empty;
    }
}

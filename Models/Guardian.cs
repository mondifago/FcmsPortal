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
        [Required]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "Occupation must be between 2 and 15 characters.")]
        public string Occupation { get; set; } = string.Empty;
        public List<Student> Wards { get; set; } = new();
    }
}

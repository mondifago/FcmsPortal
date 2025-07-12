using FcmsPortal.Enums;
using System.ComponentModel.DataAnnotations;
namespace FcmsPortal.Models

{
    public class Guardian
    {
        public int Id { get; set; }
        [Required]
        public Person Person { get; set; } = new Person();

        [Required(ErrorMessage = "RelationShip with Student is Required")]
        [MaxLength(50)]
        public Relationship RelationshipToStudent { get; set; }

        public string Occupation { get; set; } = string.Empty;
        public List<Student> Wards { get; set; } = new();
    }
}

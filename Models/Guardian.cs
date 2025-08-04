using FcmsPortal.Enums;
using System.ComponentModel.DataAnnotations;
namespace FcmsPortal.Models
{
    public class Guardian
    {
        public int Id { get; set; }
        public Person Person { get; set; } = new Person();

        [Required(ErrorMessage = "RelationShip with Student is Required")]
        public Relationship RelationshipToStudent { get; set; }

        [Required(ErrorMessage = "Occupation is Required")]
        public string Occupation { get; set; } = string.Empty;
        public List<Student> Wards { get; set; } = new List<Student>();
    }
}

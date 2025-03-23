using FcmsPortal.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FcmsPortal.Models

{
    public class Guardian
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int PersonId { get; set; }
        [Required]
        [ForeignKey("PersonId")]
        public Person Person { get; set; } = new Person();

        public Relationship RelationshipToStudent { get; set; }

        [Required(ErrorMessage = "Occupation is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Occupation must be between 2 and 50 characters.")]
        public string Occupation { get; set; } = string.Empty;

        [InverseProperty("Guardian")]
        public List<Student> Students { get; set; } = new();
    }
}

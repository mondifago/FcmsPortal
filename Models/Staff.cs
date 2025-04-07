using FcmsPortal.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FcmsPortal.Models;

public class Staff
{
    public int Id { get; set; }
    [Required]
    public Person Person { get; set; } = new Person();
    public JobRole JobRole { get; set; }
    [MaxLength(100)]
    public string? JobDescription { get; set; }
    [MaxLength(50)]
    public List<string>? Qualifications { get; set; }
    [MaxLength(50)]
    public List<string>? WorkExperience { get; set; }
    [Required]
    [Column(TypeName = "date")]
    public DateTime DateOfEmployment { get; set; }
    [MaxLength(50)]
    public string? AreaOfSpecialization { get; set; }
}
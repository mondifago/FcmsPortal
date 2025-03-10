using FcmsPortal.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FcmsPortal.Models;

public class Staff
{
    public int Id { get; set; }

    [Required]
    public Person Person { get; set; }

    public JobRole JobRole { get; set; }

    [MaxLength(500)]
    public string JobDescription { get; set; }

    public List<string> Qualifications { get; set; }

    public List<string> WorkExperience { get; set; }

    [Required]
    [Column(TypeName = "date")]
    public DateOnly DateOfEmployment { get; set; }

    [MaxLength(100)]
    public string AreaOfSpecialization { get; set; }
}
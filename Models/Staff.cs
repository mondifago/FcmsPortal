using FcmsPortal.Enums;
using System.ComponentModel.DataAnnotations;

namespace FcmsPortal.Models;

public class Staff
{
    public int Id { get; set; }

    [Required]
    public Person Person { get; set; } = new();

    [Required(ErrorMessage = "Job Role is Required")]
    public JobRole JobRole { get; set; }

    [Required(ErrorMessage = "Job Description is Required")]
    [MaxLength(50)]
    public string JobDescription { get; set; } = string.Empty;

    public List<string> Qualifications { get; set; } = new();

    public List<string> WorkExperience { get; set; } = new();

    [Required(ErrorMessage = "Date of Employment is Required")]
    public DateTime DateOfEmployment { get; set; }

    [MaxLength(50)]
    public string AreaOfSpecialization { get; set; } = string.Empty;
}

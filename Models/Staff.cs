using FcmsPortal.Enums;
using System.ComponentModel.DataAnnotations;

namespace FcmsPortal.Models;

public class Staff
{
    public int Id { get; set; }
    public int SchoolId { get; set; }
    public School School { get; set; } = null!;

    [Required]
    public int PersonId { get; set; }
    public Person Person { get; set; } = null!;

    [Required(ErrorMessage = "Job Role is Required")]
    public JobRole JobRole { get; set; }

    [Required(ErrorMessage = "Job Description is Required")]
    [MaxLength(50)]
    public string JobDescription { get; set; } = string.Empty;

    public string Qualifications { get; set; } = string.Empty;
    public string WorkExperience { get; set; } = string.Empty;

    [Required(ErrorMessage = "Date of Employment is Required")]
    public DateTime DateOfEmployment { get; set; }

    [MaxLength(50)]
    public string AreaOfSpecialization { get; set; } = string.Empty;

    public List<ClassSession> ClassSessions { get; set; } = new();
}

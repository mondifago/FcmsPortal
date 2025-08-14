using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FcmsPortal.Models;

public class Student
{
    public int Id { get; set; }
    [Required]
    public int PersonId { get; set; }
    public Person Person { get; set; } = null!;
    public int? SchoolId { get; set; }
    [ForeignKey("SchoolId")]
    public School? School { get; set; }
    [MaxLength(10)]
    public string? PositionAmongSiblings { get; set; }
    [MaxLength(30)]
    public string? LastSchoolAttended { get; set; }
    [Required]
    public int GuardianId { get; set; }
    [ForeignKey("GuardianId")]
    public Guardian? Guardian { get; set; }
    public List<CourseGrade> CourseGrades { get; set; } = new();
    public int? LearningPathId { get; set; }
    [ForeignKey("LearningPathId")]
    public LearningPath? LearningPath { get; set; }
    [NotMapped]
    public List<FileAttachment> FinalResultAttachments { get; set; } = new();
    public DateTime? ArchivedDate { get; set; }
}
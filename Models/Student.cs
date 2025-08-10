using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FcmsPortal.Models;

public class Student
{
    public int Id { get; set; }
    [Required]
    public Person Person { get; set; } = new Person();
    [MaxLength(10)]
    public string? PositionAmongSiblings { get; set; }
    [MaxLength(30)]
    public string? LastSchoolAttended { get; set; }
    [Required]
    public int GuardianId { get; set; }
    [ForeignKey("GuardianId")]
    public Guardian? Guardian { get; set; }
    public List<CourseGrade> CourseGrades { get; set; } = new();
    [Required]
    public int CurrentLearningPathId { get; set; }
    [ForeignKey("CurrentLearningPathId")]
    public LearningPath CurrentLearningPath { get; set; } = new();
    [NotMapped]
    public List<LearningPath> CompletedLearningPaths { get; set; } = new();
    public List<FileAttachment> FinalResultAttachments { get; set; } = new();
    public DateTime? ArchivedDate { get; set; }
}
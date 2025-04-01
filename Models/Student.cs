using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FcmsPortal.Models;

public class Student
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
    public Guardian Guardian { get; set; }
    public List<CourseGrade> CourseGrades { get; set; } = new();
    [InverseProperty("StudentsPresent")]
    public List<ClassAttendanceLogEntry> AttendedSessions { get; set; } = new();
    [InverseProperty("StudentsAbsent")]
    public List<ClassAttendanceLogEntry> AbsentSessions { get; set; } = new();
    [Required]
    public int CurrentLearningPathId { get; set; }
    [ForeignKey("CurrentLearningPathId")]
    public LearningPath CurrentLearningPath { get; set; } = new();
    [NotMapped]
    public List<LearningPath> CompletedLearningPaths { get; set; } = new();
}
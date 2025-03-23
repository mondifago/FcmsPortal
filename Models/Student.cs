using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FcmsPortal.Models;

public class Student
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [ForeignKey("PersonId")]
    public Person Person { get; set; }

    [MaxLength(50)]
    public string PositionAmongSiblings { get; set; }

    [MaxLength(200)]
    public string LastSchoolAttended { get; set; }

    public int? GuardianId { get; set; }

    [ForeignKey("GuardianId")]
    public Guardian? Guardian { get; set; }

    [InverseProperty("Student")]
    public List<CourseGrade> CourseGrades { get; set; } = new();

    [InverseProperty("StudentsPresent")]
    public List<ClassAttendanceLogEntry> AttendedSessions { get; set; } = new();

    [InverseProperty("StudentsAbsent")]
    public List<ClassAttendanceLogEntry> AbsentSessions { get; set; } = new();

    [Required]
    public int CurrentLearningPathId { get; set; }

    [ForeignKey("CurrentLearningPathId")]
    public LearningPath CurrentLearningPath { get; set; }

    [NotMapped]
    public List<LearningPath> CompletedLearningPaths { get; set; } = new();
}
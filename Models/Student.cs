using System.ComponentModel.DataAnnotations.Schema;

namespace FcmsPortal.Models;

public class Student
{
    private Person _person;
    public Person Person
    {
        get { return _person; }
        set { _person = value; }
    }

    private int _id;
    public int Id
    {
        get { return _id; }
        set { _id = value; }
    }

    private string _positionAmongSiblings;
    public string PositionAmongSiblings
    {
        get { return _positionAmongSiblings; }
        set { _positionAmongSiblings = value; }
    }

    private string _lastSchoolAttended;
    public string LastSchoolAttended
    {
        get { return _lastSchoolAttended; }
        set { _lastSchoolAttended = value; }
    }

    private DateOnly _dateOfEnrollment;
    public DateOnly DateOfEnrollment
    {
        get { return _dateOfEnrollment; }
        set { _dateOfEnrollment = value; }
    }

    public int? GuardianId { get; set; }

    public Guardian? Guardian { get; set; }

    public List<CourseGrade> CourseGrades { get; set; } = new();
    public List<ClassAttendanceLogEntry> AttendedSessions { get; set; } = new();

    public List<ClassAttendanceLogEntry> AbsentSessions { get; set; } = new();
    public int CurrentLearningPathId { get; set; }
    [ForeignKey(nameof(CurrentLearningPathId))]
    public LearningPath CurrentLearningPath { get; set; }
    public List<LearningPath> CompletedLearningPaths { get; set; } = new();
}

namespace FcmsPortal;

public class Student
{
    private Person _person;
    public Person Person
    {
        get { return _person; }
        set { _person = value; }
    }

    private int _studentId;
    public int StudentId
    {
        get { return _studentId; }
        set { _studentId = value; }
    }

    private string _positionAmoungSiblings;
    public string PositionAmoungSiblings
    {
        get { return _positionAmoungSiblings; }
        set { _positionAmoungSiblings = value; }
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

    private Guardian _guardianInfo;

    public Guardian GuardianInfo
    {
        get { return _guardianInfo; }
        set { _guardianInfo = value; }
    }

    public CourseGrade CourseGrade { get; set; }

}

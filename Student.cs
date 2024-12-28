namespace FcmsPortal;

public class Student
{
    private Person _person;
    public Person Person
    {
        get { return _person; }
        set { _person = value; }
    }

    private int _id;
    public int ID
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

    private Guardian _guardian;
    public Guardian? Guardian
    {
        get { return _guardian; }
        set { _guardian = value; }
    }

    public CourseGrade CourseGrade { get; set; }
}

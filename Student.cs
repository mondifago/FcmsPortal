namespace FcmsPortal;

public class Student
{
    private Person _theStudent;
    public Person TheStudent
    {
        get { return _theStudent; }
        set { _theStudent = value; }
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

    private EducationLevel _educationLevel;
    public EducationLevel EducationLevel
    {
        get { return _educationLevel; }
        set { _educationLevel = value; }
    }

    private EducationLevel _educationLevelSort;

    public EducationLevel EducationLevelSort
    {
        get { return _educationLevelSort; }
        set { _educationLevelSort = value; }
    }

    private ClassLevel _classLevel;
    public ClassLevel ClassLevel
    {
        get { return _classLevel; }
        set { _classLevel = value; }
    }

    private ClassLevel _classLevelSort;
    public ClassLevel ClassLevelSort
    {
        get { return _classLevelSort; }
        set { _classLevelSort = value; }
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

    private Payment _paymentStatus;

    public Payment PaymentStatus
    {
        get { return _paymentStatus; }
        set { _paymentStatus = value; }
    }

    private Subjects _studentSujects;

    public Subjects StudentSubjects
    {
        get { return _studentSujects; }
        set { _studentSujects = value; }
    }

    private Guardian _guardianInfo;

    public Guardian GuardianInfo
    {
        get { return _guardianInfo; }
        set { _guardianInfo = value; }
    }


}

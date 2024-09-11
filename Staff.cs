namespace FcmsPortal;

public class Staff
{
    private Person _theStaff;
    public Person TheStaff
    {
        get { return _theStaff; }
        set { _theStaff = value; }
    }

    private string _adminRole;
    public string AdminRole
    {
        get { return _adminRole; }
        set { _adminRole = value; }
    }

    private bool _hasSystemAccess;
    public bool HasSystemAccess
    {
        get { return _hasSystemAccess; }
        set { _hasSystemAccess = value; }
    }

    private List<string> _qualifications;
    public List<string> Qualifications
    {
        get { return _qualifications; }
        set { _qualifications = value; }
    }

    private List<string> _workExperience;
    public List<string> WorkExperience
    {
        get { return _workExperience; }
        set { _workExperience = value; }
    }

    private DateOnly _dateOfEmployment;
    public DateOnly DateOfEmployment
    {
        get { return _dateOfEmployment; }
        set { _dateOfEmployment = value; }
    }

    private string _nextOfKin;
    public string NextOfKin
    {
        get { return _nextOfKin; }
        set { _nextOfKin = value; }
    }

    private string _nextOfKinContactDetails;
    public string NextOfKinContactDetails
    {
        get { return _nextOfKinContactDetails; }
        set { _nextOfKinContactDetails = value; }
    }

    private Subjects _staffSubjects;

    public Subjects StaffSubjects
    {
        get { return _staffSubjects; }
        set { _staffSubjects = value; }
    }


}



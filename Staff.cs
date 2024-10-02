namespace FcmsPortal;

public class Staff
{
    private Person _theStaff;
    public Person TheStaff
    {
        get { return _theStaff; }
        set { _theStaff = value; }
    }

    private string _jobRole;
    public string JobRole
    {
        get { return _jobRole; }
        set { _jobRole = value; }
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

    private List<Course> _areaOfSpecialization;

    public List<Course> AreaOfSpecialization
    {
        get { return _areaOfSpecialization; }
        set { _areaOfSpecialization = value; }
    }

    private int _staffId;
    public int StaffId
    {
        get { return _staffId; }
        set { _staffId = value; }
    }




}



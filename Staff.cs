namespace FcmsPortal;

public class Staff
{
    private int _id;
    public int Id
    {
        get { return _id; }
        set { _id = value; }
    }

    private Person _person;
    public Person Person
    {
        get { return _person; }
        set { _person = value; }
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

    private string _areaOfSpecialization;
    public string AreaOfSpecialization
    {
        get { return _areaOfSpecialization; }
        set { _areaOfSpecialization = value; }
    }
}



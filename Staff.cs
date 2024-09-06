namespace FcmsPortal;

public class Staff
{
    private Person _theStaff;
    public Person TheStaff
    {
        get { return _theStaff; }
        set { _theStaff = value; }
    }
    
    private Gender _sex;
    public Gender Sex
    {
        get { return _sex; }
        set { _sex = value; }
    }
    
    private DateOnly _dateOfBirth;
    public DateOnly DateOfBirth
    {
        get { return _dateOfBirth; }
        set { _dateOfBirth = value; }
    }
    
    public int Age
    {
        get
        {
            // Calculate the age based on DateOfBirth
            var today = DateOnly.FromDateTime(DateTime.Today);
            int age = today.Year - _dateOfBirth.Year;
            // Adjust the age if the birthday hasn't occurred yet this year
            if (today < _dateOfBirth.AddYears(age))
            {
                age--;
            }

            return age;
        }
    }
    
    private string _stateOfOrigin;
    public string StateOfOrigin
    {
        get { return _stateOfOrigin; }
        set { _stateOfOrigin = value; }
    }
    
    private string _lgaOfOrigin;
    public string LgaOfOrigin
    {
        get { return _lgaOfOrigin; }
        set { _lgaOfOrigin = value; }
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
}



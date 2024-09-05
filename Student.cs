namespace FcmsPortal;

public class Student
{
    private Person _theStudent;

    public Person TheStudent
    {
        get { return _theStudent; }
        set { _theStudent = value; }
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
    
    private int _studentId;

    public int StudentId
    {
        get { return _studentId; }
        set { _studentId = value; }
    }
    
    private Gender _sex;

    public Gender Sex
    {
        get { return _sex; }
        set { _sex = value; }
    }
    
    private string _positionAmoungSiblings;

    public string PositionAmoungSiblings
    {
        get { return _positionAmoungSiblings; }
        set { _positionAmoungSiblings = value; }
    }
    
    
}

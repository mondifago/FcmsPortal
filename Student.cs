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
    
    
}

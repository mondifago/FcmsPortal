namespace FcmsPortal;

public class AdminStaff
{
    private Staff _theAdminStaff;
    public Staff TheAdminStaff
    {
        get { return _theAdminStaff; }
        set { _theAdminStaff = value; }
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
}

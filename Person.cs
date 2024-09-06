namespace FcmsPortal
{
    public class Person
    {
        private byte[] _profilePicture;
        public byte[] ProfilePicture
        {
            get { return _profilePicture; }
            set { _profilePicture = value; }
        }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        private string _middleName;
        public string MiddleName
        {
            get { return _middleName; }
            set { _middleName = value; }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        private List<Address> _homeAddress;
        public List<Address> HomeAddress
        {
            get { return _homeAddress; }
            set { _homeAddress = value; }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private List<string> _phoneNumber;
        public List<string> PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; }
        }

        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }
    }
}

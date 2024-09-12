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

        private List<Address> _address;
        public List<Address> Addresses
        {
            get { return _address; }
            set { _address = value; }
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

        private Calendar _myCalendar;

        public Calendar MyCalendar
        {
            get { return _myCalendar; }
            set { _myCalendar = value; }
        }

        private Attendance _myAttendance;

        public Attendance MyAttendance
        {
            get { return _myAttendance; }
            set { _myAttendance = value; }
        }

        private Curriculum myCurriculum;

        public Curriculum MyCurriculum
        {
            get { return myCurriculum; }
            set { myCurriculum = value; }
        }

        private TestScore _myScore;

        public TestScore MyScore
        {
            get { return _myScore; }
            set { _myScore = value; }
        }

        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }
    }
}

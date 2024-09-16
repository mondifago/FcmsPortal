namespace FcmsPortal
{
    public class Attendance
    {
        public EducationLevel SelectedEducationLevel { get; private set; }
        public object SelectedClassLevel { get; private set; }
        public List<Student> Students { get; private set; }

        private bool _isPresent;
        public bool IsPresent
        {
            get { return _isPresent; }
            set { _isPresent = value; }
        }
    }
}

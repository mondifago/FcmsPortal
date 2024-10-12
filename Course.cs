namespace FcmsPortal
{
    public class Course
    {
        private int _courseId;

        public int CourseId
        {
            get { return _courseId; }
            set { _courseId = value; }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
    }
}

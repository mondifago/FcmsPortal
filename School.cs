namespace FcmsPortal
{
    public class School
    {
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string WebsiteUrl { get; set; }
        public Address Address { get; set; }
        public List<Staff> Staff { get; set; } = new();
        public List<Student> Students { get; set; } = new();
        public List<LearningPath> LearningPath { get; set; } = new();
        public List<Calendar> SchoolCalendar { get; set; }
        public List<CourseGrade> Grades { get; set; } = new();
    }
}

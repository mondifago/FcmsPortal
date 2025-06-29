namespace FcmsPortal.Models
{
    public class School
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? LogoUrl { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string WebsiteUrl { get; set; }
        public Address Address { get; set; }
        public List<Staff> Staff { get; set; } = new();
        public List<Student> Students { get; set; } = new();
        public List<Guardian> Guardians { get; set; } = new();
        public List<LearningPath> LearningPaths { get; set; } = new();
        public List<CalendarModel> SchoolCalendar { get; set; }
        public List<LearningPathGradeReport> GradeReports { get; set; } = new();
    }
}

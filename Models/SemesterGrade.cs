using FcmsPortal.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace FcmsPortal.Models
{
    public class SemesterGrade
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student? Student { get; set; }
        public Semester Semester { get; set; }
        public ClassLevel ClassLevel { get; set; }
        public EducationLevel EducationLevel { get; set; }
        public DateTime AcademicYearStart { get; set; }

        [NotMapped]
        public string AcademicYear => $"{AcademicYearStart.Year}-{AcademicYearStart.Year + 1}";
        public List<CourseGrade> CourseGrades { get; set; } = new();
        public double SemesterOverallGrade { get; set; }
        public int NumberOfCourses { get; set; }
        public bool IsFinalized { get; set; } = false;
        public DateTime? DateFinalized { get; set; }
    }
}

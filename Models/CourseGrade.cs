using FcmsPortal.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace FcmsPortal.Models
{
    public class CourseGrade
    {
        public int Id { get; set; }
        public string Course { get; set; } = string.Empty;
        public List<TestGrade> TestGrades { get; set; } = new();
        public CourseGradingConfiguration? GradingConfiguration { get; set; }
        public double TotalGrade { get; set; }
        public string FinalGradeCode { get; set; } = string.Empty;
        public int StudentId { get; set; }
        public Student? Student { get; set; }
        public int? SemesterGradeId { get; set; }
        public SemesterGrade? SemesterGrade { get; set; }
        public Semester Semester { get; set; }
        public ClassLevel ClassLevel { get; set; }
        public EducationLevel EducationLevel { get; set; }
        public DateTime AcademicYearStart { get; set; }
        [NotMapped]
        public string AcademicYear => $"{AcademicYearStart.Year}-{AcademicYearStart.Year + 1}";
    }
}

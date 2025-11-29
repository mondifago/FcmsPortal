using FcmsPortal.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace FcmsPortal.Models
{
    public class ArchivedSemesterGrade
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public Semester Semester { get; set; }
        public EducationLevel EducationLevel { get; set; }
        public ClassLevel ClassLevel { get; set; }
        public DateTime AcademicYearStart { get; set; }

        [NotMapped]
        public string AcademicYear => $"{AcademicYearStart.Year}-{AcademicYearStart.Year + 1}";
        public double SemesterOverallGrade { get; set; }
        public int Rank { get; set; }
        public int TotalStudentsInClass { get; set; }
        public int NumberOfCourses { get; set; }
        public string PromotionStatus { get; set; } = string.Empty;
        public double PromotionGrade { get; set; }
        public DateTime DateArchived { get; set; }
        public ArchivedStudentReportCard? ReportCard { get; set; }
        public List<ArchivedCourseGrade> CourseGrades { get; set; } = new();
    }
}

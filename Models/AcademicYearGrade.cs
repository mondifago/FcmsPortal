using FcmsPortal.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace FcmsPortal.Models
{
    public class AcademicYearGrade
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student? Student { get; set; }
        public ClassLevel ClassLevel { get; set; }
        public EducationLevel EducationLevel { get; set; }
        public DateTime AcademicYearStart { get; set; }
        [NotMapped]
        public string AcademicYear => $"{AcademicYearStart.Year}-{AcademicYearStart.Year + 1}";
        public int? FirstSemesterGradeId { get; set; }
        public SemesterGrade? FirstSemesterGrade { get; set; }
        public int? SecondSemesterGradeId { get; set; }
        public SemesterGrade? SecondSemesterGrade { get; set; }
        public int? ThirdSemesterGradeId { get; set; }
        public SemesterGrade? ThirdSemesterGrade { get; set; }
        public double AcademicYearCumulativeGrade { get; set; }
        public bool IsPromoted { get; set; }
        public double PromotionGrade { get; set; }
        public int StudentRank { get; set; }
        public int TotalStudentsInClass { get; set; }

        public bool IsFinalized { get; set; }
        public DateTime? DateFinalized { get; set; }
    }
}

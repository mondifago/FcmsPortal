using FcmsPortal.Enums;
using System.ComponentModel.DataAnnotations;

namespace FcmsPortal.Models
{
    public class ArchivedTestGrade
    {
        public int Id { get; set; }
        public int ArchivedCourseGradeId { get; set; }
        public ArchivedCourseGrade? ArchivedCourseGrade { get; set; }
        [Range(0, 100)]
        public double Score { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public GradeType GradeType { get; set; }
    }
}

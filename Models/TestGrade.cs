using FcmsPortal.Enums;
using System.ComponentModel.DataAnnotations;

namespace FcmsPortal.Models
{
    public class TestGrade
    {
        public int Id { get; set; }
        public Staff? Teacher { get; set; }
        [Range(0, 100, ErrorMessage = "Score must be between 0 and 100.")]
        public double Score { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Grade type is required.")]
        public GradeType GradeType { get; set; }

        [StringLength(50, ErrorMessage = "Remark must be 50 characters or fewer.")]
        public string TeacherRemark { get; set; } = string.Empty;

        public int CourseGradeId { get; set; }

        public CourseGrade? CourseGrade { get; set; }
    }
}

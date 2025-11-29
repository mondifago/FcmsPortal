namespace FcmsPortal.Models
{
    public class ArchivedStudentReportCard
    {
        public int Id { get; set; }

        // Parent Reference
        public int ArchivedSemesterGradeId { get; set; }
        public ArchivedSemesterGrade? ArchivedSemesterGrade { get; set; }

        public string TeacherRemarks { get; set; } = string.Empty;
        public string PrincipalRemarks { get; set; } = string.Empty;
        public string PromotionStatus { get; set; } = string.Empty;

        public DateTime DateFinalized { get; set; }
        public string FinalizedByTeacher { get; set; } = string.Empty;
        public string FinalizedByPrincipal { get; set; } = string.Empty;
    }
}

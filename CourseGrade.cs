using FcmsPortal.Constants;

namespace FcmsPortal
{
    public class CourseGrade
    {
        public List<TestGrade> TestGrades { get; set; } = new();
        public double? AttendancePercentage { get; set; }
        public double TotalGrade => ComputeTotalGrade();
        public string FinalGradeCode => ComputeFinalGradeCode();
        public List<FileAttachment> FinalResultAttachments { get; set; } = new();
        private double ComputeTotalGrade()
        {
            if (TestGrades.Count == 0)
                return 0;

            double weightedSum = 0;
            double totalWeight = 0;

            foreach (var test in TestGrades)
            {
                weightedSum += (test.Score / FcmsConstants.TOTAL_SCORE) * test.WeightPercentage;
                totalWeight += test.WeightPercentage;
            }

            return totalWeight > 0 ? Math.Round((weightedSum / totalWeight) * FcmsConstants.TOTAL_SCORE, FcmsConstants.GRADE_ROUNDING_DIGIT) : 0;
        }

        private string ComputeFinalGradeCode()
        {
            double grade = TotalGrade;
            return grade switch
            {
                >= FcmsConstants.A_GRADE_MIN => "A",
                >= FcmsConstants.B_GRADE_MIN => "B",
                >= FcmsConstants.C_GRADE_MIN => "C",
                >= FcmsConstants.D_GRADE_MIN => "D",
                >= FcmsConstants.E_GRADE_MIN => "E",
                _ => "F",
            };
        }
    }
}

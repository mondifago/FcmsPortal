namespace FcmsPortal
{
    public class SeniorCollegeCourses : Course
    {
        public enum Course
        {
            Mathematics,
            EnglishLanguage,
            LiteratureInEnglish,
            Biology,
            Chemistry,
            Physics,
            Economics,
            Commerce,
            FinancialAccounting,
            CivicEducation,
            Crs,
            DataProcessing,
            Geography,
            Government,
            AgriculturalScience
        }
        public Course Subject { get; set; }
        public override string GetCourseName()
        {
            return Subject.ToString();
        }
    }
}

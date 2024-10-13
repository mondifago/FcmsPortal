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
        public Course GetCourse { get; set; }
        public override string GetCourseName()
        {
            return GetCourse.ToString();
        }
    }
}

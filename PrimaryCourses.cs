namespace FcmsPortal
{
    public class PrimaryCourses : Course
    {
        public enum Course
        {
            Mathematics,
            EnglishLanguage,
            BasicScience,
            NationalValues,
            PreVocationalStudies,
            VerbalReasoning,
            QuantitativeReasoning,
            CulturalAndCreativeArt,
            French,
            Crs,
            Phonics
        }
        public Course GetCourse { get; set; }
        public override string GetCourseName()
        {
            return GetCourse.ToString();
        }
    }
}

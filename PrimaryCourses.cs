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
        public Course Subject { get; set; }
        public override string GetCourseName()
        {
            return Subject.ToString();
        }
    }
}

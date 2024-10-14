namespace FcmsPortal
{
    public class JuniorCollegeCourses : Course
    {
        public enum Course
        {
            Mathematics,
            EnglishLanguage,
            PreVocationalStudies,
            NationalValues,
            CulturalAndCreativeArt,
            BasicScienceAndTechnology,
            BusinessStudies,
            Crs,
            History,
            French
        }
        public Course Subject { get; set; }
        public override string GetCourseName()
        {
            return Subject.ToString();
        }
    }
}

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
        public Course GetCourse { get; set; }
        public override string GetCourseName()
        {
            return GetCourse.ToString();
        }
    }
}

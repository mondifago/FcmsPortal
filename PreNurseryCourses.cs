namespace FcmsPortal
{
    public class PreNurseryCourses : Course
    {
        public enum Course
        {
            Mathematics,
            EnglishLanguage,
            GeneralScience,
            SocialHabit,
            CreativeArt,
            PhysicalandHealthHabit,
            Crs,
            Rhymes,
            Phonics,
            Handwriting
        }
        public Course Subject { get; set; }
        public override string GetCourseName()
        {
            return Subject.ToString();
        }
    }
}

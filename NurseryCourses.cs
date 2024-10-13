namespace FcmsPortal
{
    public class NurseryCourses : Course
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
        public Course GetCourse { get; set; }
        public override string GetCourseName()
        {
            return GetCourse.ToString();
        }
    }
}

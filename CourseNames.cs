namespace FcmsPortal
{
    internal class CourseNames
    {
        public static List<string> GetCourseNames(Levels levels)
        {
            switch (levels)
            {
                case Levels.Kindergarten:
                    return new List<string>()
                    {
                        "Maths",
                        "Dancing",
                    };
                    break;
            }
        }
    }
    enum Levels
    {
        Kindergarten,
        prescool,
        primaray
    }
}

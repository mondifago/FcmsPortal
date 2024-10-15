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
                case Levels.PreNursery:
                    return new List<string>()
                    {
                        " dhhsgd",
                        "asjgagdg",
                        "hdsdgj",
                    };
                    break;
            }
        }
    }
    enum Levels
    {
        Kindergarten,
        PreNursery,
        primaray,
        JuniorCollege,
        SeniorCollege
    }
}

namespace FcmsPortal
{
    public class Course
    {
        public static List<string> GetCourseNames(Levels level)
        {
            return level switch
            {
                Levels.Nursery => new List<string>
                {
                    "Mathematics",
                    "EnglishLanguage",
                    "GeneralScience",
                    "SocialHabit",
                    "CreativeArt",
                    "PhysicalandHealthHabit",
                    "Crs",
                    "Rhymes",
                    "Phonics",
                    "Handwriting"
                },
                Levels.PreNursery => new List<string>
                {
                    "Mathematics",
                    "EnglishLanguage",
                    "GeneralScience",
                    "SocialHabit",
                    "CreativeArt",
                    "PhysicalandHealthHabit",
                    "Crs",
                    "Rhymes",
                    "Phonics",
                    "Handwriting"
                },
                Levels.Primary => new List<string>
                {
                   "Mathematics",
                   "EnglishLanguage",
                   "BasicScience",
                   "NationalValues",
                   "PreVocationalStudies",
                   "VerbalReasoning",
                   "QuantitativeReasoning",
                   "CulturalAndCreativeArt",
                   "French",
                   "Crs",
                   "Phonics"
                },
                Levels.JuniorCollege => new List<string>
                {
                   "Mathematics",
                   "EnglishLanguage",
                   "PreVocationalStudies",
                   "NationalValues",
                   "CulturalAndCreativeArt",
                   "BasicScienceAndTechnology",
                   "BusinessStudies",
                   "Crs",
                   "History",
                   "French"
                },
                Levels.SeniorCollege => new List<string>
                {
                   "Mathematics",
                   "EnglishLanguage",
                   "LiteratureInEnglish",
                   "Biology",
                   "Chemistry",
                   "Physics",
                   "Economics",
                   "Commerce",
                   "FinancialAccounting",
                   "CivicEducation",
                   "Crs",
                   "DataProcessing",
                   "Geography",
                   "Government",
                   "AgriculturalScience"
                },
                _ => throw new ArgumentException("Invalid Class level", nameof(level))
            };
        }
    }

    public enum Levels
    {
        Nursery,
        PreNursery,
        Primary,
        JuniorCollege,
        SeniorCollege
    }
}
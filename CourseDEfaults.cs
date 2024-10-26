namespace FcmsPortal
{
    public class CourseDefaults
    {
        public static List<string> GetCourseNames(Enums.EducationLevel educationLevel)
        {
            return educationLevel switch
            {
                Enums.EducationLevel.Kindergarten => new List<string>
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
                Enums.EducationLevel.Primary => new List<string>
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
                Enums.EducationLevel.JuniorCollege => new List<string>
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
                Enums.EducationLevel.SeniorCollege => new List<string>
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
                _ => throw new ArgumentException("Invalid Class educationLevel", nameof(educationLevel))
            };
        }
    }
}
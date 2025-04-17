namespace FcmsPortal.Models
{
    public class CourseDefaults
    {
        public static List<string> GetCourseNames(Enums.EducationLevel educationLevel)
        {
            return educationLevel switch
            {
                Enums.EducationLevel.None => new List<string>(),
                Enums.EducationLevel.Kindergarten => new List<string>
                {
                    "Mathematics",
                    "English Language",
                    "General Science",
                    "Social Habit",
                    "Creative Art",
                    "Physical and Health Habit",
                    "CRS",
                    "Rhymes",
                    "Phonics",
                    "Handwriting"
                },
                Enums.EducationLevel.Primary => new List<string>
                {
                   "Mathematics",
                   "English Language",
                   "Basic Science",
                   "National Values",
                   "PreVocational Studies",
                   "Verbal Reasoning",
                   "Quantitative Reasoning",
                   "Cultural And Creative Art",
                   "French",
                   "CRS",
                   "Phonics"
                },
                Enums.EducationLevel.JuniorCollege => new List<string>
                {
                   "Mathematics",
                   "English Language",
                   "PreVocational Studies",
                   "National Values",
                   "Cultural And Creative Art",
                   "Basic Science And Technology",
                   "Business Studies",
                   "CRS",
                   "History",
                   "French"
                },
                Enums.EducationLevel.SeniorCollege => new List<string>
                {
                   "Mathematics",
                   "English Language",
                   "Literature In English",
                   "Biology",
                   "Chemistry",
                   "Physics",
                   "Economics",
                   "Commerce",
                   "Financial Accounting",
                   "Civic Education",
                   "CRS",
                   "Data Processing",
                   "Geography",
                   "Government",
                   "Agricultural Science"
                },
                _ => throw new ArgumentException("Invalid Class educationLevel", nameof(educationLevel))
            };
        }
    }
}
namespace FcmsPortal
{
    internal class Program
    {
        static void Main(string[] args)
        {
           /* ClassLevel studentClassLevel = new ClassLevel();

            // Set student's class to Primary 3
            studentClassLevel.SetClassLevel(EducationLevel.Primary, Primary.Primary3);

            // Output student's education level and sub-level
            Console.WriteLine($"Student education level: {studentClassLevel.Level}");
            Console.WriteLine($"Student sub-level: {studentClassLevel.SubLevel}");*/
            
            ClassLevel studentClassLevel = new ClassLevel();

            // Ask the user for the education level
            Console.WriteLine("Enter the education level: (0 for Kindergarten, 1 for Primary, 2 for JuniorCollege, 3 for SeniorCollege)");
            string levelInput = Console.ReadLine();
            if (Enum.TryParse<EducationLevel>(levelInput, out EducationLevel educationLevel))
            {
                object subLevel = GetSubLevel(educationLevel);
                studentClassLevel.SetClassLevel(educationLevel, subLevel);

                Console.WriteLine($"Student education level: {studentClassLevel.Level}");
                Console.WriteLine($"Student sub-level: {studentClassLevel.SubLevel}");
            }
            else
            {
                Console.WriteLine("Invalid input for education level.");
            }
            
            static object GetSubLevel(EducationLevel educationLevel)
            {
                switch (educationLevel)
                {
                    case EducationLevel.Kindergarten:
                        Console.WriteLine("Enter the sub-level for Kindergarten: (0 for Daycare, 1 for PlayGroup, 2 for PreNursery, 3 for Nursery)");
                        string kindergartenInput = Console.ReadLine();
                        return Enum.TryParse<Kindergarten>(kindergartenInput, out var kindergartenSubLevel) ? kindergartenSubLevel : null;

                    case EducationLevel.Primary:
                        Console.WriteLine("Enter the sub-level for Primary: (0 for Primary1, 1 for Primary2, 2 for Primary3, 3 for Primary4, 4 for Primary5, 5 for Primary6)");
                        string primaryInput = Console.ReadLine();
                        return Enum.TryParse<Primary>(primaryInput, out var primarySubLevel) ? primarySubLevel : null;

                    case EducationLevel.JuniorCollege:
                        Console.WriteLine("Enter the sub-level for Junior College: (0 for Jss1, 1 for Jss2, 2 for Jss3)");
                        string juniorInput = Console.ReadLine();
                        return Enum.TryParse<JuniorCollege>(juniorInput, out var juniorSubLevel) ? juniorSubLevel : null;

                    case EducationLevel.SeniorCollege:
                        Console.WriteLine("Enter the sub-level for Senior College: (0 for Sss1, 1 for Sss2, 2 for Sss3)");
                        string seniorInput = Console.ReadLine();
                        return Enum.TryParse<SeniorCollege>(seniorInput, out var seniorSubLevel) ? seniorSubLevel : null;

                    default:
                        return null;
                }
            }
        }
    }
}

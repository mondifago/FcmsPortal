namespace FcmsPortal
{
    public class Attendance
    {
        private List<Student> students;
        private Dictionary<int, bool> attendanceRecord;

        public Attendance()
        {
            students = new List<Student>();
            attendanceRecord = new Dictionary<int, bool>();
        }

        public void StartAttendance()
        {
            // Step 1: User selects the education level
            EducationLevel selectedLevel = SelectEducationLevel();

            // Step 2: Sub-levels are populated based on education level
            ClassLevel selectedSubLevel = SelectSubLevel(selectedLevel);

            // Step 3: Retrieve the list of students in the selected level and sub-level
            RetrieveStudents(selectedLevel, selectedSubLevel);

            // Step 4: Display students and mark attendance
            MarkAttendance();

            // Step 5: Review and Submit the attendance to the database, with date and time
            SubmitAttendance();
        }

        private EducationLevel SelectEducationLevel()
        {
            Console.WriteLine("Select Education Level:");
            foreach (EducationLevel level in Enum.GetValues(typeof(EducationLevel)))
            {
                Console.WriteLine($"{(int)level}. {level}");
            }

            int selection;
            while (!int.TryParse(Console.ReadLine(), out selection) || !Enum.IsDefined(typeof(EducationLevel), selection))
            {
                Console.WriteLine("Invalid selection. Please try again.");
            }

            return (EducationLevel)selection;
        }

        private ClassLevel SelectSubLevel(EducationLevel level)
        {
            Console.WriteLine($"Select {level} Sub-Level:");

            Type subLevelType = level switch
            {
                EducationLevel.Kindergarten => typeof(Kindergarten.Classes),
                EducationLevel.Primary => typeof(Primary.Classes),
                EducationLevel.JuniorCollege => typeof(JuniorCollege.Classes),
                EducationLevel.SeniorCollege => typeof(SeniorCollege.Classes),
                _ => throw new ArgumentException("Invalid education level")
            };

            var subLevels = Enum.GetValues(subLevelType);
            for (int i = 0; i < subLevels.Length; i++)
            {
                Console.WriteLine($"{i}. {subLevels.GetValue(i)}");
            }

            int selection;
            while (!int.TryParse(Console.ReadLine(), out selection) || selection < 0 || selection >= subLevels.Length)
            {
                Console.WriteLine("Invalid selection. Please try again.");
            }

            return level switch
            {
                EducationLevel.Kindergarten => new Kindergarten { GetClass = (Kindergarten.Classes)selection },
                EducationLevel.Primary => new Primary { GetClass = (Primary.Classes)selection },
                EducationLevel.JuniorCollege => new JuniorCollege { GetClass = (JuniorCollege.Classes)selection },
                EducationLevel.SeniorCollege => new SeniorCollege { GetClass = (SeniorCollege.Classes)selection },
                _ => throw new ArgumentException("Invalid education level")
            };
        }

        private void RetrieveStudents(EducationLevel level, ClassLevel subLevel)
        {
            // In a real application, this would query a database
            // For this example, we'll create some dummy data
            students = new List<Student>
            {
                new Student { StudentId = 1, TheStudent = new Person { FirstName = "John", LastName = "Doe" }, EducationLevel = level, ClassLevel = subLevel },
                new Student { StudentId = 2, TheStudent = new Person { FirstName = "Jane", LastName = "Smith" }, EducationLevel = level, ClassLevel = subLevel },
                new Student { StudentId = 3, TheStudent = new Person { FirstName = "Alice", LastName = "Johnson" }, EducationLevel = level, ClassLevel = subLevel }
            };
        }

        private void MarkAttendance()
        {
            foreach (var student in students)
            {
                Console.WriteLine($"Is {student.TheStudent.FirstName} {student.TheStudent.LastName} present? (Y/N)");
                string response = Console.ReadLine().ToUpper();
                while (response != "Y" && response != "N")
                {
                    Console.WriteLine("Invalid input. Please enter Y or N.");
                    response = Console.ReadLine().ToUpper();
                }
                attendanceRecord[student.StudentId] = (response == "Y");
            }
        }

        private void SubmitAttendance()
        {
            Console.WriteLine("\nAttendance Summary:");
            foreach (var student in students)
            {
                Console.WriteLine($"{student.TheStudent.FirstName} {student.TheStudent.LastName}: {(attendanceRecord[student.StudentId] ? "Present" : "Absent")}");
            }

            Console.WriteLine("\nDo you want to submit this attendance record? (Y/N)");
            string response = Console.ReadLine().ToUpper();
            while (response != "Y" && response != "N")
            {
                Console.WriteLine("Invalid input. Please enter Y or N.");
                response = Console.ReadLine().ToUpper();
            }

            if (response == "Y")
            {
                // In a real application, this would save to a database
                Console.WriteLine($"Attendance submitted for {DateTime.Now}");
            }
            else
            {
                Console.WriteLine("Attendance submission cancelled.");
            }
        }
    }
}
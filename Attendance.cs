namespace FcmsPortal
{
    public class Attendance
    {
        private List<IAttendee> attendees;
        private Dictionary<IAttendee, bool> attendanceRecord;

        public Attendance()
        {
            attendees = new List<IAttendee>();
            attendanceRecord = new Dictionary<IAttendee, bool>();
        }

        public void StartAttendance(string type)
        {
            Console.WriteLine($"\n--- {type} Attendance System ---");

            // Step 1: Retrieve the list of attendees
            RetrieveAttendees(type);

            // Step 2: Display attendees and mark attendance
            MarkAttendance();

            // Step 3: Review and Submit the attendance to the database, with date and time
            SubmitAttendance(type);
        }

        private void RetrieveAttendees(string type)
        {
            // In a real application, this would query a database
            // For this example, we'll create some dummy data
            if (type.ToLower() == "student")
            {
                attendees = new List<IAttendee>
                {
                    new StudentAttendee { StudentId = 1001, TheStudent = new Person { FirstName = "John", MiddleName = "Michael", LastName = "Doe" }, EducationLevel = EducationLevel.JuniorCollege, ClassLevel = new JuniorCollege { GetClass = JuniorCollege.Classes.Jss2 } },
                    new StudentAttendee { StudentId = 1002, TheStudent = new Person { FirstName = "Jane", MiddleName = "Elizabeth", LastName = "Smith" }, EducationLevel = EducationLevel.Primary, ClassLevel = new Primary { GetClass = Primary.Classes.Primary3 } },
                    new StudentAttendee { StudentId = 1003, TheStudent = new Person { FirstName = "Alice", MiddleName = "Marie", LastName = "Johnson" }, EducationLevel = EducationLevel.SeniorCollege, ClassLevel = new SeniorCollege { GetClass = SeniorCollege.Classes.Sss1 } }
                };
            }
            else if (type.ToLower() == "staff")
            {
                attendees = new List<IAttendee>
                {
                    new StaffAttendee { StaffId = 2001, TheStaff = new Person { FirstName = "Mark", MiddleName = "Robert", LastName = "Taylor" }, AdminRole = "Teacher" },
                    new StaffAttendee { StaffId = 2002, TheStaff = new Person { FirstName = "Sarah", MiddleName = "Jane", LastName = "Brown" }, AdminRole = "Administrator" },
                    new StaffAttendee { StaffId = 2003, TheStaff = new Person { FirstName = "David", MiddleName = "William", LastName = "Wilson" }, AdminRole = "Counselor" }
                };
            }
        }

        private void MarkAttendance()
        {
            foreach (var attendee in attendees)
            {
                Console.WriteLine($"Is {attendee.PersonInfo.FirstName} {attendee.PersonInfo.MiddleName} {attendee.PersonInfo.LastName} (ID: {attendee.Id}, {attendee.Role}) present? (Y/N)");
                string response = Console.ReadLine().ToUpper();
                while (response != "Y" && response != "N")
                {
                    Console.WriteLine("Invalid input. Please enter Y or N.");
                    response = Console.ReadLine().ToUpper();
                }
                attendanceRecord[attendee] = (response == "Y");
            }
        }

        private void SubmitAttendance(string type)
        {
            Console.WriteLine($"\n{type} Attendance Summary:");
            foreach (var attendee in attendees)
            {
                Console.WriteLine($"{attendee.PersonInfo.FirstName} {attendee.PersonInfo.MiddleName} {attendee.PersonInfo.LastName} (ID: {attendee.Id}, {attendee.Role}): {(attendanceRecord[attendee] ? "Present" : "Absent")}");
            }

            Console.WriteLine($"\nDo you want to submit this {type.ToLower()} attendance record? (Y/N)");
            string response = Console.ReadLine().ToUpper();
            while (response != "Y" && response != "N")
            {
                Console.WriteLine("Invalid input. Please enter Y or N.");
                response = Console.ReadLine().ToUpper();
            }

            if (response == "Y")
            {
                // In a real application, this would save to a database
                Console.WriteLine($"{type} attendance submitted for {DateTime.Now}");
            }
            else
            {
                Console.WriteLine($"{type} attendance submission cancelled.");
            }
        }
    }
}

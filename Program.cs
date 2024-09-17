namespace FcmsPortal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the School Management System!");

            while (true)
            {
                Console.WriteLine("\nPlease select an option:");
                Console.WriteLine("1. Take Student Attendance");
                Console.WriteLine("2. Take Staff Attendance");
                Console.WriteLine("3. View Student Information");
                Console.WriteLine("4. View Staff Information");
                Console.WriteLine("5. Exit");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        TakeAttendance("Student");
                        break;
                    case "2":
                        TakeAttendance("Staff");
                        break;
                    case "3":
                        ViewStudentInformation();
                        break;
                    case "4":
                        ViewStaffInformation();
                        break;
                    case "5":
                        Console.WriteLine("Thank you for using the School Management System. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void TakeAttendance(string type)
        {
            Attendance attendance = new Attendance();
            attendance.StartAttendance(type);
        }

        static void ViewStudentInformation()
        {
            Console.WriteLine("\n--- Student Information System ---");
            Console.WriteLine("Enter student ID:");
            if (int.TryParse(Console.ReadLine(), out int studentId))
            {
                // In a real application, this would fetch the student from a database
                // For this example, we'll create a dummy student
                Student student = new Student
                {
                    StudentId = studentId,
                    TheStudent = new Person
                    {
                        FirstName = "John",
                        LastName = "Doe",
                        DateOfBirth = new DateOnly(2005, 5, 15),
                        Sex = Gender.Male
                    },
                    EducationLevel = EducationLevel.JuniorCollege,
                    ClassLevel = new JuniorCollege { GetClass = JuniorCollege.Classes.Jss2 }
                };

                DisplayStudentInfo(student);
            }
            else
            {
                Console.WriteLine("Invalid student ID. Please enter a number.");
            }
        }

        static void DisplayStudentInfo(Student student)
        {
            Console.WriteLine($"\nStudent ID: {student.StudentId}");
            Console.WriteLine($"Name: {student.TheStudent.FirstName} {student.TheStudent.LastName}");
            Console.WriteLine($"Date of Birth: {student.TheStudent.DateOfBirth}");
            Console.WriteLine($"Age: {student.TheStudent.Age}");
            Console.WriteLine($"Gender: {student.TheStudent.Sex}");
            Console.WriteLine($"Education Level: {student.EducationLevel}");
            Console.WriteLine($"Class: {student.ClassLevel.GetLevelName()}");
        }

        static void ViewStaffInformation()
        {
            Console.WriteLine("\n--- Staff Information System ---");
            Console.WriteLine("Enter staff first name:");
            string firstName = Console.ReadLine();
            Console.WriteLine("Enter staff last name:");
            string lastName = Console.ReadLine();

            // In a real application, this would fetch the staff from a database
            // For this example, we'll create a dummy staff
            Staff staff = new Staff
            {
                TheStaff = new Person
                {
                    FirstName = firstName,
                    LastName = lastName,
                    DateOfBirth = new DateOnly(1980, 5, 15),
                    Sex = Gender.Female
                },
                AdminRole = "Teacher",
                HasSystemAccess = true,
                Qualifications = new List<string> { "B.Ed", "M.Ed" },
                WorkExperience = new List<string> { "5 years at XYZ School", "3 years at ABC Academy" },
                DateOfEmployment = new DateOnly(2020, 8, 1),
                NextOfKin = "John Doe",
                NextOfKinContactDetails = "123-456-7890"
            };

            DisplayStaffInfo(staff);
        }

        static void DisplayStaffInfo(Staff staff)
        {
            Console.WriteLine($"\nName: {staff.TheStaff.FirstName} {staff.TheStaff.LastName}");
            Console.WriteLine($"Date of Birth: {staff.TheStaff.DateOfBirth}");
            Console.WriteLine($"Age: {staff.TheStaff.Age}");
            Console.WriteLine($"Gender: {staff.TheStaff.Sex}");
            Console.WriteLine($"Admin Role: {staff.AdminRole}");
            Console.WriteLine($"Has System Access: {staff.HasSystemAccess}");
            Console.WriteLine("Qualifications:");
            foreach (var qualification in staff.Qualifications)
            {
                Console.WriteLine($"- {qualification}");
            }
            Console.WriteLine("Work Experience:");
            foreach (var experience in staff.WorkExperience)
            {
                Console.WriteLine($"- {experience}");
            }
            Console.WriteLine($"Date of Employment: {staff.DateOfEmployment}");
            Console.WriteLine($"Next of Kin: {staff.NextOfKin}");
            Console.WriteLine($"Next of Kin Contact Details: {staff.NextOfKinContactDetails}");
        }



    }
}










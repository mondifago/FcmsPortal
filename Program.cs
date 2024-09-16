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
                Console.WriteLine("1. Take Attendance");
                Console.WriteLine("2. View Student Information");
                Console.WriteLine("3. Exit");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        TakeAttendance();
                        break;
                    case "2":
                        ViewStudentInformation();
                        break;
                    case "3":
                        Console.WriteLine("Thank you for using the School Management System. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void TakeAttendance()
        {
            Console.WriteLine("\n--- Attendance System ---");
            Attendance attendance = new Attendance();
            attendance.StartAttendance();
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
    }
}





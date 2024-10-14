﻿namespace FcmsPortal
{
    internal class Program
    {
        /// <summary>
        /// create one school
        /// create one student
        /// create the student's math teacher
        /// create the student's parent
        /// create primary 3 math session
        /// create an admin staff
        /// </summary>
        /// <param name="args"></param>
        /// 

        static void Main(string[] args)
        {
            // create a school with name and address
            Address address = new Address();
            School fcmSchool = new School();

            fcmSchool.Name = "FCM School";
            fcmSchool.Staff = new List<Staff>();
            fcmSchool.Students = new List<Student>();
            fcmSchool.Guardians = new List<Guardian>();

            fcmSchool.SchoolAddress = address;

            address.Street = "120 City Road";
            address.City = "Asaba";
            address.State = "Delta State";
            address.PostalCode = "P.O.Box 150";
            address.Country = "Nigeria";


            //create a primary 3 student
            Student student1 = new Student();
            student1.StudentId = 304;

            // Initialize GuardianInfo
            student1.GuardianInfo = new Guardian();
            student1.GuardianInfo.RelationshipToStudent = Relationship.Father;

            // Initialize TheGuardian within GuardianInfo
            student1.GuardianInfo.TheGuardian = new Person();
            student1.GuardianInfo.TheGuardian.FirstName = "Adam";
            student1.GuardianInfo.TheGuardian.MiddleName = "Ben";
            student1.GuardianInfo.TheGuardian.LastName = "Mark";

            // Initialize TheStudent
            student1.TheStudent = new Person();
            student1.TheStudent.FirstName = "Lucky";
            student1.TheStudent.MiddleName = "Steve";
            student1.TheStudent.LastName = "Mark";
            student1.TheStudent.Sex = Gender.Male;
            student1.TheStudent.EducationLevel = EducationLevel.Primary;
            student1.TheStudent.ClassLevel = new Primary() { GetCurrentLevel = Primary.Level.Primary3 };

            // Add the student to the school's student list
            fcmSchool.Students.Add(student1);

            //create student's mathematics teacher
            Staff mathTeacher = new Staff();
            Person staff1 = new Person();

            mathTeacher.StaffId = 1234;
            mathTeacher.AreaOfSpecialization = new PrimaryCourses() { GetCourse = PrimaryCourses.Course.Mathematics };
            mathTeacher.JobRole = "Primary Education Teacher";

            mathTeacher.TheStaff = staff1;

            staff1.FirstName = "John";
            staff1.MiddleName = "Michael";
            staff1.LastName = "Smith";
            staff1.Sex = Gender.Male;
            staff1.EducationLevel = EducationLevel.Primary;
            staff1.ClassLevel = new Primary() { GetCurrentLevel = Primary.Level.Primary3 };

            // Add the mathematics teacher to the school's staff list
            fcmSchool.Staff.Add(mathTeacher);

            //create student's Guardian
            Guardian guardian1 = new Guardian();
            guardian1.Occupation = "Engineer";
            guardian1.RelationshipToStudent = Relationship.Father;
            guardian1.ChildInfo = new List<Student>() { student1 };
            guardian1.TheGuardian = new Person() { FirstName = "Adam", MiddleName = "ben", LastName = "Mark" };
            guardian1.TheGuardian.PhoneNumber = new List<string> { "35237522372", "4527245742" };
            guardian1.TheGuardian.SchoolFees = new Schoolfees
            {
                totalamount = 1000.00,
                Payments = new List<Payment>
                {
                    new Payment { Amount = 6.666, Date = new DateTime(1967, 12, 22) }
                }
            };

            //create primary 3 Mathematics class session
            ClassSession multiplicationClass = new ClassSession();
            multiplicationClass.Course = new PrimaryCourses() { GetCourse = PrimaryCourses.Course.Mathematics };
            multiplicationClass.Topic = " Multiplication Table";
            multiplicationClass.Description = " Learning multiplication of single digit numbers";
            multiplicationClass.EducationLevel = EducationLevel.Primary;
            multiplicationClass.ClassLevel = new Primary() { GetCurrentLevel = Primary.Level.Primary3 };
            multiplicationClass.Semester = Semester.First;
            multiplicationClass.Teacher = mathTeacher;
            multiplicationClass.Students = new List<Student>() { student1 };
            multiplicationClass.Duration = TimeSpan.FromMinutes(45);
            multiplicationClass.AttendanceLog = new List<ClassAttendanceLogEntry>
            {
            new ClassAttendanceLogEntry() { Attendees = new List<Student>() {student1} }
            };
            multiplicationClass.Grade = new Grade();
            multiplicationClass.Grade.QuizScore = 100;
        }

    }
}










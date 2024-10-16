namespace FcmsPortal
{
    internal class Program
    {
        /// <summary>
        /// create one school
        /// create one student
        /// create the student's math teacher
        /// create the student's parent
        /// create primary 3 math session
        /// put the multiplication class session for  primary 3 in a schedule
        /// put the multiplication class schedule in a learning path
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

            // Initialize Person within GuardianInfo
            student1.GuardianInfo.Person = new Person();
            student1.GuardianInfo.Person.FirstName = "Adam";
            student1.GuardianInfo.Person.MiddleName = "Ben";
            student1.GuardianInfo.Person.LastName = "Mark";

            // Initialize Person
            student1.Person = new Person();
            student1.Person.FirstName = "Lucky";
            student1.Person.MiddleName = "Steve";
            student1.Person.LastName = "Mark";
            student1.Person.Sex = Gender.Male;
            student1.Person.EducationLevel = EducationLevel.Primary;
            student1.Person.ClassLevel = ClassLevel.Primary3;

            // Add the student to the school's student list
            fcmSchool.Students.Add(student1);

            //create student's mathematics teacher
            Staff mathTeacher = new Staff();
            Person staff1 = new Person();
            mathTeacher.StaffId = 1234;
            mathTeacher.AreaOfSpecialization = Course.GetCourseNames(Levels.Primary)[0];
            mathTeacher.JobRole = "Primary Education Teacher";
            mathTeacher.Person = staff1;
            staff1.FirstName = "John";
            staff1.MiddleName = "Michael";
            staff1.LastName = "Smith";
            staff1.Sex = Gender.Male;
            staff1.EducationLevel = EducationLevel.Primary;
            staff1.ClassLevel = ClassLevel.Primary3;

            // Add the mathematics teacher to the school's staff list
            fcmSchool.Staff.Add(mathTeacher);

            //create student's Guardian
            Guardian guardian1 = new Guardian();
            guardian1.Occupation = "Engineer";
            guardian1.RelationshipToStudent = Relationship.Father;
            guardian1.ChildInfo = new List<Student>() { student1 };
            guardian1.Person = new Person() { FirstName = "Adam", MiddleName = "ben", LastName = "Mark" };
            guardian1.Person.PhoneNumber = new List<string> { "35237522372", "4527245742" };
            guardian1.Person.SchoolFees = new Schoolfees
            {
                totalamount = 1000.00,
                Payments = new List<Payment>
                {
                    new Payment { Amount = 6.666, Date = new DateTime(1967, 12, 22) }
                }
            };

            //create primary 3 Mathematics class session
            ClassSession multiplicationClass = new ClassSession();
            multiplicationClass.Course = Course.GetCourseNames(Levels.Primary)[0];
            multiplicationClass.Topic = " Multiplication Table";
            multiplicationClass.Description = " Learning multiplication of single digit numbers";
            multiplicationClass.Teacher = mathTeacher;
            multiplicationClass.Students = new List<Student>() { student1 };
            multiplicationClass.AttendanceLog = new List<ClassAttendanceLogEntry>
            {
            new ClassAttendanceLogEntry() { Attendees = new List<Student>() {student1} }
            };
            multiplicationClass.Grade = new Grade();
            multiplicationClass.Grade.QuizScore = 100;


            //put the multiplication class session for  primary 3 in a schedule
            ScheduleEntry scheduleEntry1 = new ScheduleEntry();
            scheduleEntry1.Id = 301001;
            scheduleEntry1.DateTime = new DateTime(2024, 12, 15, 09, 00, 00);
            scheduleEntry1.Duration = TimeSpan.FromMinutes(45);
            scheduleEntry1.ClassSession = multiplicationClass;


            //put the multiplication class schedule in a learning path
            LearningPath learningPath1 = new LearningPath();
            learningPath1.Id = 301;
            learningPath1.EducationLevel = EducationLevel.Primary;
            learningPath1.ClassLevel = ClassLevel.Primary3;
            learningPath1.Semester = Semester.First;
            learningPath1.Schedule = new List<ScheduleEntry> { scheduleEntry1 };
            learningPath1.StudentsPaymentSuccesful = new List<Student>() { student1 };

            //create two more class sessions
            var classSession2 = new ClassSession();
            var classSession3 = new ClassSession();


            //create corresponding schedule for them
            var scheduleEntry2 = new ScheduleEntry();
            var scheduleEntry3 = new ScheduleEntry();


        }

    }
}










using FcmsPortal.Enums;

namespace FcmsPortal
{
    internal class Program
    {
        /// <summary>
        /// create one school
        /// create 3 students
        /// create admin staff
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
            fcmSchool.SchoolAddress = address;
            address.Street = "120 City Road";
            address.City = "Asaba";
            address.State = "Delta State";
            address.PostalCode = "P.O.Box 150";
            address.Country = "Nigeria";

            //create student 1
            Student student1 = new Student();
            student1.ID = 301;
            // Initialize Person
            student1.Person = new Person();
            student1.Person.FirstName = "Joe";
            student1.Person.MiddleName = "J";
            student1.Person.LastName = "Jake";
            student1.Person.EducationLevel = EducationLevel.SeniorCollege;
            student1.Person.ClassLevel = ClassLevel.SC_3;
            // Add the student to the school's student list
            fcmSchool.Students.Add(student1);

            //create student 2
            Student student2 = new Student();
            student2.ID = 302;
            student2.Person = new Person();
            student2.Person.FirstName = "Dan";
            student2.Person.MiddleName = "D";
            student2.Person.LastName = "Deen";
            student2.Person.EducationLevel = EducationLevel.SeniorCollege;
            student2.Person.ClassLevel = ClassLevel.SC_3;
            fcmSchool.Students.Add(student2);

            //create student 3
            Student student3 = new Student();
            student3.ID = 303;
            student3.Person = new Person();
            student3.Person.FirstName = "Zac";
            student3.Person.MiddleName = "Z";
            student3.Person.LastName = "Zik";
            student3.Person.EducationLevel = EducationLevel.SeniorCollege;
            student3.Person.ClassLevel = ClassLevel.SC_3;
            fcmSchool.Students.Add(student3);

            //create admin staff
            Staff staff1 = new Staff();
            staff1.Id = 101;
            staff1.Person = new Person();
            staff1.Person.FirstName = "Mr. Fin";
            staff1.Person.MiddleName = "F";
            staff1.Person.LastName = "Fen";
            staff1.JobRole = "Principal";
            fcmSchool.Staff.Add(staff1);

            //create Biology teacher
            Staff staff2 = new Staff();
            staff2.Id = 102;
            staff2.Person = new Person();
            staff2.Person.FirstName = "Mr Eric";
            staff2.Person.MiddleName = "E";
            staff2.Person.LastName = "Een";
            staff2.Person.EducationLevel = EducationLevel.SeniorCollege;
            staff2.Person.ClassLevel = ClassLevel.SC_3;
            staff2.JobRole = "Biology Teacher";
            staff2.AreaOfSpecialization = CourseDefaults.GetCourseNames(EducationLevel.SeniorCollege)[0];
            fcmSchool.Staff.Add(staff2);

            //create Geography teacher
            Staff staff3 = new Staff();
            staff3.Id = 103;
            staff3.Person = new Person();
            staff3.Person.FirstName = "Mrs Qin";
            staff3.Person.MiddleName = "Q";
            staff3.Person.LastName = "Que";
            staff3.Person.EducationLevel = EducationLevel.SeniorCollege;
            staff3.Person.ClassLevel = ClassLevel.SC_3;
            staff3.JobRole = "Geography Teacher";
            staff3.AreaOfSpecialization = CourseDefaults.GetCourseNames(EducationLevel.SeniorCollege)[0];
            fcmSchool.Staff.Add(staff3);

            //retrieving SC_3 students in Senior College based on Education Level and Class level selection
            var sc3Students = LogicMethods.GetStudentsByLevel(fcmSchool, EducationLevel.SeniorCollege, ClassLevel.SC_3);

            Console.WriteLine("SC_3 Students:");
            foreach (var student in sc3Students)
            {
                Console.WriteLine($"ID: {student.ID}, Name: {student.Person.FirstName} {student.Person.MiddleName} {student.Person.LastName}");
            }

            //retrieving teachers who are specialized for handling senior college courses
            var sCStaff = LogicMethods.GetStaffByEducationLevel(fcmSchool, EducationLevel.SeniorCollege);
            Console.WriteLine("Senior College Teachers:");
            foreach (var staff in sCStaff)
            {
                Console.WriteLine($"ID: {staff.Id}, Name: {staff.Person.FirstName} {staff.Person.MiddleName} {staff.Person.LastName}");
            }




            /*

            //put the multiplication class session for  primary 3 in a schedule
            ScheduleEntry scheduleEntry1 = new ScheduleEntry();
            scheduleEntry1.Id = 301001;
            scheduleEntry1.DateTime = new DateTime(2024, 02, 15, 09, 00, 00);
            scheduleEntry1.Duration = TimeSpan.FromMinutes(45);



            //put the multiplication class schedule in a learning path
            LearningPath learningPath1 = new LearningPath();
            learningPath1.Id = 301;
            learningPath1.EducationLevel = EducationLevel.Primary;
            learningPath1.ClassLevel = ClassLevel.PRI_3;
            learningPath1.Semester = 1;
            learningPath1.Schedule = new List<ScheduleEntry> { scheduleEntry1 };
            learningPath1.StudentsPaymentSuccesful = new List<Student>() { student1 };

            //create two more class sessions
            var classSession2 = new ClassSession();
            var classSession3 = new ClassSession();


            //create corresponding schedule for them
            var scheduleEntry2 = new ScheduleEntry();
            var scheduleEntry3 = new ScheduleEntry();

            //put class session 2 and 3 into schedule entry 2 and 3
            scheduleEntry2.ClassSession = classSession2;
            scheduleEntry3.ClassSession = classSession3;

            //add all schedules to the first learning path
            Console.WriteLine(learningPath1.Schedule.Count);

            learningPath1.Schedule.Add(scheduleEntry1);
            learningPath1.Schedule.Add(scheduleEntry2);

            Console.WriteLine(learningPath1.Schedule.Count);

            //add all learning paths to school
            fcmSchool.LearningPath = new List<LearningPath> { learningPath1 };

            //generate curriculum for 
            var curriculum = LogicMethods.GenerateCurriculum(fcmSchool, 2024, ClassLevel.PRI_3, EducationLevel.Primary, 1, 301);
            int year = curriculum.Year;
            Console.WriteLine($"Curriculum for {year}:");
            Console.WriteLine($"Education Level: {curriculum.EducationLevel}");
            Console.WriteLine($"Class Level: {curriculum.ClassLevel}");
            Console.WriteLine($"Year: {curriculum.Year}");
            Console.WriteLine($"Semester: {curriculum.Semester}");
            Console.WriteLine("=============================================");

            foreach (var entry in curriculum.ClassSessions)
            {
                Console.WriteLine($"Course: {entry.Course}, Topic: {entry.Topic}");
                Console.WriteLine($"Lesson Note: {entry.LessonNote}");
                Console.WriteLine($"Teacher's Remark: {entry.TeacherRemarks}");
                Console.WriteLine("------------------------------------------------");
            }
            //student1 make payment of 200 out of 1000
            student1.Person.SchoolFees = new Schoolfees();
            student1.Person.SchoolFees.TotalAmount = 1000.0;
            LogicMethods.MakePayment(student1, 200.0, "Cash");

            LogicMethods.MakePayment(student1, 100.0, "Credit Card");

            //LogicMethods.ShowAllPayments(student1); */
        }

    }
}










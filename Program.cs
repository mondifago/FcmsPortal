using FcmsPortal.Enums;

namespace FcmsPortal
{
    internal class Program
    {
        /// <summary>
        /// create one school
        /// create 3 students
        /// create admin staff
        /// retrieve SC_3 students from school list
        /// retrieve Senior college teachers from school list
        /// create two biology class sessions and assign it to a teacher
        /// create two geography class sessions and assign it to a teacher
        /// 
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
            staff2.AreaOfSpecialization = CourseDefaults.GetCourseNames(EducationLevel.SeniorCollege)[3];
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
            staff3.AreaOfSpecialization = CourseDefaults.GetCourseNames(EducationLevel.SeniorCollege)[12];
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
                Console.WriteLine($"ID: {staff.Id}, Name: {staff.Person.FirstName} {staff.Person.MiddleName} {staff.Person.LastName} Area of specialization: {staff.AreaOfSpecialization}");
            }

            //create a biology class session and assign a teacher to it
            var classSession1 = new ClassSession();
            classSession1.Id = 1;
            classSession1.Course = CourseDefaults.GetCourseNames(EducationLevel.SeniorCollege)[3];
            classSession1.Topic = "Digestive System";
            classSession1.Description = "Function of Organs in Digestive System";
            classSession1.LessonNote = "Make the students understand the function of every organ within the Digestive system";
            classSession1.Teacher = staff2;

            //create the second biology class session
            var classSession2 = new ClassSession();
            classSession2.Id = 2;
            classSession2.Course = CourseDefaults.GetCourseNames(EducationLevel.SeniorCollege)[3];
            classSession2.Topic = "Digestive Sytem";
            classSession2.Description = "Function of Enzymes in Digestive System";
            classSession1.LessonNote = "Make the students understand the function of every Enzyme within the Digestive system";
            classSession1.Teacher = staff2;

            //create a geography class session
            var classSession3 = new ClassSession();
            classSession3.Id = 3;
            classSession3.Course = CourseDefaults.GetCourseNames(EducationLevel.SeniorCollege)[12];
            classSession3.Topic = "The Solar System";
            classSession3.Description = "Planets of the solar System";
            classSession3.LessonNote = "Make the students know the name of the nine planets";
            classSession3.Teacher = staff3;

            //create another geography class session
            var classSession4 = new ClassSession();
            classSession4.Id = 4;
            classSession4.Course = CourseDefaults.GetCourseNames(EducationLevel.SeniorCollege)[12];
            classSession4.Topic = "Map of the earth";
            classSession4.Description = "Introduction to Continents";
            classSession4.LessonNote = "Make the students know the name and location of the all the continents";
            classSession4.Teacher = staff3;
            
            //create schedule for all existing class sessions
            ScheduleEntry scheduleEntry1 = new ScheduleEntry();
            scheduleEntry1.Id = 11;
            scheduleEntry1.DateTime = new DateTime(2026, 04, 15, 10, 00, 00);
            scheduleEntry1.Duration = TimeSpan.FromMinutes(30);
            scheduleEntry1.ClassSession = classSession1;

            ScheduleEntry scheduleEntry2 = new ScheduleEntry();
            scheduleEntry2.Id = 22;
            scheduleEntry2.DateTime = new DateTime(2026, 04, 17, 10, 00, 00);
            scheduleEntry2.Duration = TimeSpan.FromMinutes(30);
            scheduleEntry2.ClassSession = classSession2;

            ScheduleEntry scheduleEntry3 = new ScheduleEntry();
            scheduleEntry3.Id = 33;
            scheduleEntry3.DateTime = new DateTime(2026, 04, 15, 11, 00, 00);
            scheduleEntry3.Duration = TimeSpan.FromMinutes(30);
            scheduleEntry3.ClassSession = classSession3;

            ScheduleEntry scheduleEntry4 = new ScheduleEntry();
            scheduleEntry4.Id = 44;
            scheduleEntry4.DateTime = new DateTime(2026, 04, 17, 09, 00, 00);
            scheduleEntry4.Duration = TimeSpan.FromMinutes(30);
            scheduleEntry4.ClassSession = classSession4;
            
            //put all the schedule in a learning path
            LearningPath learningPath1 = new LearningPath();
            learningPath1.Id = 3301;
            learningPath1.EducationLevel = EducationLevel.SeniorCollege;
            learningPath1.ClassLevel = ClassLevel.SC_3;
            learningPath1.Semester = 1;
            learningPath1.Schedule = new List<ScheduleEntry> { scheduleEntry1, scheduleEntry2, scheduleEntry3, scheduleEntry4 };
            learningPath1.Students = new List<Student>() { student1, student2, student3 };
            
            //LogicMethods.AddScheduleToLearningPath(learningPath1,scheduleEntry1);
            Console.WriteLine($"Learning Path {learningPath1.Id} now contains {learningPath1.Schedule.Count} schedule(s).");









            /*


            

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










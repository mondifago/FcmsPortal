﻿using FcmsPortal.Enums;

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
            fcmSchool.LearningPath = new List<LearningPath>();
            fcmSchool.SchoolCalendar = new List<Calendar>();
            fcmSchool.Address = address;
            address.Street = "120 City Road";
            address.City = "Asaba";
            address.State = "Delta State";
            address.PostalCode = "P.O.Box 150";
            address.Country = "Nigeria";
            Calendar allCalendar = new Calendar();
            allCalendar.Id = 2024;
            allCalendar.Name = "2024 Calendar";
            allCalendar.ScheduleEntries = new List<ScheduleEntry>();
            fcmSchool.SchoolCalendar.Add(allCalendar);
            

            //create student 1
            Student student1 = new Student();
            student1.ID = 301;
            student1.GuardianId = 3011;
            // Initialize Person
            student1.Person = new Person();
            student1.Person.FirstName = "Joe";
            student1.Person.MiddleName = "J";
            student1.Person.LastName = "Jake";
            student1.Person.EducationLevel = EducationLevel.SeniorCollege;
            student1.Person.ClassLevel = ClassLevel.SC_3;
            student1.Person.PersonalCalendar = new Calendar();
            student1.Person.PersonalCalendar.Id = 177;
            student1.Person.PersonalCalendar.Name = "Student1's Study Calendar";
            student1.Person.PersonalCalendar.ScheduleEntries = new List<ScheduleEntry>();
            student1.Guardian = new Guardian();
            student1.Guardian.Id = 3011;
            student1.Guardian.RelationshipToStudent = Relationship.Father;
            student1.Guardian.Occupation = "Engineer";
            student1.Guardian.Person.LastName = "Mr. Jake";
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
            student2.Person.PersonalCalendar = new Calendar();
            student2.Person.PersonalCalendar.Id = 277;
            student2.Person.PersonalCalendar.Name = "Student2's Study Calendar";
            student2.Person.PersonalCalendar.ScheduleEntries = new List<ScheduleEntry>();
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
            student3.Person.PersonalCalendar = new Calendar();
            student3.Person.PersonalCalendar.Id = 377;
            student3.Person.PersonalCalendar.Name = "Student3's Study Calendar";
            student3.Person.PersonalCalendar.ScheduleEntries = new List<ScheduleEntry>();
            fcmSchool.Students.Add(student3);

            //create admin staff
            Staff staff1 = new Staff();
            staff1.Id = 101;
            staff1.Person = new Person();
            staff1.Person.FirstName = "Mr. Fin";
            staff1.Person.MiddleName = "F";
            staff1.Person.LastName = "Fen";
            staff1.JobRole = JobRole.Admin;
            staff1.JobDescription = "Principal";
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
            staff2.JobRole = JobRole.Teacher;
            staff2.JobDescription = "Biology Teacher";
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
            staff3.JobRole = JobRole.Teacher;
            staff3.JobDescription = "Geography Teacher";
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
            
            //retrieving All the teachers in the school
            var allTeachers = LogicMethods.GetAllTeachers(fcmSchool);
            foreach (var teacher in allTeachers)
            {
                Console.WriteLine($"ID: {teacher.Id}, Name: {teacher.Person.FirstName} {teacher.Person.LastName} ........ {teacher.AreaOfSpecialization}");
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
            classSession2.Topic = "Digestive System";
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
            scheduleEntry1.Title = "Biology Class";
            scheduleEntry1.Venue = "SSS3A Classroom";

            ScheduleEntry scheduleEntry2 = new ScheduleEntry();
            scheduleEntry2.Id = 22;
            scheduleEntry2.DateTime = new DateTime(2026, 04, 17, 10, 00, 00);
            scheduleEntry2.Duration = TimeSpan.FromMinutes(30);
            scheduleEntry2.ClassSession = classSession2;
            scheduleEntry2.Title = "Biology Class";
            scheduleEntry2.Venue = "SSS3A Classroom";

            ScheduleEntry scheduleEntry3 = new ScheduleEntry();
            scheduleEntry3.Id = 33;
            scheduleEntry3.DateTime = new DateTime(2026, 04, 15, 11, 00, 00);
            scheduleEntry3.Duration = TimeSpan.FromMinutes(30);
            scheduleEntry3.ClassSession = classSession3;
            scheduleEntry3.Title = "Geography Class";
            scheduleEntry3.Venue = "SSS3A Classroom";

            ScheduleEntry scheduleEntry4 = new ScheduleEntry();
            scheduleEntry4.Id = 44;
            scheduleEntry4.DateTime = new DateTime(2026, 04, 17, 09, 00, 00);
            scheduleEntry4.Duration = TimeSpan.FromMinutes(30);
            scheduleEntry4.ClassSession = classSession4;
            scheduleEntry4.Title = "Geography Class";
            scheduleEntry4.Venue = "SSS3A Classroom";
            
            //put all the schedule in a learning path
            LearningPath learningPath1 = new LearningPath();
            learningPath1.Id = 3301;
            learningPath1.EducationLevel = EducationLevel.SeniorCollege;
            learningPath1.ClassLevel = ClassLevel.SC_3;
            learningPath1.Semester = 1;
            learningPath1.Schedule = new List<ScheduleEntry> { scheduleEntry1, scheduleEntry2, scheduleEntry3, scheduleEntry4 };
            learningPath1.Students = new List<Student>() { student1, student2, student3 };
            fcmSchool.LearningPath.Add(learningPath1);
            
            //learningpath1 is added again for test and detected to have been added before
            //LogicMethods.AddAScheduleToLearningPath(learningPath1,scheduleEntry1);
            
            Console.WriteLine($"Learning Path {learningPath1.Id} now contains {learningPath1.Schedule.Count} schedule(s).");
            
            //testing time overlap
            var schedule1 = new ScheduleEntry { Id = 101, DateTime = DateTime.Now, Duration = TimeSpan.FromHours(1), ClassSession = new ClassSession() };
            var schedule2 = new ScheduleEntry { Id = 102, DateTime = DateTime.Now.AddHours(2), Duration = TimeSpan.FromHours(1), ClassSession = new ClassSession() };
            var schedule3 = new ScheduleEntry { Id = 103, DateTime = DateTime.Now, Duration = TimeSpan.FromHours(1), ClassSession = new ClassSession() };
            
            try
            {
                LogicMethods.AddAScheduleToLearningPath(learningPath1, schedule1); // Success
                LogicMethods.AddAScheduleToLearningPath(learningPath1, schedule2); // Success
                LogicMethods.AddAScheduleToLearningPath(learningPath1, schedule3); // Error - overlaps with schedule1
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            
            Console.WriteLine("Updated Schedules:");
            foreach (var schedule in learningPath1.Schedule)
            {
                Console.WriteLine($"ID: {schedule.Id}, Date: {schedule.DateTime}, Duration: {schedule.Duration}");
            }
            
            var learningPath2 = new LearningPath
            {
                Id = 4401,
                EducationLevel = EducationLevel.Primary,
                ClassLevel = ClassLevel.PRI_1,
                Semester = 1,
                Schedule = new List<ScheduleEntry>()
            };
            // Add multiple schedules
            try
            {
                var schedulesToAdd = new List<ScheduleEntry> { schedule1, schedule2, schedule3 };
                LogicMethods.AddMultipleSchedulesToLearningPath(learningPath2, schedulesToAdd); // Error due to overlap with schedule1
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            // Display updated schedules
            Console.WriteLine("Updated Schedules:");
            foreach (var schedule in learningPath2.Schedule)
            {
                Console.WriteLine($"ID: {schedule.Id}, Date: {schedule.DateTime}, Duration: {schedule.Duration}");
            }
            
            //testing add learning path to school: adding learningpath2 which has not been added - success
            try
            {
                LogicMethods.AddLearningPathToSchool(fcmSchool, learningPath2);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            
            //add learningpath1 which has been added - fail
            try
            {
                LogicMethods.AddLearningPathToSchool(fcmSchool, learningPath1);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            
            //Create an Event, Annual Sports day
            ScheduleEntry eventEntry = new ScheduleEntry();
            eventEntry.Id = 20202;
            eventEntry.DateTime = new DateTime(2026, 12, 20, 12, 00,00);
            eventEntry.Duration = TimeSpan.FromHours(2);
            eventEntry.Venue = "School Sports Field";
            eventEntry.Event = "Annual Sport day";
            eventEntry.Notes = "All students and staff to wear school sports attire";
            allCalendar.ScheduleEntries.Add(eventEntry);
            
            //Create a meeting for a Teachers with the Principal
            ScheduleEntry meetingEntry = new ScheduleEntry();
            meetingEntry.Id = 3098;
            meetingEntry.DateTime = DateTime.Today;
            meetingEntry.Duration = TimeSpan.FromMinutes(20);
            meetingEntry.Venue = "Staff Room";
            meetingEntry.Meeting = "Academic Staff Meeting";
            meetingEntry.Notes = "Agenda: Weekly Academic Progress Evaluation";
            allCalendar.ScheduleEntries.Add(meetingEntry);
            //make staff meeting a recurring feature
            meetingEntry.IsRecurring = true;
            meetingEntry.RecurrencePattern = RecurrenceType.Weekly;
            meetingEntry.RecurrenceInterval = 1;
            meetingEntry.DateTime = DateTime.Now.AddMonths(2);
            
            List<ScheduleEntry> recurringSchedules = LogicMethods.GenerateRecurringSchedules(meetingEntry);
            foreach (var schedule in recurringSchedules)
            {
                Console.WriteLine($"{schedule.DateTime}: {schedule.Title} at {schedule.Venue}");
            }
            
            
            
            
            
            
            //add fee for learning path 1
            learningPath1.FeePerSemester = 100.0;
            
            //assign semester fee to each student in learning path1
            LogicMethods.AssignSemesterFeesToStudents(learningPath1);

            foreach (var student in learningPath1.Students)
            {
                Console.WriteLine($"Student: {student.Person.FirstName} - {student.Person.SchoolFees.TotalAmount}");
            }

            Student student5 = new Student();
            student5.ID = 304;
            student5.Person = new Person();
            student5.Person.FirstName = "Kevin";
            
            learningPath1.Students.Add(student5);
            
            //Console.WriteLine($"Name: {student5.Person.FirstName} - Fees: {student5.Person.SchoolFees.TotalAmount}");
            //above created new student Kevin does not have a set school fees Amount because event has to happen automatically
            
            
            
            
           

            foreach (var scheduleEntry in learningPath1.Schedule)
            {
                Console.WriteLine($"{scheduleEntry.Id}: {scheduleEntry.DateTime} - {scheduleEntry.Title}, {scheduleEntry.Duration}");
            }
            
            LogicMethods.DisplayStudentSchedules(learningPath1);
            
            // Synchronize schedules
            LogicMethods.SynchronizeSchedulesWithStudents(learningPath1);
            
            LogicMethods.DisplayStudentSchedules(learningPath1);
            var calendar = LogicMethods.GenerateStudentCalendar(fcmSchool, student1);

            // Display the student's calendar
            Console.WriteLine($"Calendar for {student1.Person.FirstName} {student1.Person.LastName}:");
            foreach (var entry in calendar)
            {
                Console.WriteLine($"Date: {entry.DateTime}, Course: {entry.ClassSession.Course}, Topic: {entry.ClassSession.Topic}");
            }

            












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










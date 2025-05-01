using FcmsPortal.Enums;
using FcmsPortal.Models;

namespace FcmsPortal
{
    public class Program
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

        public static School CreateSchool()
        {
            // create a school with name and address
            Address address = new Address();
            School fcmSchool = new School();
            fcmSchool.Id = 1;
            fcmSchool.Name = "Future Champions Model School";
            fcmSchool.Staff = new List<Staff>();
            fcmSchool.Students = new List<Student>();
            fcmSchool.LearningPath = new List<LearningPath>();
            fcmSchool.SchoolCalendar = new List<Calendar>();
            fcmSchool.Guardians = new List<Guardian>();
            fcmSchool.Curricula = new List<Curriculum>();
            fcmSchool.LogoUrl = "";
            fcmSchool.Email = "info@fcms.com";
            fcmSchool.PhoneNumber = "08012345678";
            fcmSchool.WebsiteUrl = "www.fcms.com";
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

            //create guardian 1
            Guardian guardian1 = new Guardian();
            guardian1.Id = 3011;
            guardian1.RelationshipToStudent = Relationship.Father;
            guardian1.Occupation = "Engineer";
            guardian1.Wards = new List<Student>();
            guardian1.Person = new Person();
            guardian1.Person.FirstName = "Jason";
            guardian1.Person.MiddleName = "C";
            guardian1.Person.LastName = "Jake";
            guardian1.Person.IsActive = true;
            guardian1.Person.Sex = Gender.Male;
            guardian1.Person.StateOfOrigin = "Anambra State";
            guardian1.Person.LgaOfOrigin = "Ihiala";
            guardian1.Person.Email = "cjake@fcms.com";
            guardian1.Person.PhoneNumber = "08012345558";
            guardian1.Person.DateOfEnrollment = DateTime.Today;
            guardian1.Person.Addresses = new List<Address>();
            guardian1.Person.Addresses.Add(new Address
            {
                Street = "12, City Road",
                City = "Asaba",
                State = "Delta State",
                PostalCode = "P.O.Box 150",
                Country = "Nigeria",
                AddressType = AddressType.Home
            });
            guardian1.Person.Addresses.Add(new Address
            {
                Street = "45, Market Street",
                City = "Benin City",
                State = "Edo State",
                PostalCode = "P.O.Box 200",
                Country = "Nigeria",
                AddressType = AddressType.Office
            });
            fcmSchool.Guardians.Add(guardian1);

            //create student 1
            Student student1 = new Student();
            student1.Id = 301;
            student1.GuardianId = 3011;
            // Initialize Person
            student1.Person = new Person();
            student1.Person.FirstName = "Joe";
            student1.Person.MiddleName = "J";
            student1.Person.LastName = "Jake";
            student1.Person.DateOfBirth = new DateTime(2009, 01, 01);
            student1.Person.EducationLevel = EducationLevel.SeniorCollege;
            student1.Person.ClassLevel = ClassLevel.SC_3;
            student1.Person.IsActive = true;
            student1.Person.PersonalCalendar = new Calendar();
            student1.Person.PersonalCalendar.Id = 177;
            student1.Person.PersonalCalendar.Name = "Student1's Study Calendar";
            student1.Person.PersonalCalendar.ScheduleEntries = new List<ScheduleEntry>();
            student1.Person.SchoolFees = new SchoolFees();
            LogicMethods.AddStudentToGuardianWards(guardian1, student1);
            LogicMethods.AddStudentToSchool(fcmSchool, student1);
            LogicMethods.MakePaymentForStudent(student1, 30000, PaymentMethod.Cash);

            //create student 4
            Student student4 = new Student();
            student4.Id = 304;
            student4.GuardianId = 3011;
            student4.Person = new Person();
            student4.Person.FirstName = "John";
            student4.Person.MiddleName = "C";
            student4.Person.LastName = "Jake";
            student4.Person.DateOfBirth = new DateTime(2020, 07, 20);
            student4.Person.EducationLevel = EducationLevel.Kindergarten;
            student4.Person.ClassLevel = ClassLevel.KG_Nursery;
            student4.Person.IsActive = true;
            LogicMethods.AddStudentToGuardianWards(guardian1, student4);
            LogicMethods.AddStudentToSchool(fcmSchool, student4);

            //create guardian 2
            Guardian guardian2 = new Guardian();
            guardian2.Id = 3021;
            guardian2.Occupation = "Doctor";
            guardian2.RelationshipToStudent = Relationship.Mother;
            guardian2.Wards = new List<Student>();
            guardian2.Person = new Person();
            guardian2.Person.FirstName = "Diana";
            guardian2.Person.MiddleName = "D";
            guardian2.Person.LastName = "Deen";
            guardian2.Person.IsActive = true;
            guardian2.Person.Sex = Gender.Female;
            guardian2.Person.StateOfOrigin = "Anambra State";
            guardian2.Person.LgaOfOrigin = "Nnewi";
            guardian2.Person.Email = "ddeen@fcms.com";
            guardian2.Person.PhoneNumber = "08033345559";
            guardian2.Person.DateOfEnrollment = DateTime.Today;
            guardian2.Person.Addresses = new List<Address>();
            guardian2.Person.Addresses.Add(new Address
            {
                Street = "34, Green Avenue",
                City = "Lagos",
                State = "Lagos State",
                PostalCode = "P.O.Box 200",
                Country = "Nigeria",
                AddressType = AddressType.Home
            });
            fcmSchool.Guardians.Add(guardian2);

            //create student 2
            Student student2 = new Student();
            student2.Id = 302;
            student2.GuardianId = 3021;
            student2.Person = new Person();
            student2.Person.FirstName = "Dan";
            student2.Person.MiddleName = "D";
            student2.Person.LastName = "Deen";
            student2.Person.DateOfBirth = new DateTime(2011, 06, 13);
            student2.Person.EducationLevel = EducationLevel.SeniorCollege;
            student2.Person.ClassLevel = ClassLevel.SC_3;
            student2.Person.IsActive = true;
            student2.Person.PersonalCalendar = new Calendar();
            student2.Person.PersonalCalendar.Id = 277;
            student2.Person.PersonalCalendar.Name = "Student2's Study Calendar";
            student2.Person.PersonalCalendar.ScheduleEntries = new List<ScheduleEntry>();
            LogicMethods.AddStudentToGuardianWards(guardian2, student2);
            LogicMethods.AddStudentToSchool(fcmSchool, student2);

            //create guardian 3
            Guardian guardian3 = new Guardian();
            guardian3.Id = 3031;
            guardian3.Occupation = "professor";
            guardian3.RelationshipToStudent = Relationship.Father;
            guardian3.Wards = new List<Student>();
            guardian3.Person = new Person();
            guardian3.Person.FirstName = "Zok";
            guardian3.Person.MiddleName = "Z";
            guardian3.Person.LastName = "Zik";
            guardian3.Person.IsActive = true;
            guardian3.Person.Sex = Gender.Male;
            guardian3.Person.StateOfOrigin = "Anambra State";
            guardian3.Person.LgaOfOrigin = "Awka";
            guardian3.Person.Email = "zikzok@fcms.com";
            guardian3.Person.PhoneNumber = "08033348811";
            guardian3.Person.DateOfEnrollment = DateTime.Today;
            guardian3.Person.Addresses = new List<Address>();
            guardian3.Person.Addresses.Add(new Address
            {
                Street = "56, Hospital Road",
                City = "Abeokuta",
                State = "Ogun State",
                PostalCode = "P.O.Box 300",
                Country = "Nigeria",
                AddressType = AddressType.Home
            });
            fcmSchool.Guardians.Add(guardian3);

            //create student 3
            Student student3 = new Student();
            student3.Id = 303;
            student3.GuardianId = 3031;
            student3.Person = new Person();
            student3.Person.FirstName = "Zac";
            student3.Person.MiddleName = "Z";
            student3.Person.LastName = "Zik";
            student3.Person.DateOfBirth = new DateTime(2010, 08, 23);
            student3.Person.EducationLevel = EducationLevel.SeniorCollege;
            student3.Person.ClassLevel = ClassLevel.SC_3;
            student3.Person.IsActive = true;
            student3.Person.PersonalCalendar = new Calendar();
            student3.Person.PersonalCalendar.Id = 377;
            student3.Person.PersonalCalendar.Name = "Student3's Study Calendar";
            student3.Person.PersonalCalendar.ScheduleEntries = new List<ScheduleEntry>();
            LogicMethods.AddStudentToGuardianWards(guardian3, student3);
            LogicMethods.AddStudentToSchool(fcmSchool, student3);

            //create admin staff
            Staff staff1 = new Staff();
            staff1.Id = 101;
            staff1.JobRole = JobRole.Admin;
            staff1.JobDescription = "Principal";
            staff1.Qualifications = new() { "B.Edu", "PhD" };
            staff1.WorkExperience = new() { "Glender High School 2000-2007", "Fountain School 2007-2024" };
            staff1.DateOfEmployment = DateTime.Today;
            staff1.Person = new Person();
            staff1.Person.FirstName = "Fin";
            staff1.Person.MiddleName = "F";
            staff1.Person.LastName = "Fen";
            staff1.Person.Email = "fin.fen@fcms.com";
            staff1.Person.PhoneNumber = "0804883344";
            staff1.Person.DateOfBirth = new DateTime(1980, 05, 23);
            staff1.Person.Sex = Gender.Male;
            staff1.Person.StateOfOrigin = "Anambra State";
            staff1.Person.LgaOfOrigin = "Ihiala";
            staff1.Person.IsActive = true;
            staff1.Person.Addresses = new List<Address>();
            staff1.Person.Addresses.Add(new Address
            {
                Street = "123, Corporate Drive",
                City = "Lagos",
                State = "Lagos State",
                PostalCode = "P.O.Box 101",
                Country = "Nigeria",
                AddressType = AddressType.HomeTown
            });
            fcmSchool.Staff.Add(staff1);

            //create Biology teacher
            Staff staff2 = new Staff();
            staff2.Id = 102;
            staff2.JobRole = JobRole.Teacher;
            staff2.JobDescription = "Biology Teacher";
            staff2.AreaOfSpecialization = CourseDefaults.GetCourseNames(EducationLevel.SeniorCollege)[3];
            staff2.Qualifications = new() { "B.Edu", "TTc" };
            staff2.WorkExperience = new() { "Sloke High School 2003-2010", "Novel School 2010-2024" };
            staff2.DateOfEmployment = DateTime.Today;
            staff2.Person = new Person();
            staff2.Person.FirstName = "Eric";
            staff2.Person.MiddleName = "E";
            staff2.Person.LastName = "Een";
            staff2.Person.Email = "eric.een@fcms.com";
            staff2.Person.PhoneNumber = "0803003344";
            staff2.Person.DateOfBirth = new DateTime(1980, 05, 23);
            staff2.Person.Sex = Gender.Male;
            staff2.Person.StateOfOrigin = "Anambra State";
            staff2.Person.LgaOfOrigin = "Nnewi";
            staff2.Person.EducationLevel = EducationLevel.SeniorCollege;
            staff2.Person.ClassLevel = ClassLevel.SC_3;
            staff2.Person.IsActive = true;
            staff2.Person.Addresses = new List<Address>();
            staff2.Person.Addresses.Add(new Address
            {
                Street = "456, Business Center",
                City = "Abuja",
                State = "FCT",
                PostalCode = "P.O.Box 102",
                Country = "Nigeria",
                AddressType = AddressType.Office
            });
            fcmSchool.Staff.Add(staff2);

            //create Geography teacher
            Staff staff3 = new Staff();
            staff3.Id = 103;
            staff3.JobRole = JobRole.Teacher;
            staff3.JobDescription = "Geography Teacher";
            staff3.AreaOfSpecialization = CourseDefaults.GetCourseNames(EducationLevel.SeniorCollege)[12];
            staff3.Qualifications = new() { "B.Edu", "TTc" };
            staff3.WorkExperience = new() { "Sloke High School 2003-2010", "Novel School 2010-2024" };
            staff3.DateOfEmployment = DateTime.Today;
            staff3.Person = new Person();
            staff3.Person.FirstName = "Qin";
            staff3.Person.MiddleName = "Q";
            staff3.Person.LastName = "Que";
            staff3.Person.Email = "qin.que@fcms.com";
            staff3.Person.PhoneNumber = "0801993311";
            staff3.Person.DateOfBirth = new DateTime(1985, 07, 23);
            staff3.Person.Sex = Gender.Female;
            staff3.Person.StateOfOrigin = "Anambra State";
            staff3.Person.LgaOfOrigin = "Nnewi";
            staff3.Person.EducationLevel = EducationLevel.SeniorCollege;
            staff3.Person.ClassLevel = ClassLevel.SC_3;
            staff3.Person.IsActive = true;
            staff3.Person.Addresses = new List<Address>();
            staff3.Person.Addresses.Add(new Address
            {
                Street = "789, Market Square",
                City = "Port Harcourt",
                State = "Rivers State",
                PostalCode = "P.O.Box 103",
                Country = "Nigeria",
                AddressType = AddressType.Office
            });
            fcmSchool.Staff.Add(staff3);

            //create a biology class session and assign a teacher to it
            var classSession1 = new ClassSession();
            classSession1.Id = 1;
            classSession1.Course = CourseDefaults.GetCourseNames(EducationLevel.SeniorCollege)[3];
            classSession1.Topic = "Digestive System";
            classSession1.Description = "Function of Organs in Digestive System";
            classSession1.LessonPlan =
                "Make the students understand the function of every organ within the Digestive system";
            classSession1.Teacher = staff2;
            classSession1.AttendanceLog = new List<ClassAttendanceLogEntry>();
            classSession1.HomeworkDetails = new List<Homework>();
            var homework1 = new Homework();
            homework1.Id = 1;
            homework1.Title = "Digestive System Homework";
            homework1.AssignedDate = DateTime.Today;
            homework1.DueDate = DateTime.Today.AddDays(7);
            homework1.ClassSessionId = classSession1.Id;
            homework1.Question = "Explain the function of the stomach in the digestive system.";
            homework1.Submissions = new List<HomeworkSubmission>();
            var homeworkSubmission1 = new HomeworkSubmission();
            homeworkSubmission1.Id = 1;
            homeworkSubmission1.Homework = homework1;
            homeworkSubmission1.Student = student1;
            //homeworkSubmission1.Student.Id = student1.Id;
            homeworkSubmission1.SubmissionDate = DateTime.Today.AddDays(1);
            homeworkSubmission1.Answer = "The stomach is a muscular organ that mixes food with gastric juices, breaking it down into a semi-liquid form called chyme. It also plays a role in the digestion of proteins and the absorption of certain nutrients.";
            homeworkSubmission1.IsGraded = false;
            homework1.Submissions.Add(homeworkSubmission1);
            classSession1.HomeworkDetails.Add(homework1);

            classSession1.StudyMaterials = new List<FileAttachment>();
            classSession1.DiscussionThreads = new List<DiscussionThread>();
            classSession1.DiscussionThreads.Clear();
            // Create a classroom discussion thread started by student1
            var biologyClassThread = LogicMethods.StartDiscussion(
                1,
                1,
                student1.Person,
                "Can someone help explain the digestive enzymes we learned about in biology class today? I'm having trouble understanding the difference between amylase, protease, and lipase."
            );

            // Add replies from other students
            LogicMethods.AddReply(
                biologyClassThread,
                2,
                student2.Person,
                "Sure, I can help! Amylase breaks down carbohydrates into sugars, protease breaks down proteins into amino acids, and lipase breaks down fats into fatty acids and glycerol. Each works at different parts of the digestive system."
            );

            LogicMethods.AddReply(
                biologyClassThread,
                3,
                student3.Person,
                "To add to what Dan said, amylase is found in saliva and starts working in the mouth. Protease works mainly in the stomach and small intestine (like pepsin and trypsin). Lipase is mostly in the small intestine and pancreas."
            );

            LogicMethods.AddReply(
                biologyClassThread,
                4,
                student1.Person,
                "Thanks for explaining! So different enzymes work in different parts of the digestive system. That makes sense now. Do we need to memorize which enzyme works where for the exam?"
            );

            LogicMethods.AddReply(
                biologyClassThread,
                5,
                student2.Person,
                "Yes, Mr. Een mentioned that we need to know the location and function of each enzyme for the test next week. I can share my study notes if you want."
            );

            // Update the thread timestamp
            biologyClassThread.UpdateLastUpdated();
            classSession1.DiscussionThreads.Add(biologyClassThread);


            //create the second biology class session
            var classSession2 = new ClassSession();
            classSession2.Id = 2;
            classSession2.Course = CourseDefaults.GetCourseNames(EducationLevel.SeniorCollege)[3];
            classSession2.Topic = "Digestive System";
            classSession2.Description = "Function of Enzymes in Digestive System";
            classSession2.LessonPlan =
                "Make the students understand the function of every Enzyme within the Digestive system";
            classSession2.Teacher = staff2;
            classSession2.AttendanceLog = new List<ClassAttendanceLogEntry>();
            classSession2.HomeworkDetails = new List<Homework>();
            classSession2.StudyMaterials = new List<FileAttachment>();
            classSession2.DiscussionThreads = new List<DiscussionThread>();
            classSession2.DiscussionThreads.Clear();
            // Create a homework question thread started by student3
            var homeworkThread = LogicMethods.StartDiscussion(
                2,
                6,
                student3.Person,
                "I'm stuck on question 5 of the geography homework about continental drift. Has anyone figured out how to explain the evidence for Pangaea?"
            );

            // Add replies
            LogicMethods.AddReply(
                homeworkThread,
                7,
                student2.Person,
                "The main evidence includes matching fossils found on different continents, the shapes of continents that fit together like a puzzle, and similar rock formations across oceans. Ms. Que said we should focus on these three points."
            );

            LogicMethods.AddReply(
                homeworkThread,
                8,
                student1.Person,
                "Don't forget to mention the climate evidence too! There are coal deposits in Antarctica showing it once had forests, and glacial evidence in Africa showing it was once near the South Pole."
            );

            LogicMethods.AddReply(
                homeworkThread,
                9,
                student3.Person,
                "Thanks for the help! I think I can complete the assignment now. Geography isn't my strongest subject, but this makes more sense."
            );

            // Update the thread timestamp
            homeworkThread.UpdateLastUpdated();
            classSession2.DiscussionThreads.Add(homeworkThread);

            //create a geography class session
            var classSession3 = new ClassSession();
            classSession3.Id = 3;
            classSession3.Course = CourseDefaults.GetCourseNames(EducationLevel.SeniorCollege)[12];
            classSession3.Topic = "The Solar System";
            classSession3.Description = "Planets of the solar System";
            classSession3.LessonPlan = "Make the students know the name of the nine planets";
            classSession3.Teacher = staff3;
            classSession3.AttendanceLog = new List<ClassAttendanceLogEntry>();
            classSession3.HomeworkDetails = new List<Homework>();
            classSession3.StudyMaterials = new List<FileAttachment>();
            classSession3.DiscussionThreads = new List<DiscussionThread>();
            classSession3.DiscussionThreads.Clear();
            // Create an exam preparation thread
            var examThread = LogicMethods.StartDiscussion(
                3,
                10,
                student1.Person,
                "Final exams are coming up next month. Should we create a study group for biology and geography? We could meet in the library after school."
            );

            // Add replies
            LogicMethods.AddReply(
                examThread,
                11,
                student2.Person,
                "That's a great idea! I'm definitely interested. We could divide the topics and each prepare study materials for different sections."
            );

            LogicMethods.AddReply(
                examThread,
                12,
                student3.Person,
                "Count me in. I'm good with the ecosystem chapters in biology and world geography topics. I can prepare those sections for our study group."
            );

            LogicMethods.AddReply(
                examThread,
                13,
                student4.Person, // Even the younger student wants to help!
                "Can I join too? I know I'm in kindergarten but I like science and my brother can bring me!"
            );

            LogicMethods.AddReply(
                examThread,
                14,
                student1.Person,
                "Sorry John, these are senior college exams, but maybe we can help you with your studies another time! Let's meet this Friday at 3 PM in the library, everyone."
            );

            // Update the thread timestamp and add it to the school
            examThread.UpdateLastUpdated();
            classSession3.DiscussionThreads.Add(examThread);

            //create another geography class session
            var classSession4 = new ClassSession();
            classSession4.Id = 4;
            classSession4.Course = CourseDefaults.GetCourseNames(EducationLevel.SeniorCollege)[12];
            classSession4.Topic = "Map of the earth";
            classSession4.Description = "Introduction to Continents";
            classSession4.LessonPlan = "Make the students know the name and location of the all the continents";
            classSession4.Teacher = staff3;
            classSession4.AttendanceLog = new List<ClassAttendanceLogEntry>();
            classSession4.HomeworkDetails = new List<Homework>();
            classSession4.StudyMaterials = new List<FileAttachment>();
            classSession4.DiscussionThreads = new List<DiscussionThread>();
            classSession4.DiscussionThreads.Clear();
            // Create a discussion about a school event
            var eventThread = LogicMethods.StartDiscussion(
                4,
                15,
                staff1.Person, // Started by the principal
                "Attention students: We're planning a Science Fair for the end of the term. Please start thinking about project ideas and which teachers you want as advisors."
            );

            // Add replies
            LogicMethods.AddReply(
                eventThread,
                16,
                student1.Person,
                "I'm thinking of doing a project on enzyme activity at different temperatures. Would Mr. Een be available to advise on this?"
            );

            LogicMethods.AddReply(
                eventThread,
                17,
                staff2.Person, // Biology teacher responds
                "Joe, that sounds like an excellent project idea. I'd be happy to be your advisor. Let's discuss the details after class tomorrow."
            );

            LogicMethods.AddReply(
                eventThread,
                18,
                student2.Person,
                "I'm interested in a project about weather patterns and climate change. Ms. Que, would you be willing to advise me?"
            );

            LogicMethods.AddReply(
                eventThread,
                19,
                staff3.Person, // Geography teacher responds
                "Of course, Dan. That's a very relevant topic. Stop by my office on Thursday and we can outline your project approach."
            );

            LogicMethods.AddReply(
                eventThread,
                20,
                student3.Person,
                "When is the submission deadline for project proposals? And will we be presenting to just our class or the whole school?"
            );

            LogicMethods.AddReply(
                eventThread,
                21,
                staff1.Person, // Principal answers
                "Project proposals are due by the end of next week. The best projects will be selected for presentation to the whole school during our Science Fair Day on the last Friday of term."
            );

            // Update the thread timestamp
            eventThread.UpdateLastUpdated();
            classSession4.DiscussionThreads.Add(eventThread);

            //create schedule for all existing class sessions
            ScheduleEntry scheduleEntry1 = new ScheduleEntry();
            scheduleEntry1.Id = 11;
            scheduleEntry1.DateTime = new DateTime(2025, 04, 15, 10, 00, 00);
            scheduleEntry1.Duration = TimeSpan.FromMinutes(30);
            scheduleEntry1.ClassSession = classSession1;
            scheduleEntry1.Title = "Biology Class";
            scheduleEntry1.Venue = "SSS3A Classroom";

            ScheduleEntry scheduleEntry2 = new ScheduleEntry();
            scheduleEntry2.Id = 22;
            scheduleEntry2.DateTime = new DateTime(2025, 04, 17, 10, 00, 00);
            scheduleEntry2.Duration = TimeSpan.FromMinutes(30);
            scheduleEntry2.ClassSession = classSession2;
            scheduleEntry2.Title = "Biology Class";
            scheduleEntry2.Venue = "SSS3A Classroom";

            ScheduleEntry scheduleEntry3 = new ScheduleEntry();
            scheduleEntry3.Id = 33;
            scheduleEntry3.DateTime = new DateTime(2025, 04, 15, 11, 00, 00);
            scheduleEntry3.Duration = TimeSpan.FromMinutes(30);
            scheduleEntry3.ClassSession = classSession3;
            scheduleEntry3.Title = "Geography Class";
            scheduleEntry3.Venue = "SSS3A Classroom";

            ScheduleEntry scheduleEntry4 = new ScheduleEntry();
            scheduleEntry4.Id = 44;
            scheduleEntry4.DateTime = new DateTime(2025, 04, 17, 11, 00, 00);
            scheduleEntry4.Duration = TimeSpan.FromMinutes(30);
            scheduleEntry4.ClassSession = classSession4;
            scheduleEntry4.Title = "Geography Class";
            scheduleEntry4.Venue = "SSS3A Classroom";

            //put all the schedule in a learning path
            LearningPath learningPath1 = new LearningPath();
            learningPath1.Id = 1001;
            learningPath1.EducationLevel = EducationLevel.SeniorCollege;
            learningPath1.ClassLevel = ClassLevel.SC_3;
            learningPath1.Semester = Semester.First;
            learningPath1.AcademicYearStart = new DateTime(2026, 01, 01);
            learningPath1.FeePerSemester = 50000;
            learningPath1.Schedule = new List<ScheduleEntry> { scheduleEntry1, scheduleEntry2, scheduleEntry3, scheduleEntry4 };
            var learning1Students = LogicMethods.GetStudentsByLevel(fcmSchool, EducationLevel.SeniorCollege, ClassLevel.SC_3);
            LogicMethods.AddMultipleStudentsToLearningPath(learningPath1, learning1Students);
            fcmSchool.LearningPath.Add(learningPath1);

            LearningPath learningPath2 = new LearningPath();
            learningPath2.Id = 1002;
            learningPath2.EducationLevel = EducationLevel.Kindergarten;
            learningPath2.ClassLevel = ClassLevel.KG_Nursery;
            learningPath2.Semester = Semester.First;
            learningPath2.AcademicYearStart = new DateTime(2026, 01, 01);
            learningPath2.Schedule = new List<ScheduleEntry> { };
            learningPath2.Students = new List<Student>() { };
            fcmSchool.LearningPath.Add(learningPath2);

            return fcmSchool;
        }

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
            fcmSchool.Guardians = new List<Guardian>();
            fcmSchool.Curricula = new List<Curriculum>();
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
            student1.Id = 301;
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
            student1.Guardian.Person = new Person();
            student1.Guardian.Person.LastName = "Mr. Jake";
            // Add the student to the school's student list
            LogicMethods.AddStudentToSchool(fcmSchool, student1);

            //create student 2
            Student student2 = new Student();
            student2.Id = 302;
            student2.GuardianId = 3021;
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
            student2.Guardian = new Guardian();
            var guardian2 = student2.Guardian;
            guardian2.Id = 3021;
            guardian2.Occupation = "Doctor";
            guardian2.RelationshipToStudent = Relationship.Mother;
            guardian2.Person = new Person();
            guardian2.Person.FirstName = "Diana";
            guardian2.Person.MiddleName = "D";
            guardian2.Person.LastName = "Mrs Deen";
            LogicMethods.AddStudentToSchool(fcmSchool, student2);


            //create student 3
            Student student3 = new Student();
            student3.Id = 303;
            student3.GuardianId = 3031;
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
            student3.Guardian = new Guardian();
            var guardian3 = student3.Guardian;
            guardian3.Id = 3031;
            guardian3.Occupation = "professor";
            guardian3.RelationshipToStudent = Relationship.Father;
            guardian3.Person = new Person();
            guardian3.Person.FirstName = "Zok";
            guardian3.Person.MiddleName = "Z";
            guardian3.Person.LastName = "Mr Zik";
            LogicMethods.AddStudentToSchool(fcmSchool, student3);

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
                Console.WriteLine(
                    $"Id: {student.Id}, Name: {student.Person.FirstName} {student.Person.MiddleName} {student.Person.LastName}");
            }

            //retrieving teachers who are specialized for handling senior college courses
            var sCStaff = LogicMethods.GetStaffByEducationLevel(fcmSchool, EducationLevel.SeniorCollege);
            Console.WriteLine("Senior College Teachers:");
            foreach (var staff in sCStaff)
            {
                Console.WriteLine(
                    $"Id: {staff.Id}, Name: {staff.Person.FirstName} {staff.Person.MiddleName} {staff.Person.LastName} Area of specialization: {staff.AreaOfSpecialization}");
            }

            //retrieving All the teachers in the school
            var allTeachers = LogicMethods.GetAllTeachers(fcmSchool);
            foreach (var teacher in allTeachers)
            {
                Console.WriteLine(
                    $"Id: {teacher.Id}, Name: {teacher.Person.FirstName} {teacher.Person.LastName} ........ {teacher.AreaOfSpecialization}");
            }

            //create a biology class session and assign a teacher to it
            var classSession1 = new ClassSession();
            classSession1.Id = 1;
            classSession1.Course = CourseDefaults.GetCourseNames(EducationLevel.SeniorCollege)[3];
            classSession1.Topic = "Digestive System";
            classSession1.Description = "Function of Organs in Digestive System";
            classSession1.LessonPlan =
                "Make the students understand the function of every organ within the Digestive system";
            classSession1.Teacher = staff2;

            //create the second biology class session
            var classSession2 = new ClassSession();
            classSession2.Id = 2;
            classSession2.Course = CourseDefaults.GetCourseNames(EducationLevel.SeniorCollege)[3];
            classSession2.Topic = "Digestive System";
            classSession2.Description = "Function of Enzymes in Digestive System";
            classSession2.LessonPlan =
                "Make the students understand the function of every Enzyme within the Digestive system";
            classSession2.Teacher = staff2;

            //create a geography class session
            var classSession3 = new ClassSession();
            classSession3.Id = 3;
            classSession3.Course = CourseDefaults.GetCourseNames(EducationLevel.SeniorCollege)[12];
            classSession3.Topic = "The Solar System";
            classSession3.Description = "Planets of the solar System";
            classSession3.LessonPlan = "Make the students know the name of the nine planets";
            classSession3.Teacher = staff3;

            //create another geography class session
            var classSession4 = new ClassSession();
            classSession4.Id = 4;
            classSession4.Course = CourseDefaults.GetCourseNames(EducationLevel.SeniorCollege)[12];
            classSession4.Topic = "Map of the earth";
            classSession4.Description = "Introduction to Continents";
            classSession4.LessonPlan = "Make the students know the name and location of the all the continents";
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
            learningPath1.Semester = Semester.First;
            learningPath1.Schedule = new List<ScheduleEntry>
                { scheduleEntry1, scheduleEntry2, scheduleEntry3, scheduleEntry4 };
            learningPath1.Students = new List<Student>() { student1, student2, student3 };


            //learning path1 is added again for test and detected to have been added before
            //LogicMethods.AddAScheduleToLearningPath(learningPath1,scheduleEntry1);

            Console.WriteLine(
                $"Learning Path {learningPath1.Id} now contains {learningPath1.Schedule.Count} schedule(s).");

            //testing time overlap
            var schedule1 = new ScheduleEntry
            {
                Id = 101,
                DateTime = DateTime.Now,
                Duration = TimeSpan.FromHours(1),
                ClassSession = new ClassSession()
            };
            var schedule2 = new ScheduleEntry
            {
                Id = 102,
                DateTime = DateTime.Now.AddHours(2),
                Duration = TimeSpan.FromHours(1),
                ClassSession = new ClassSession()
            };
            var schedule3 = new ScheduleEntry
            {
                Id = 103,
                DateTime = DateTime.Now,
                Duration = TimeSpan.FromHours(1),
                ClassSession = new ClassSession()
            };

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
                Console.WriteLine($"Id: {schedule.Id}, Date: {schedule.DateTime}, Duration: {schedule.Duration}");
            }

            var learningPath2 = new LearningPath
            {
                Id = 4401,
                EducationLevel = EducationLevel.SeniorCollege,
                ClassLevel = ClassLevel.SC_3,
                Semester = Semester.Second,
                Schedule = new List<ScheduleEntry>()
            };

            // Add multiple schedules
            try
            {
                var schedulesToAdd = new List<ScheduleEntry> { schedule1, schedule2, schedule3 };
                LogicMethods.AddMultipleSchedulesToLearningPath(learningPath2,
                    schedulesToAdd); // Error due to overlap with schedule1
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            // Display updated schedules
            Console.WriteLine("Updated Schedules:");
            foreach (var schedule in learningPath2.Schedule)
            {
                Console.WriteLine($"Id: {schedule.Id}, Date: {schedule.DateTime}, Duration: {schedule.Duration}");
            }

            //testing add learning path to school: adding learning path2 which has not been added - success
            try
            {
                LogicMethods.AddLearningPathToSchool(fcmSchool, learningPath2);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            //add learning path1 which has been added - fail
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
            eventEntry.DateTime = new DateTime(2026, 12, 20, 12, 00, 00);
            eventEntry.Duration = TimeSpan.FromHours(2);
            eventEntry.Venue = "School Sports Field";
            eventEntry.Event = "Annual Sport day";
            eventEntry.Notes = "All students and staff to wear school sports attire";
            allCalendar.ScheduleEntries.Add(eventEntry);

            //Create a meeting for a Teachers with the Principal
            ScheduleEntry meetingEntry = new ScheduleEntry();
            meetingEntry.Id = 3098;
            meetingEntry.Title = "weekly review";
            meetingEntry.DateTime = DateTime.Today;
            meetingEntry.Duration = TimeSpan.FromMinutes(20);
            meetingEntry.Venue = "Staff Room";
            meetingEntry.Meeting = "Academic Staff Meeting";
            meetingEntry.Event = null;
            meetingEntry.ClassSession = null;
            meetingEntry.Notes = "Agenda: Weekly Academic Progress Evaluation";

            //make staff meeting a recurring feature
            meetingEntry.IsRecurring = true;
            meetingEntry.RecurrencePattern = RecurrenceType.Weekly;
            meetingEntry.RecurrenceInterval = 1;
            meetingEntry.EndDate = DateTime.Now.AddMonths(6);

            List<ScheduleEntry> recurringSchedules = LogicMethods.GenerateRecurringSchedules(meetingEntry);
            foreach (var schedule in recurringSchedules)
            {
                Console.WriteLine($"{schedule.DateTime}: {schedule.Title} at {schedule.Venue}");
            }

            allCalendar.ScheduleEntries.Add(meetingEntry);
            LogicMethods.DisplayAllCalendarEntries(fcmSchool);

            // Create a base schedule entry
            ScheduleEntry scheduleEntry = new ScheduleEntry
            {
                Id = 1,
                DateTime = DateTime.Now,
                Duration = TimeSpan.FromHours(1),
                Venue = "Room 101",
                Title = "Mathematics Class",
                Event = null,
                Meeting = null,
                Notes = "Recurrence Example",
                IsRecurring = true, // Enable recurrence
                RecurrencePattern = RecurrenceType.Weekly, // Repeat Weekly
                RecurrenceInterval = 1, // Every 1 week
                EndDate = DateTime.Now.AddMonths(2) // Recurrence ends in 2 months
            };

            // Generate recurring schedules
            List<ScheduleEntry> recurringSchedules2 = LogicMethods.GenerateRecurringSchedules(scheduleEntry);

            // Display the schedules
            foreach (var schedule in recurringSchedules2)
            {
                Console.WriteLine($"{schedule.DateTime}: {schedule.Title} at {schedule.Venue}");
            }

            //Get all schedules in a learning path
            var schedulesInLearning1 = LogicMethods.GetAllSchedulesInLearningPath(learningPath1);
            foreach (var schedule in schedulesInLearning1)
            {
                Console.WriteLine(
                    $"Id: {schedule.Id}, Title: {schedule.Title}, Date: {schedule.DateTime}, Duration: {schedule.Duration}");
            }

            //Get all the schedules in a learning path for a particular day (today)
            var schedulesForToday = LogicMethods.GetSchedulesByDateInLearningPath(learningPath1, DateTime.Today);
            foreach (var schedule in schedulesForToday)
            {
                Console.WriteLine(
                    $"Id: {schedule.Id}, Title: {schedule.Title}, Date: {schedule.DateTime}, Duration: {schedule.Duration}");
            }

            //Get all class sessions in a learning path
            var classSessionInLp1 = LogicMethods.GetClassSessionsInLearningPath(learningPath1);
            foreach (var session in classSessionInLp1)
            {
                Console.WriteLine($"Id: {session.Id}, Topic: {session.Topic}, Description: {session.Description}");
            }

            //Get meetings for a particular day from any calendar
            var myMeetings = LogicMethods.GetAllMeetingsByDate(allCalendar, DateTime.Today);
            Console.WriteLine($"Meetings on {DateTime.Today.ToShortDateString()} in {allCalendar.Name}:");
            foreach (var meeting in myMeetings)
            {
                Console.WriteLine($"  Id: {meeting.Id}, Title: {meeting.Meeting}, " +
                                  $"Start: {meeting.DateTime.ToShortTimeString()}, " +
                                  $"Duration: {meeting.Duration}");
            }

            // Get events for a particular day from any calendar
            var myEvents = LogicMethods.GetAllEventsByDate(allCalendar, DateTime.Today);
            Console.WriteLine($"Events on {DateTime.Today.ToShortDateString()} in {allCalendar.Name}:");
            foreach (var evt in myEvents)
            {
                Console.WriteLine($"  Id: {evt.Id}, Title: {evt.Event}, " +
                                  $"Start: {evt.DateTime.ToShortTimeString()}, " +
                                  $"Duration: {evt.Duration}");
            }

            foreach (var guardian in fcmSchool.Guardians)
            {
                Console.WriteLine(
                    $"Id: {guardian.Id}, Name: {guardian.Person.LastName}, Occupation: {guardian.Occupation}, Relationship to student: {guardian.RelationshipToStudent}");
            }

            foreach (var student in fcmSchool.Students)
            {
                Console.WriteLine(
                    $"Id: {student.Id}, Name: {student.Person.FirstName}, guardian: {student.GuardianId}, Nane of Guardian: {student.Guardian.Person.LastName}");
            }

            //Testing post meeting to all staff
            LogicMethods.PostMeetingToStaff(fcmSchool, meetingEntry);

            foreach (var staff in fcmSchool.Staff)
            {
                Console.WriteLine($"{staff.Person.FirstName} {staff.Person.LastName}'s Calendar:");
                foreach (var entry in staff.Person.PersonalCalendar.ScheduleEntries)
                {
                    Console.WriteLine($"  Meeting: {entry.Meeting}, Date: {entry.DateTime}, Venue: {entry.Venue}");
                }
            }

            //testing post meeting to all guardians
            LogicMethods.PostMeetingToGuardian(fcmSchool, meetingEntry);
            foreach (var guardian in fcmSchool.Guardians)
            {
                Console.WriteLine($"{guardian.Person.FirstName} {guardian.Person.LastName}'s Calendar:");
                foreach (var entry in guardian.Person.PersonalCalendar.ScheduleEntries)
                {
                    Console.WriteLine($"  Meeting: {entry.Meeting}, Date: {entry.DateTime}, Venue: {entry.Venue}");
                }
            }

            // Generate the curriculum for the Senior College Learning Path
            LogicMethods.GenerateCurriculumForLearningPath(fcmSchool, learningPath1);

            foreach (var curriculum in fcmSchool.Curricula)
            {
                Console.WriteLine($"Curriculum Id: {curriculum.Id}");
                Console.WriteLine($"Year: {curriculum.Year}");
                Console.WriteLine($"Education Level: {curriculum.EducationLevel}");
                Console.WriteLine($"Class Level: {curriculum.ClassLevel}");
                foreach (var semester in curriculum.Semesters)
                {
                    Console.WriteLine($"  Semester {semester.Semester}:");
                    foreach (var classSession in semester.ClassSessions)
                    {
                        Console.WriteLine($"    Course: {classSession.Course}");
                        Console.WriteLine($"    Topic: {classSession.Topic}");
                        Console.WriteLine($"    Description: {classSession.Description}");
                        Console.WriteLine($"    Lesson Note: {classSession.LessonPlan}");
                    }
                }

                //test retrieve curriculum
                try
                {
                    var curriculumForClass = LogicMethods.GetCurriculumForClass(fcmSchool, EducationLevel.SeniorCollege, ClassLevel.SC_3, 2026);

                    Console.WriteLine($"Curriculum Id: {curriculum.Id}");
                    Console.WriteLine($"Year: {curriculum.Year}");
                    Console.WriteLine($"Education Level: {curriculum.EducationLevel}");
                    Console.WriteLine($"Class Level: {curriculum.ClassLevel}");
                    foreach (var semester in curriculum.Semesters)
                    {
                        Console.WriteLine($"  Semester {semester.Semester}:");
                        foreach (var classSession in semester.ClassSessions)
                        {
                            Console.WriteLine($"    Course: {classSession.Course}");
                            Console.WriteLine($"    Topic: {classSession.Topic}");
                            Console.WriteLine($"    Description: {classSession.Description}");
                            Console.WriteLine($"    Lesson Note: {classSession.LessonPlan}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                //add fee for learning path 1
                learningPath1.FeePerSemester = 1000.0;

                //assign semester fee to each student in learning path1
                LogicMethods.SetStudentFeesForLearningPath(learningPath1);

                foreach (var student in learningPath1.Students)
                {
                    Console.WriteLine($"Student: {student.Person.FirstName} - {student.Person.SchoolFees.TotalAmount}");
                }
                //student1 make payment of 200 out of 1000
                LogicMethods.MakePaymentForStudent(student1, 200.0, PaymentMethod.Cash);
                LogicMethods.MakePaymentForStudent(student1, 100.0, PaymentMethod.Card);
                Console.WriteLine($"Student 1's school fees cost is: {student1.Person.SchoolFees.TotalAmount}");
                Console.WriteLine($"student 1's total fees are: {student1.Person.SchoolFees.Balance}");

                //test generate payment summery
                var summary = LogicMethods.GeneratePaymentSummaryForStudent(student1);
                foreach (var line in summary)
                {
                    Console.WriteLine(line);
                }

                //students with outstanding payments
                var outstandingPayments = LogicMethods.GetStudentsWithOutstandingPayments(learningPath1);

                foreach (var (student, balance) in outstandingPayments)
                {
                    Console.WriteLine($"Student: {student.Person.FirstName} {student.Person.LastName}, Outstanding Balance: {balance:C}");
                }
                LogicMethods.MakePaymentForStudent(student1, 700.0, PaymentMethod.Card);
                LogicMethods.GrantAccessToSchedules(student1, learningPath1);
                //retrieve students with up to 50% payment
                var studentsWithAccess = LogicMethods.GetStudentsWithAccess(learningPath1);

                foreach (var student in studentsWithAccess)
                {
                    Console.WriteLine($"Student Id: {student.Id}, Name: {student.Person.FirstName} {student.Person.LastName}");
                }

                //generate payment report for learning path
                var report = LogicMethods.GetPaymentReportForLearningPath(learningPath1);

                foreach (var entry in report)
                {
                    Console.WriteLine($"Student: {entry.StudentName}");
                    Console.WriteLine($"Total Fees: {entry.TotalFees:C}");
                    Console.WriteLine($"Total Paid: {entry.TotalPaid:C}");
                    Console.WriteLine($"Outstanding Balance: {entry.OutstandingBalance:C}");

                    Console.WriteLine("Payment Details:");
                    foreach (var payment in entry.PaymentDetails)
                    {
                        Console.WriteLine($"  Date: {payment.Date:d}, Amount: {payment.Amount:C}, Method: {payment.PaymentMethod}");
                    }
                    Console.WriteLine();
                }

                //LogicMethods.NotifyStudentsOfPaymentStatus(learningPath1);

                var currentLearningPath = learningPath1;

                Console.WriteLine($"Current Learning Path: {currentLearningPath.ClassLevel} - Semester {currentLearningPath.Semester}");

                foreach (var schedule in learningPath1.Schedule)
                {
                    Console.WriteLine($"{scheduleEntry.Id}: {scheduleEntry.DateTime} - {scheduleEntry.Title}, {scheduleEntry.Duration}");
                }

                LogicMethods.DisplayStudentSchedules(learningPath1);
                // Get the next learning path
                var nextLearningPath = LogicMethods.GetNextLearningPath(currentLearningPath, fcmSchool);

                if (nextLearningPath != null)
                {
                    Console.WriteLine($"Next Learning Path: {nextLearningPath.ClassLevel} - Semester {nextLearningPath.Semester}");
                }
                else
                {
                    Console.WriteLine("No next learning path found. Manual promotion may be required.");
                }

                foreach (var student in learningPath1.Students)
                {
                    Console.WriteLine($"{student.Id}");
                }

                foreach (var entry in student1.Person.PersonalCalendar.ScheduleEntries)
                {
                    Console.WriteLine($"{scheduleEntry.DateTime}, {scheduleEntry.Title}, {scheduleEntry.Venue}");
                }
                LogicMethods.SynchronizeSchedulesWithStudents(learningPath1);

                foreach (var entry in student1.Person.PersonalCalendar.ScheduleEntries)
                {
                    Console.WriteLine($"{scheduleEntry.DateTime}, {scheduleEntry.Title}, {scheduleEntry.Venue}");
                }

                var student1Calendar = student1.Person.PersonalCalendar;

                LogicMethods.ClearCalendar(student1Calendar);

                foreach (var entry in student1.Person.PersonalCalendar.ScheduleEntries)
                {
                    Console.WriteLine($"{scheduleEntry.DateTime}, {scheduleEntry.Title}, {scheduleEntry.Venue}");
                }

                //LogicMethods.TransferStudentsToNextLearningPath(learningPath1, learningPath2);

                foreach (var student in learningPath1.Students)
                {
                    Console.WriteLine($"{student.Id}");
                }

                foreach (var student in learningPath2.Students)
                {
                    Console.WriteLine($"{student.Id}");
                }
                //put class session 2 and 3 into schedule entry 2 and 3
                scheduleEntry2.ClassSession = classSession2;
                scheduleEntry3.ClassSession = classSession3;

                //add all schedules to the first learning path
                Console.WriteLine(learningPath1.Schedule.Count);

                learningPath1.Schedule.Add(scheduleEntry1);
                learningPath1.Schedule.Add(scheduleEntry2);

                Console.WriteLine(learningPath1.Schedule.Count);

                var todaysSchedule = LogicMethods.GetEntriesByDate(allCalendar, DateTime.Today);

                foreach (var entry in todaysSchedule)
                {
                    Console.WriteLine($" {entry.Title} , {entry.DateTime}, {entry.IsRecurring}, {entry.Id}");
                }

                var futureEntries = LogicMethods.GetUpcomingEntries(allCalendar);

                foreach (var entry in futureEntries)
                {
                    Console.WriteLine($" {entry.Title} , {entry.DateTime}, {entry.IsRecurring}, {entry.Id}");
                }

                var mondayEntries = LogicMethods.GetEntriesForDayOfWeek(allCalendar, DayOfWeek.Thursday);
                foreach (var entry in mondayEntries)
                {
                    Console.WriteLine($" {entry.Title} , {entry.DateTime}, {entry.IsRecurring}, {entry.Id}");
                }

                LogicMethods.AddScheduleEntry(allCalendar, scheduleEntry1);
                LogicMethods.AddScheduleEntry(allCalendar, scheduleEntry2);
                LogicMethods.AddScheduleEntry(allCalendar, scheduleEntry3);
                LogicMethods.AddScheduleEntry(allCalendar, scheduleEntry4);
                fcmSchool.SchoolCalendar.Add(allCalendar);

                foreach (var entry in fcmSchool.SchoolCalendar)
                {
                    Console.WriteLine($" {entry.Name} ,  {entry.Id}");
                }

                //LogicMethods.ExportCalendar(allCalendar, @"../../../MYSTUDIES.xml");

                var termWork = LogicMethods.GenerateSchoolCalendar(fcmSchool);

                foreach (var entry in termWork)
                {
                    Console.WriteLine($" {entry.Title} , {entry.DateTime}, {entry.IsRecurring}, {entry.Id}");
                }

                //Attendance test
                fcmSchool.LearningPath.Add(learningPath1);

                var expectedStudents = LogicMethods.GetExpectedStudentsForClassSession(fcmSchool, classSession2);
                foreach (var student in expectedStudents)
                {
                    Console.WriteLine($"{student.Id}, {student.Person.FirstName} {student.Person.LastName}");
                }

                List<Student> presenStudents = new List<Student>() { student1, student2 };
                LogicMethods.TakeAttendanceForClassSession(fcmSchool, classSession2, presenStudents, staff2);

                var absent = LogicMethods.GetStudentsAbsentForClassSession(classSession2);
                foreach (var student in absent)
                {
                    Console.WriteLine($"Absentees from math class: {student.Id}: {student.Person.FirstName} {student.Person.LastName}");
                }

                List<Student> presentStudents = LogicMethods.GetStudentsPresentForClassSession(classSession2);

                foreach (var student in presenStudents)
                {
                    Console.WriteLine($" Student present for math class: {student.Id}: {student.Person.FirstName} {student.Person.LastName}");
                }

                var attLog = LogicMethods.GetAttendanceForLearningPath(learningPath1);

                Console.WriteLine($"Attendance Records for {learningPath1.Semester} Semester:");
                foreach (var log in attLog)
                {
                    Console.WriteLine($"Class Session Id: {log.ClassSession.Id}, Course: {log.ClassSession.Course}, Topic: {log.ClassSession.Topic}");
                    Console.WriteLine($"Teacher: {log.Teacher.Person.FirstName}");
                    Console.WriteLine("Present Students:");
                    foreach (var student in log.Attendees)
                    {
                        Console.WriteLine($" - {student.Person.FirstName}");
                    }
                    Console.WriteLine("Absent Students:");
                    foreach (var student in log.AbsentStudents)
                    {
                        Console.WriteLine($" - {student.Person.FirstName}");
                    }
                    Console.WriteLine("--------------------------------------------------");
                }


            }
        }

    }
}



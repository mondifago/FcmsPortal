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
            fcmSchool.SchoolCalendar = new List<CalendarModel>();
            fcmSchool.Guardians = new List<Guardian>();
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
            CalendarModel allCalendar = new CalendarModel();
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
            student1.Person.PersonalCalendar = new CalendarModel();
            student1.Person.PersonalCalendar.Id = 177;
            student1.Person.PersonalCalendar.Name = "Student1's Study Calendar";
            student1.Person.PersonalCalendar.ScheduleEntries = new List<ScheduleEntry>();
            student1.CourseGrades = new List<CourseGrade>();
            student1.CourseGrades.Add(new CourseGrade
            {
                Id = 1,
                Course = CourseDefaults.GetCourseNames(EducationLevel.SeniorCollege)[3],
                TestGrades = new List<TestGrade>(),
                TotalGrade = 0,
                AttendancePercentage = 0,
                FinalGradeCode = "",
                StudentId = student1.Id,
                LearningPathId = 1001
            });
            LogicMethods.AddStudentToGuardianWards(guardian1, student1);
            LogicMethods.AddStudentToSchool(fcmSchool, student1);


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
            student2.Person.PersonalCalendar = new CalendarModel();
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
            student3.Person.PersonalCalendar = new CalendarModel();
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
            homeworkSubmission1.SubmissionDate = DateTime.Today.AddDays(1);
            homeworkSubmission1.Answer = "The stomach is a muscular organ that mixes food with gastric juices, breaking it down into a semi-liquid form called chyme. It also plays a role in the digestion of proteins and the absorption of certain nutrients.";
            homeworkSubmission1.IsGraded = false;

            homework1.Submissions.Add(homeworkSubmission1);

            // Assign the single homework to the class session
            classSession1.HomeworkDetails = homework1;

            var joeSubmission = homework1.Submissions.FirstOrDefault(s => s.Student.Id == student1.Id);
            if (joeSubmission != null)
            {
                // Create the homework grade
                var homeworkGrade = new TestGrade
                {
                    Course = classSession1.Course, // Biology
                    Score = 70,
                    GradeType = GradeType.Homework,
                    Teacher = staff2, // Mr. Een (Biology teacher)
                    Date = DateTime.Now.AddDays(-1), // Graded yesterday
                    TeacherRemark = "Good understanding of enzyme functions. Your explanation shows you grasp the basic concepts well. Try to include more specific examples of where each enzyme is produced in the body for extra points."
                };

                // Update the submission with grade
                joeSubmission.HomeworkGrade = homeworkGrade;
                joeSubmission.IsGraded = true;
                joeSubmission.FeedbackComment = "Good understanding of enzyme functions. Your explanation shows you grasp the basic concepts well. Try to include more specific examples of where each enzyme is produced in the body for extra points.";

                // Add the grade to Joe's course grades using the corrected method
                //LogicMethods.SubmitHomeworkGradeToStudent(student1, joeSubmission);
            }


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
            classSession2.HomeworkDetails = new Homework();
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
            classSession3.HomeworkDetails = new Homework();
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
                student4.Person,
                "Can I join too? I know I'm in kindergarten but I like science and my brother can bring me!"
            );

            LogicMethods.AddReply(
                examThread,
                14,
                student1.Person,
                "Sorry John, these are senior college exams, but maybe we can help you with your studies another time! Let's meet this Friday at 3 PM in the library, everyone."
            );

            // Update the thread timestamp and add it to the class session
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
            classSession4.HomeworkDetails = new Homework();
            classSession4.StudyMaterials = new List<FileAttachment>();
            classSession4.DiscussionThreads = new List<DiscussionThread>();
            classSession4.DiscussionThreads.Clear();

            // Create a discussion about a school event
            var eventThread = LogicMethods.StartDiscussion(
                4,
                15,
                staff1.Person,
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
                staff2.Person,
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
                staff3.Person,
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
                staff1.Person,
                "Project proposals are due by the end of next week. The best projects will be selected for presentation to the whole school during our Science Fair Day on the last Friday of term."
            );

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
            allCalendar.ScheduleEntries.Add(scheduleEntry1);

            ScheduleEntry scheduleEntry2 = new ScheduleEntry();
            scheduleEntry2.Id = 22;
            scheduleEntry2.DateTime = new DateTime(2025, 04, 17, 10, 00, 00);
            scheduleEntry2.Duration = TimeSpan.FromMinutes(30);
            scheduleEntry2.ClassSession = classSession2;
            scheduleEntry2.Title = "Biology Class";
            scheduleEntry2.Venue = "SSS3A Classroom";
            allCalendar.ScheduleEntries.Add(scheduleEntry2);

            ScheduleEntry scheduleEntry3 = new ScheduleEntry();
            scheduleEntry3.Id = 33;
            scheduleEntry3.DateTime = new DateTime(2025, 04, 15, 11, 00, 00);
            scheduleEntry3.Duration = TimeSpan.FromMinutes(30);
            scheduleEntry3.ClassSession = classSession3;
            scheduleEntry3.Title = "Geography Class";
            scheduleEntry3.Venue = "SSS3A Classroom";
            allCalendar.ScheduleEntries.Add(scheduleEntry3);

            ScheduleEntry scheduleEntry4 = new ScheduleEntry();
            scheduleEntry4.Id = 44;
            scheduleEntry4.DateTime = new DateTime(2025, 04, 17, 11, 00, 00);
            scheduleEntry4.Duration = TimeSpan.FromMinutes(30);
            scheduleEntry4.ClassSession = classSession4;
            scheduleEntry4.Title = "Geography Class";
            scheduleEntry4.Venue = "SSS3A Classroom";
            allCalendar.ScheduleEntries.Add(scheduleEntry4);

            //put all the schedule in a learning path
            LearningPath learningPath1 = new LearningPath();
            learningPath1.Id = 1001;
            learningPath1.EducationLevel = EducationLevel.SeniorCollege;
            learningPath1.ClassLevel = ClassLevel.SC_3;
            learningPath1.Semester = Semester.First;
            learningPath1.AcademicYearStart = new DateTime(2025, 01, 01);
            learningPath1.FeePerSemester = 50000;
            learningPath1.SemesterStartDate = new DateTime(2025, 01, 01);
            learningPath1.SemesterEndDate = new DateTime(2025, 06, 30);
            learningPath1.ExamsStartDate = new DateTime(2025, 07, 01);
            learningPath1.Schedule = new List<ScheduleEntry> { scheduleEntry1, scheduleEntry2, scheduleEntry3, scheduleEntry4 };
            var learning1Students = LogicMethods.GetStudentsByLevel(fcmSchool, EducationLevel.SeniorCollege, ClassLevel.SC_3);
            fcmSchool.LearningPath.Add(learningPath1);

            LearningPath learningPath2 = new LearningPath();
            learningPath2.Id = 1002;
            learningPath2.EducationLevel = EducationLevel.Kindergarten;
            learningPath2.ClassLevel = ClassLevel.KG_Nursery;
            learningPath2.Semester = Semester.First;
            learningPath2.AcademicYearStart = new DateTime(2025, 01, 01);
            learningPath2.FeePerSemester = 20000;
            learningPath2.SemesterStartDate = new DateTime(2025, 01, 01);
            learningPath2.SemesterEndDate = new DateTime(2025, 06, 30);
            learningPath2.ExamsStartDate = new DateTime(2025, 07, 01);
            learningPath2.AcademicYearStart = new DateTime(2025, 01, 01);
            learningPath2.Schedule = new List<ScheduleEntry> { };
            var learning2Students = LogicMethods.GetStudentsByLevel(fcmSchool, EducationLevel.Kindergarten, ClassLevel.KG_Nursery);
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
            fcmSchool.SchoolCalendar = new List<CalendarModel>();
            fcmSchool.Guardians = new List<Guardian>();
            fcmSchool.Address = address;
            address.Street = "120 City Road";
            address.City = "Asaba";
            address.State = "Delta State";
            address.PostalCode = "P.O.Box 150";
            address.Country = "Nigeria";
            CalendarModel allCalendar = new CalendarModel();
            allCalendar.Id = 2024;
            allCalendar.Name = "2024 Calendar";
            allCalendar.ScheduleEntries = new List<ScheduleEntry>();
            fcmSchool.SchoolCalendar.Add(allCalendar);
        }
    }
}





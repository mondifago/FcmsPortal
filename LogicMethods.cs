using FcmsPortal.Enums;
using FcmsPortal.ViewModel;

namespace FcmsPortal
{
    internal static class LogicMethods
    {
        /// <summary>
        /// Methods involved in Initial Setup
        /// </summary>
        // Filter students based on specified education level and class level
        public static List<Student> GetStudentsByLevel(School school, EducationLevel educationLevel, ClassLevel classLevel)
        {
            if (school == null)
                throw new ArgumentNullException(nameof(school), "School cannot be null.");

            return school.Students
                .Where(student => student.Person.EducationLevel == educationLevel
                                  && student.Person.ClassLevel == classLevel)
                .ToList();
        }

        //filter staff based on specified education level
        public static List<Staff> GetStaffByEducationLevel(School school, EducationLevel educationLevel)
        {
            if (school == null)
                throw new ArgumentNullException(nameof(school), "School cannot be null.");
            return school.Staff
                .Where(staff => staff.Person.EducationLevel == educationLevel).ToList();
        }
        
        //Get all teachers from school list of staff
        public static List<Staff> GetAllTeachers(School school)
        {
            if (school == null)
            {
                throw new ArgumentNullException(nameof(school), "School cannot be null");
            }

            if (school.Staff == null)
            {
                throw new ArgumentException("School staff list is not initialized", nameof(school));
            }

            try
            {
                return school.Staff
                    .Where(staff => staff != null && staff.JobRole == JobRole.Teacher)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving teachers: {ex.Message}");
                throw;
            }
        }
        
        //Adding a schedule entry to learning path
        public static void AddAScheduleToLearningPath(LearningPath learningPath, ScheduleEntry scheduleEntry)
        {
            if (learningPath == null)
            {
                throw new ArgumentNullException(nameof(learningPath), "Learning path cannot be null.");
            }

            if (scheduleEntry == null)
            {
                throw new ArgumentNullException(nameof(scheduleEntry), "Schedule entry cannot be null.");
            }

            if (scheduleEntry.ClassSession == null)
            {
                throw new ArgumentNullException(nameof(scheduleEntry.ClassSession), "Class session cannot be null.");
            }
            if (learningPath.Schedule.Any(s => s.Id == scheduleEntry.Id))
            {
                throw new ArgumentException($"A schedule with ID {scheduleEntry.Id} already exists in the learning path.");
            }
            bool hasOverlap = learningPath.Schedule.Any(existing =>
                existing.DateTime < scheduleEntry.DateTime.Add(scheduleEntry.Duration) &&
                scheduleEntry.DateTime < existing.DateTime.Add(existing.Duration));

            if (hasOverlap)
            {
                throw new InvalidOperationException("This schedule overlaps with an existing class session.");
            }
            bool sameTimePeriod = learningPath.Schedule.Any(existing =>
                existing.DateTime == scheduleEntry.DateTime &&
                existing.Duration == scheduleEntry.Duration);

            if (sameTimePeriod)
            {
                throw new InvalidOperationException("A schedule with the same time period already exists.");
            }
            learningPath.Schedule.Add(scheduleEntry);
            Console.WriteLine($"Schedule with ID {scheduleEntry.Id} has been successfully added to Learning Path ID {learningPath.Id}.");
        }
        
        //Adding multiple schedules to a learning path
        public static void AddMultipleSchedulesToLearningPath(LearningPath learningPath, List<ScheduleEntry> scheduleEntries)
        {
    
            if (learningPath == null)
            {
                throw new ArgumentNullException(nameof(learningPath), "Learning path cannot be null.");
            }

            if (scheduleEntries == null || !scheduleEntries.Any())
            {
                throw new ArgumentNullException(nameof(scheduleEntries), "Schedule entries cannot be null or empty.");
            }

            foreach (var scheduleEntry in scheduleEntries)
            {
                if (scheduleEntry == null)
                {
                    throw new ArgumentNullException(nameof(scheduleEntry), "One or more schedule entries are null.");
                }

                if (scheduleEntry.ClassSession == null)
                {
                    throw new ArgumentNullException(nameof(scheduleEntry.ClassSession), "Class session cannot be null.");
                }
                
                if (learningPath.Schedule.Any(s => s.Id == scheduleEntry.Id))
                {
                    throw new ArgumentException($"A schedule with ID {scheduleEntry.Id} already exists in the learning path.");
                }
                
                bool hasOverlap = learningPath.Schedule.Any(existing =>
                    existing.DateTime < scheduleEntry.DateTime.Add(scheduleEntry.Duration) &&
                    scheduleEntry.DateTime < existing.DateTime.Add(existing.Duration));

                if (hasOverlap)
                {
                    throw new InvalidOperationException($"Schedule ID {scheduleEntry.Id} overlaps with an existing class session.");
                }
                
                bool sameTimePeriod = learningPath.Schedule.Any(existing =>
                    existing.DateTime == scheduleEntry.DateTime &&
                    existing.Duration == scheduleEntry.Duration);

                if (sameTimePeriod)
                {
                    throw new InvalidOperationException($"A schedule with the same time period as ID {scheduleEntry.Id} already exists.");
                }
            }
            
            learningPath.Schedule.AddRange(scheduleEntries);
            Console.WriteLine($"{scheduleEntries.Count} schedules have been successfully added to Learning Path ID {learningPath.Id}.");
        }
        
        //Add learning path to school
        public static void AddLearningPathToSchool(School school, LearningPath learningPath)
        {
            if (school == null)
            {
                throw new ArgumentNullException(nameof(school), "School cannot be null.");
            }

            if (learningPath == null)
            {
                throw new ArgumentNullException(nameof(learningPath), "Learning path cannot be null.");
            }
            
            bool isDuplicatePath = school.LearningPath.Any(existingPath =>
                existingPath.EducationLevel == learningPath.EducationLevel &&
                existingPath.ClassLevel == learningPath.ClassLevel &&
                existingPath.Semester == learningPath.Semester);

            if (isDuplicatePath)
            {
                throw new InvalidOperationException("A learning path with the same Education Level, Class Level, and Semester already exists in the school.");
            }
            
            foreach (var student in learningPath.Students)
            {
                bool isStudentInAnotherPath = school.LearningPath.Any(existingPath =>
                    existingPath.Students.Any(s => s.ID == student.ID));

                if (isStudentInAnotherPath)
                {
                    throw new InvalidOperationException($"Student ID {student.ID} already belongs to another learning path.");
                }
            }
            
            school.LearningPath.Add(learningPath);

            Console.WriteLine($"Learning path with ID {learningPath.Id} has been added to the school.");
        }



        
        /// <summary>
        /// Methods involved in Scheduling
        /// </summary>
        
      
        
        
        
        
        
        
        /// <summary>
        /// Methods for Curriculum
        /// </summary>
        
        public static Curriculum GenerateCurriculum(School school, int year, ClassLevel classLevel, EducationLevel educationLevel, int semester, int id)
        {
            var curriculum = new Curriculum
            {
                Id = id,
                Year = year,
                Semester = semester,
                EducationLevel = educationLevel,
                ClassLevel = classLevel,
                ClassSessions = new List<ClassSession>()
            };
            var learningPaths = school.LearningPath
                .Where(lp => lp.ClassLevel == classLevel && lp.EducationLevel == educationLevel)
                .ToList();

            foreach (var learningPath in learningPaths)
            {
                curriculum.Semester = learningPath.Semester;

                foreach (var scheduleEntry in learningPath.Schedule)
                {
                    var classSession = scheduleEntry.ClassSession;
                    curriculum.ClassSessions.Add(classSession);
                }
            }
            return curriculum;
        }
        
        //Assign fees for all students in a specific learning path
        public static void AssignSemesterFeesToStudents(LearningPath learningPath)
        {
            if (learningPath == null) throw new ArgumentNullException(nameof(learningPath));

            foreach (var student in learningPath.Students)
            {
                if (student.Person.SchoolFees == null)
                {
                    student.Person.SchoolFees = new Schoolfees();
                }
                // Assign the semester fee
                student.Person.SchoolFees.TotalAmount = learningPath.FeePerSemester;
            }
        }
        
        //Assign fees to all students in all learning paths of a school assuming all students in the school has uniform school fees
        public static void AssignFeesForAllLearningPaths(School school)
        {
            if (school == null) throw new ArgumentNullException(nameof(school));

            foreach (var learningPath in school.LearningPath)
            {
                AssignSemesterFeesToStudents(learningPath);
            }
        }
        
        //Assign fees to list of selected learning paths 
        public static void AssignFeesForSelectedLearningPaths(List<LearningPath> selectedLearningPaths)
        {
            if (selectedLearningPaths == null || selectedLearningPaths.Count == 0)
            {
                throw new ArgumentException("The list of selected learning paths cannot be null or empty.");
            }

            foreach (var learningPath in selectedLearningPaths)
            {
                if (learningPath == null)
                {
                    throw new ArgumentException("One of the learning paths in the list is null.");
                }

                foreach (var student in learningPath.Students)
                {
                    if (student.Person.SchoolFees == null)
                    {
                        student.Person.SchoolFees = new Schoolfees();
                    }

                    // Assign the semester fee
                    student.Person.SchoolFees.TotalAmount = learningPath.FeePerSemester;
                }
            }
        }
        
        public static void DisplayStudentSchedules(LearningPath learningPath)
        {
            Console.WriteLine($"Schedules for Students in Learning Path: {learningPath.Id} - {learningPath.ClassLevel} {learningPath.EducationLevel}\n");

            foreach (var student in learningPath.Students)
            {
                Console.WriteLine($"Student: {student.Person.FirstName} {student.Person.LastName}");

                if (student.Person.PersonalCalendar != null && student.Person.PersonalCalendar.ScheduleEntries.Any())
                {
                    foreach (var entry in student.Person.PersonalCalendar.ScheduleEntries)
                    {
                        Console.WriteLine($" - Schedule Entry ID: {entry.Id}, Date: {entry.DateTime}, Duration: {entry.Duration}, Topic: {entry.ClassSession?.Topic ?? "N/A"}");
                    }
                }
                else
                {
                    Console.WriteLine(" - No schedules available.");
                }
                Console.WriteLine(); // Blank line for better readability
            }
        }
        
        
        
        
        
        public static void SynchronizeSchedulesWithStudents(LearningPath learningPath)
        {
            if (learningPath == null)
            {
                throw new ArgumentNullException(nameof(learningPath), "LearningPath cannot be null.");
            }

            if (learningPath.Students == null || !learningPath.Students.Any())
            {
                throw new InvalidOperationException("No students are enrolled in the learning path.");
            }

            foreach (var student in learningPath.Students)
            {
                // Ensure the student's calendar is initialized
                if (student.Person.PersonalCalendar == null)
                {
                    student.Person.PersonalCalendar = new Calendar
                    {
                        Id = student.ID,
                        Name = $"{student.Person.FirstName} {student.Person.LastName}'s Calendar"
                    };
                }
                // Synchronize the schedule entries from the learning path to the student's calendar
                foreach (var entry in learningPath.Schedule)
                {
                    // Avoid duplicate entries by checking if the entry already exists
                    if (!student.Person.PersonalCalendar.ScheduleEntries.Contains(entry))
                    {
                        student.Person.PersonalCalendar.ScheduleEntries.Add(entry);
                    }
                }
            }
        }

        
        
        
        
        
        
        
        
        
        
        





        //Check for any payment for a student
        public static bool IsStudentPaymentMade(Student student)
        {
            return student.Person.SchoolFees.Payments != null && student.Person.SchoolFees.Payments.Any(p => p.Amount > 0);
        }
        //Check for successful payment for a student based on half of Total amount required
        public static bool IsStudentPaymentSuccessful(Student student)
        {
            double requiredAmount = student.Person.SchoolFees.TotalAmount / 2;
            return student.Person.SchoolFees.Payments != null
                   && student.Person.SchoolFees.Payments.Any(p => p.Amount >= requiredAmount);
        }

        //Enroll student to learning path based on successful payment
        public static void EnrollStudentInLearningPath(Student student, LearningPath learningPath)
        {
            if (student == null)
            {
                throw new ArgumentNullException(nameof(student), "Student cannot be null.");
            }

            if (learningPath == null)
            {
                throw new ArgumentNullException(nameof(learningPath), "LearningPath cannot be null.");
            }

            if (!IsStudentPaymentSuccessful(student))
            {
                throw new InvalidOperationException("Student cannot be enrolled without successful payment.");
            }

            if (learningPath.Students.Contains(student))
            {
                throw new InvalidOperationException("Student is already enrolled in the learning path.");
            }

            learningPath.Students.Add(student);
        }

        //student make payment
        public static void MakePayment(Student student, double amount, string paymentMethod)
        {
            if (student?.Person?.SchoolFees == null)
            {
                throw new ArgumentException("Invalid student or school fees record.");
            }
            var payment = new Payment
            {
                Amount = amount,
                Date = DateTime.Now,
                PaymentMethod = paymentMethod
            };
            student.Person.SchoolFees.Payments.Add(payment);
        }
        
       

        //generate a student's calendar
        public static List<ScheduleEntry> GenerateStudentCalendar(School school, Student student)
        {
            if (school == null)
            {
                throw new ArgumentNullException(nameof(school), "School cannot be null.");
            }

            if (student == null)
            {
                throw new ArgumentNullException(nameof(student), "Student cannot be null.");
            }
            List<ScheduleEntry> studentCalendar = new();
            foreach (var learningPath in school.LearningPath)
            {
                if (learningPath.Students.Contains(student))
                {
                    studentCalendar.AddRange(learningPath.Schedule);
                }
            }
            studentCalendar.Sort((entry1, entry2) => entry1.DateTime.CompareTo(entry2.DateTime));

            return studentCalendar;
            
        }

        //generate teacher's calendar
        public static List<ScheduleEntry> GenerateTeacherCalendar(School school, Staff teacher)
        {
            if (school == null)
            {
                throw new ArgumentNullException(nameof(school), "School cannot be null.");
            }

            if (teacher == null)
            {
                throw new ArgumentNullException(nameof(teacher), "Teacher cannot be null.");
            }
            List<ScheduleEntry> teacherCalendar = new();

            foreach (var learningPath in school.LearningPath)
            {
                foreach (var schedule in learningPath.Schedule)
                {
                    if (schedule.ClassSession.Teacher != null && schedule.ClassSession.Teacher.Id == teacher.Id)
                    {
                        teacherCalendar.Add(schedule);
                    }
                }
            }
            teacherCalendar.Sort((entry1, entry2) => entry1.DateTime.CompareTo(entry2.DateTime));

            return teacherCalendar;
        }

        //generate school calendar
        public static List<ScheduleEntry> GenerateSchoolCalendar(School school)
        {
            if (school == null)
            {
                throw new ArgumentNullException(nameof(school), "School cannot be null.");
            }
            List<ScheduleEntry> schoolCalendar = new();
            foreach (var learningPath in school.LearningPath)
            {
                schoolCalendar.AddRange(learningPath.Schedule);
            }
            schoolCalendar.Sort((entry1, entry2) => entry1.DateTime.CompareTo(entry2.DateTime));

            return schoolCalendar;
        }

        //take attendance for class session
        public static void TakeAttendanceForClassSession(ClassSession classSession, List<Student> presentStudents, Staff teacher)
        {
            if (classSession == null)
            {
                throw new ArgumentNullException(nameof(classSession), "Class session cannot be null.");
            }
            if (teacher == null)
            {
                throw new ArgumentNullException(nameof(teacher), "Teacher cannot be null.");
            }
            if (presentStudents == null || !presentStudents.Any())
            {
                throw new ArgumentException("The list of present students cannot be null or empty.", nameof(presentStudents));
            }
            if (classSession.Teacher != teacher)
            {
                throw new InvalidOperationException("Only the assigned teacher can take attendance for this class session.");
            }

            var attendanceLogEntry = new ClassAttendanceLogEntry
            {
                Id = classSession.AttendanceLog.Count + 1,
                Teacher = teacher,
                Attendees = presentStudents,
                TimeStamp = DateTime.Now
            };

            classSession.AttendanceLog.Add(attendanceLogEntry);
        }

        //Retrieve a student's attendance for a particular course in a semester
        public static List<ClassAttendanceLogEntry> GetStudentAttendanceForCourse(
        Student student,
        string course,
        LearningPath learningPath)
        {
            if (student == null)
            {
                throw new ArgumentNullException(nameof(student), "Student cannot be null.");
            }
            if (string.IsNullOrWhiteSpace(course))
            {
                throw new ArgumentException("Course cannot be null or empty.", nameof(course));
            }
            if (learningPath == null)
            {
                throw new ArgumentNullException(nameof(learningPath), "Learning path cannot be null.");
            }

            var validCourses = CourseDefaults.GetCourseNames(learningPath.EducationLevel);
            if (!validCourses.Contains(course))
            {
                throw new ArgumentException($"The course '{course}' is not valid for the education level {learningPath.EducationLevel}.", nameof(course));
            }

            // Retrieve all schedule entries in the learning path
            var scheduleEntries = learningPath.Schedule;

            // Filter schedule entries for the specified course
            var courseSessions = scheduleEntries
                .Where(se => se.ClassSession.Course.Equals(course, StringComparison.OrdinalIgnoreCase))
                .Select(se => se.ClassSession)
                .ToList();

            // Get attendance logs where the student is marked as present
            var attendanceLogs = courseSessions
                .SelectMany(cs => cs.AttendanceLog)
                .Where(log => log.Attendees.Contains(student))
                .ToList();

            return attendanceLogs;
        }

        //Get attendance of all the students in a particular learning path for a day
        public static List<ClassAttendanceLogEntry> GetAttendanceForLearningPathOnDay(
        LearningPath learningPath,
        DateTime date)
        {
            if (learningPath == null)
            {
                throw new ArgumentNullException(nameof(learningPath), "Learning path cannot be null.");
            }

            var relevantScheduleEntries = learningPath.Schedule
                .Where(se => se.DateTime.Date == date.Date)
                .ToList();

            if (!relevantScheduleEntries.Any())
            {
                Console.WriteLine($"No class sessions were scheduled for the date {date.ToShortDateString()}.");
                return new List<ClassAttendanceLogEntry>();
            }

            var attendanceLogs = relevantScheduleEntries
                .SelectMany(se => se.ClassSession.AttendanceLog)
                .ToList();

            return attendanceLogs;
        }

        //Get attendance of a teacher for a specified time period
        public static List<ClassAttendanceLogEntry> GetTeacherAttendanceForPeriod(
           Staff teacher,
           List<LearningPath> learningPaths,
           DateTime startDate,
           DateTime endDate)
        {
            if (teacher == null)
                throw new ArgumentNullException(nameof(teacher), "Teacher cannot be null.");

            if (learningPaths == null || !learningPaths.Any())
                throw new ArgumentException("Learning paths list cannot be null or empty.", nameof(learningPaths));

            if (endDate < startDate)
                throw new ArgumentException("End date must be greater than or equal to start date.");

            var teacherAttendance = new List<ClassAttendanceLogEntry>();

            foreach (var learningPath in learningPaths)
            {
                foreach (var schedule in learningPath.Schedule)
                {
                    var classSession = schedule.ClassSession;
                    var attendanceLogs = classSession.AttendanceLog
                        .Where(log => log.Teacher.Id == teacher.Id &&
                                      log.TimeStamp >= startDate &&
                                      log.TimeStamp <= endDate)
                        .ToList();

                    teacherAttendance.AddRange(attendanceLogs);
                }
            }
            return teacherAttendance;
        }

        //To retrieve all Grades of all students for a particular course
        public static List<TestGrade> GetAllGradesForCourse(string courseName, List<Student> students)
        {
            if (string.IsNullOrWhiteSpace(courseName))
                throw new ArgumentException("Course name cannot be null or empty.", nameof(courseName));

            if (students == null || !students.Any())
                return new List<TestGrade>();

            var allGrades = new List<TestGrade>();

            foreach (var student in students)
            {
                var courseGrades = student.CourseGrade?.TestGrades
                    .Where(grade => grade.Course == courseName)
                    .ToList();

                if (courseGrades != null)
                    allGrades.AddRange(courseGrades);
            }

            return allGrades;
        }

        //To retrieve the homework Grades of a students for a particular course
        public static List<TestGrade> GetHomeworkScoresForCourse(Student student, string courseName)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student), "Student cannot be null.");

            if (string.IsNullOrWhiteSpace(courseName))
                throw new ArgumentException("Course name cannot be null or empty.", nameof(courseName));

            // Filter for homework grades for the specified course
            return student.CourseGrade?.TestGrades
                .Where(grade => grade.Course == courseName && grade.GradeType == GradeType.Homework)
                .ToList() ?? new List<TestGrade>();
        }






    }
}

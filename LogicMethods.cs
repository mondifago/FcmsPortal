using FcmsPortal.Enums;
using FcmsPortal.ViewModel;

namespace FcmsPortal
{
    internal static class LogicMethods
    {
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

        //create new class session
        public static ClassSession CreateNewClassSession(
        int id,
        string course,
        string topic,
        string description,
        string lessonNote,
        string homeWork,
        Staff teacher,
        string teacherRemarks
        )
        {
            if (teacher == null)
            {
                throw new NullReferenceException("The teacher cannot be null when creating a class session.");
            }

            var classSession = new ClassSession
            {
                Id = id,
                Course = course,
                Topic = topic,
                Description = description,
                LessonNote = lessonNote,
                HomeWork = homeWork,
                Teacher = teacher,
                TeacherRemarks = teacherRemarks
            };
            return classSession;
        }

        // add a teacher to a class session
        public static void AddTeacherToClassSession(ClassSession classSession, Staff teacher)
        {
            if (classSession == null)
            {
                throw new ArgumentNullException(nameof(classSession), "Class session cannot be null.");
            }

            if (teacher == null)
            {
                throw new ArgumentNullException(nameof(teacher), "Teacher cannot be null.");
            }
            classSession.Teacher = teacher;
        }

        //create new schedule
        public static ScheduleEntry CreateScheduleEntry(int id, DateTime dateTime, TimeSpan duration, ClassSession classSession)
        {
            if (classSession == null)
            {
                throw new ArgumentNullException(nameof(classSession), "Class session cannot be null.");
            }
            var scheduleEntry = new ScheduleEntry
            {
                Id = id,
                DateTime = dateTime,
                Duration = duration,
                ClassSession = classSession
            };
            return scheduleEntry;
        }

        //schedule a class session
        public static void ScheduleClassSession(LearningPath learningPath, ClassSession classSession, int scheduleId, DateTime dateTime, TimeSpan duration)
        {
            if (learningPath == null)
            {
                throw new ArgumentNullException(nameof(learningPath), "LearningPath cannot be null.");
            }

            if (classSession == null)
            {
                throw new ArgumentNullException(nameof(classSession), "ClassSession cannot be null.");
            }
            var scheduleEntry = new ScheduleEntry
            {
                Id = scheduleId,
                DateTime = dateTime,
                Duration = duration,
                ClassSession = classSession
            };

            learningPath.Schedule.Add(scheduleEntry);
        }

        //add a schedule to a learning path
        public static void AddScheduleToLearningPath(LearningPath learningPath, ScheduleEntry scheduleEntry)
        {
            if (learningPath == null)
            {
                throw new ArgumentNullException(nameof(learningPath), "Learning path cannot be null.");
            }

            if (scheduleEntry == null)
            {
                throw new ArgumentNullException(nameof(scheduleEntry), "Schedule entry cannot be null.");
            }

            if (learningPath.Schedule.Any(s => s.Id == scheduleEntry.Id))
            {
                throw new ArgumentException($"A schedule with ID {scheduleEntry.Id} already exists in the learning path.");
            }
            learningPath.Schedule.Add(scheduleEntry);
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

        //Retieve a student's attendance for a particular course in a semester
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




    }
}

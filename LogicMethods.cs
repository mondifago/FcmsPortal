﻿using FcmsPortal.Enums;
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
            if (IsStudentPaymentSuccessful(student))
            {
                learningPath.Students.Add(student);
            }
            else
            {
                throw new InvalidOperationException("Student cannot be enrolled without successful payment.");
            }
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

        //create new school
        public static School CreateNewSchool(
        string schoolName,
        Address schoolAddress,
        List<Staff>? initialStaff = null,
        List<Student>? initialStudents = null
        )
        {
            var newSchool = new School
            {
                Name = schoolName,
                SchoolAddress = schoolAddress,
                Staff = initialStaff ?? new List<Staff>(),
                Students = initialStudents ?? new List<Student>(),
                LearningPath = new List<LearningPath>(),
                SchoolCalendar = new List<SchoolCalendar>(),
                Grades = new List<CourseGrade>()
            };
            return newSchool;
        }

        //create new teacher
        public static Staff CreateNewTeacher(
        int id,
        string firstName,
        string middleName,
        string lastName,
        Gender gender,
        DateTime dateOfBirth,
        DateOnly dateOfEmployment,
        string jobRole,
        string areaOfSpecialization,
        List<string>? qualifications = null,
        List<string>? workExperience = null
        )
        {
            var teacher = new Staff
            {
                Id = id,
                Person = new Person
                {
                    FirstName = firstName,
                    MiddleName = middleName,
                    LastName = lastName,
                    Sex = gender,
                    DateOfBirth = dateOfBirth
                },
                JobRole = jobRole,
                AreaOfSpecialization = areaOfSpecialization,
                Qualifications = qualifications ?? new List<string>(),
                WorkExperience = workExperience ?? new List<string>(),
                DateOfEmployment = dateOfEmployment
            };
            return teacher;
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
            if (teacher != null)
                classSession.Teacher = teacher;
        }

        //create new schedule
        public static ScheduleEntry CreateScheduleEntry(int id, DateTime dateTime, TimeSpan duration, ClassSession classSession)
        {
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
            var scheduleEntry = new ScheduleEntry
            {
                Id = scheduleId,
                DateTime = dateTime,
                Duration = duration,
                ClassSession = classSession
            };

            learningPath.Schedule.Add(scheduleEntry);
        }

        //create new learning path
        public static LearningPath CreateLearningPath(int id, EducationLevel educationLevel, ClassLevel classLevel, int semester)
        {
            var learningPath = new LearningPath
            {
                Id = id,
                EducationLevel = educationLevel,
                ClassLevel = classLevel,
                Semester = semester
            };
            return learningPath;
        }

        //add a schedule to a learning path
        public static void AddScheduleToLearningPath(LearningPath learningPath, ScheduleEntry scheduleEntry)
        {
            learningPath.Schedule.Add(scheduleEntry);
        }

        //generate a student's calendar
        public static List<ScheduleEntry> GenerateStudentCalendar(School school, Student student)
        {
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
            List<ScheduleEntry> schoolCalendar = new();
            foreach (var learningPath in school.LearningPath)
            {
                schoolCalendar.AddRange(learningPath.Schedule);
            }
            schoolCalendar.Sort((entry1, entry2) => entry1.DateTime.CompareTo(entry2.DateTime));

            return schoolCalendar;
        }








    }
}

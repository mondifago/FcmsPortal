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
            Console.WriteLine($"Payment of {amount:C} successfully made by {student.Person.FirstName}. Remaining Balance: {student.Person.SchoolFees.Balance:C}");
        }

        //show all student's payments
        public static void ShowAllPayments(Student student)
        {
            if (student.Person.SchoolFees.Payments.Count == 0)
            {
                Console.WriteLine("No payments have been made.");
                return;
            }

            Console.WriteLine("List of Payments:");
            foreach (var payment in student.Person.SchoolFees.Payments)
            {
                Console.WriteLine($"Amount: {payment.Amount}, Date: {payment.Date}, Reference: {payment.Reference}");
            }
        }

        // Create a new student
        public static Student CreateNewStudent(
        int id,
        string studentFirstName,
        string studentMiddleName,
        string studentLastName,
        Gender studentGender,
        DateTime studentDateOfBirth,
        EducationLevel educationLevel,
        ClassLevel classLevel,
        string guardianFirstName,
        string guardianMiddleName,
        string guardianLastName,
        Relationship guardianRelationship,
        School school
        )
        {

            var newStudent = new Student
            {
                ID = id,
                Person = new Person
                {
                    FirstName = studentFirstName,
                    MiddleName = studentMiddleName,
                    LastName = studentLastName,
                    Sex = studentGender,
                    DateOfBirth = studentDateOfBirth,
                    EducationLevel = educationLevel,
                    ClassLevel = classLevel
                },
                GuardianInfo = new Guardian
                {
                    RelationshipToStudent = guardianRelationship,
                    Person = new Person
                    {
                        FirstName = guardianFirstName,
                        MiddleName = guardianMiddleName,
                        LastName = guardianLastName
                    }
                }
            };
            school.Students.Add(newStudent);
            return newStudent;
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
            classSession.Teacher = teacher;
        }









    }
}

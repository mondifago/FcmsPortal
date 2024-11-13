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









    }
}

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
        //Check for successful payment for a student based on specific amount
        public static bool IsStudentPaymentSuccessful(Student student, double requiredAmount)
        {
            return student.Person.SchoolFees.Payments != null && student.Person.SchoolFees.Payments.Any(p => p.Amount >= requiredAmount);
        }
       
        
        
        


    }
}

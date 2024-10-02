namespace FcmsPortal
{
    internal class Program
    {
        /// <summary>
        /// create one school
        /// create one student
        /// create the student's parent
        /// create the student's teacher
        /// create an admin staff
        /// </summary>
        /// <param name="args"></param>
        /// 

        static void Main(string[] args)
        {
            // create a school with name and address
            School fcmSchool = new School
            {
                Name = "FCM School",
                SchoolAddress = new Address
                {
                    Street = "120 City Road",
                    City = "Asaba",
                    State = "Delta State",
                    PostalCode = "P.O.Box 150",
                    Country = "Nigeria"
                }
            };



            /*
            //create one teacher one student

            var teacher = new Staff { FirstName = "John", MiddleName = "Paul", LastName = "Kent" };
            var student1 = new Person { FirstName = "Scott", MiddleName = "Silas", LastName = "Anthony" };

            fcmSchool.Staff.Add(teacher);
            fcmSchool.Students.Add(student1);


            //create a class session biology class for babies:

            var cl = new ClassSession()
            {
                Name = "Bio 101",
                Topic = "Digestive system",
                Teacher = teacher,
                Students = new List<Person> { student1 },
                EducationLevel = EducationLevel.Kindergarten,
                ClassLevel = "Pre-Nursry",
                Semester = 2,
                Duration = TimeSpan.FromHours(1)
            };*/

        }

    }
}










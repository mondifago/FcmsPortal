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

            //create a mathematics teacher
            Staff mathTeacher = new Staff
            {
                StaffId = 1234,
                AreaOfSpecialization = new List<Course> { new Course { Name = "Mathematics" } },
                JobRole = "Primary Education Teacher",
                TheStaff = new Person
                {
                    FirstName = "John",
                    MiddleName = "Michael",
                    LastName = "Smith",
                    Sex = Gender.Male,
                    EducationLevel = EducationLevel.Primary,
                }
            };

            // Add the mathematics teacher to the school's staff list
            fcmSchool.Staff.Add(mathTeacher);






        }

    }
}










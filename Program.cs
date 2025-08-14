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

        static void Main(string[] args)
        {
            // create a school with name and address
            Address address = new Address();
            School fcmSchool = new School();
            fcmSchool.Name = "FCM School";
            fcmSchool.Staff = new List<Staff>();
            fcmSchool.Students = new List<Student>();
            fcmSchool.LearningPaths = new List<LearningPath>();
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





using FcmsPortal.Models;

namespace FcmsPortal
{
    public class Program
    {
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





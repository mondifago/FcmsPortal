namespace FcmsPortal
{
    public class StaffAttendee : IAttendee
    {
        public Person TheStaff { get; set; }
        public string AdminRole { get; set; }
        public int StaffId { get; set; }

        public Person PersonInfo => TheStaff;
        public string Role => $"Staff ({AdminRole})";
        public int Id => StaffId;
    }
}

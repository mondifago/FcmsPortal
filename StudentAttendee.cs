namespace FcmsPortal
{
    public class StudentAttendee : IAttendee
    {
        public Person TheStudent { get; set; }
        public int StudentId { get; set; }
        public EducationLevel EducationLevel { get; set; }
        public ClassLevel ClassLevel { get; set; }

        public Person PersonInfo => TheStudent;
        public string Role => $"Student ({EducationLevel} - {ClassLevel.GetLevelName()})";
        public int Id => StudentId;
    }
}

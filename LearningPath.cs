﻿namespace FcmsPortal
{
    public class LearningPath
    {
        public int Id { get; set; }
        public EducationLevel EducationLevel { get; set; }
        public ClassLevel ClassLevel { get; set; }
        public Semester Semester { get; set; }
        public List<ScheduleEntry> Schedule { get; set; } = new List<ScheduleEntry>();
        public List<Student> Students { get; set; } = new List<Student>();
        public List<Student> StudentsPaymentSuccesful { get; set; } = new List<Student>();

    }
}

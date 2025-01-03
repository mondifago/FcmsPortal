﻿using FcmsPortal.Enums;

namespace FcmsPortal
{
    public class LearningPath
    {
        public int Id { get; set; }
        public EducationLevel EducationLevel { get; set; }
        public ClassLevel ClassLevel { get; set; }
        public int Semester { get; set; }
        public List<ScheduleEntry> Schedule { get; set; } = new List<ScheduleEntry>();
        public double FeePerSemester { get; set; }
        public List<Student> Students { get; set; } = new List<Student>();
        public List<Student> StudentsPaymentSuccessful { get; set; } = new List<Student>();

    }
}

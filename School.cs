﻿namespace FcmsPortal
{
    public class School
    {
        public string Name { get; set; }
        public Address SchoolAddress { get; set; }
        public List<Staff> Staff { get; set; } = new();
        public List<Student> Students { get; set; } = new();
        public List<LearningPath> LearningPath { get; set; } = new();
        public List<SchoolCalendar> SchoolCalendar { get; set; }
        public List<CourseGrade> Grades { get; set; } = new();
    }
}

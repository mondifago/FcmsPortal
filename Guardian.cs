﻿namespace FcmsPortal
{
    public class Guardian
    {
        private Person _person;
        public Person Person
        {
            get { return _person; }
            set { _person = value; }
        }

        private Relationship _relationshipToStudent;
        public Relationship RelationshipToStudent
        {
            get { return _relationshipToStudent; }
            set { _relationshipToStudent = value; }
        }

        private List<Student> _childInfo;
        public List<Student> ChildInfo
        {
            get { return _childInfo; }
            set { _childInfo = value; }
        }

        private string _occupation;
        public string Occupation
        {
            get { return _occupation; }
            set { _occupation = value; }
        }

    }
}

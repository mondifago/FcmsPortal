﻿namespace FcmsPortal
{
    public class Guardian
    {
        private Person _theGuardian;
        public Person TheGuardian
        {
            get { return _theGuardian; }
            set { _theGuardian = value; }
        }

        private string _relationshipToStudent;
        public string RelationshipToStudent
        {
            get { return _relationshipToStudent; }
            set { _relationshipToStudent = value; }
        }

        private List<Student> _childrenIds;
        public List<Student> ChildrenIds
        {
            get { return _childrenIds; }
            set { _childrenIds = value; }
        }

        private string _occupation;
        public string Occupation
        {
            get { return _occupation; }
            set { _occupation = value; }
        }

    }
}

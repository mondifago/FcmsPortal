using FcmsPortal.Enums;

namespace FcmsPortal.Models
{
    public class Guardian
    {
        public int Id { get; set; } 
        
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

        private string _occupation;
        public string Occupation
        {
            get { return _occupation; }
            set { _occupation = value; }
        }
    }
}

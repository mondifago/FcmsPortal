namespace FcmsPortal
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

        private List<int> _childrenIds;
        public List<int> ChildrenIds
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

        private Payment _paymentInfo;

        public Payment PaymentInfo
        {
            get { return _paymentInfo; }
            set { _paymentInfo = value; }
        }

    }
}

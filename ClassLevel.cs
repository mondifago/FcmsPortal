namespace FcmsPortal
{
    public class ClassLevel
    {
        private Kindergarten _myKindergarten;

        public Kindergarten MyKindergarten
        {
            get { return _myKindergarten; }
            set { _myKindergarten = value; }
        }

        private Primary _myPrimary;

        public Primary MyPrimary
        {
            get { return _myPrimary; }
            set { _myPrimary = value; }
        }

        private JuniorCollege _myJuniorCollege;

        public JuniorCollege MyJuniorCollege
        {
            get { return _myJuniorCollege; }
            set { _myJuniorCollege = value; }
        }

        private SeniorCollege _mySeniorCollege;

        public SeniorCollege MySeniorCollege
        {
            get { return _mySeniorCollege; }
            set { _mySeniorCollege = value; }
        }

    }
}

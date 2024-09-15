namespace FcmsPortal
{
    public class ClassLevel
    {
        private Kindergarten _kindergartenLevel;

        public Kindergarten KindergartenLevel
        {
            get { return _kindergartenLevel; }
            set { _kindergartenLevel = value; }
        }

        private Primary _primaryLevel;

        public Primary PrimaryLevel
        {
            get { return _primaryLevel; } 
            set { _primaryLevel = value; }
        }

        private JuniorCollege _juniorCollegeLevel;

        public JuniorCollege JuniorCollegeLevel
        {
            get { return _juniorCollegeLevel; }
            set { _juniorCollegeLevel = value; }
        }

        private SeniorCollege _seniorCollegeLevel;

        public SeniorCollege SeniorCollegeLevel
        {
            get { return _seniorCollegeLevel; }
            set { _seniorCollegeLevel = value; }
        }
        
        public EducationLevel Level { get; private set; }
        public object SubLevel { get; private set; }

        public void SetClassLevel(EducationLevel level, object subLevel)
        {
            Level = level;
            SubLevel = subLevel;
        }

    }
}

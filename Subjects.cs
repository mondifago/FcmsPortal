namespace FcmsPortal
{
    public class Subjects
    {
        private PreNurserySubjects _preNurserySubjects;

        public PreNurserySubjects PreNurserySubjects
        {
            get { return _preNurserySubjects; }
            set { _preNurserySubjects = value; }
        }

        private NurserySubjects _nurserySubjects;

        public NurserySubjects NurserySubjects
        {
            get { return _nurserySubjects; }
            set { _nurserySubjects = value; }
        }

        private PrimarySubjects _primarySubjects;

        public PrimarySubjects PrimarySubjects
        {
            get { return _primarySubjects; }
            set { _primarySubjects = value; }
        }

        private JuniorCollegeSubjects _juniorCollegeSubjects;

        public JuniorCollegeSubjects juniorCollegeSubjects
        {
            get { return _juniorCollegeSubjects; }
            set { _juniorCollegeSubjects = value; }
        }

        private SeniorCollegeSubjects _seniorCollegeSubjects;

        public SeniorCollegeSubjects SeniorCollegeSubjects
        {
            get { return _seniorCollegeSubjects; }
            set { _seniorCollegeSubjects = value; }
        }

    }
}

namespace FcmsPortal
{
    public class SeniorCollege : ClassLevel
    {
        public enum Classes
        {
            Sss1,
            Sss2,
            Sss3
        }

        public Classes GetClass { get; set; }

        public override string GetLevelName()
        {
            return GetClass.ToString();
        }
    }

}

namespace FcmsPortal
{
    public class JuniorCollege : ClassLevel
    {
        public enum Classes
        {
            Jss1,
            Jss2,
            Jss3
        }
        public Classes GetClass { get; set; }
        public override string GetLevelName()
        {
            return GetClass.ToString();

        }
    }
}

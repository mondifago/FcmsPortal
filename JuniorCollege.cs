namespace FcmsPortal
{
    public class JuniorCollege : ClassLevel
    {
        public enum Level
        {
            Jss1,
            Jss2,
            Jss3
        }
        public Level CurrentLevel { get; set; }
        public override string GetLevelName()
        {
            return CurrentLevel.ToString();

        }
    }
}

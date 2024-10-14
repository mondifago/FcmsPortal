namespace FcmsPortal
{
    public class SeniorCollege : ClassLevel
    {
        public enum Level
        {
            Sss1,
            Sss2,
            Sss3
        }
        public Level CurrentLevel { get; set; }
        public override string GetLevelName()
        {
            return CurrentLevel.ToString();
        }
    }
}

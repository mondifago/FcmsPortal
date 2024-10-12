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
        public Level GetCurrentLevel { get; set; }
        public override string GetLevelName()
        {
            return GetCurrentLevel.ToString();
        }
    }
}

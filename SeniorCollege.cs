namespace FcmsPortal
{
    public class SeniorCollege : ClassLevel
    {
        public enum Level
        {
            SeniorCollege1,
            SeniorCollege2,
            SeniorCollege3
        }
        public Level CurrentLevel { get; set; }
        public override string GetLevelName()
        {
            return CurrentLevel.ToString();
        }
    }
}

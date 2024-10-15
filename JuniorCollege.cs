namespace FcmsPortal
{
    public class JuniorCollege : ClassLevel
    {
        public enum Level
        {
            JuniorCollege1,
            JuniorCollege2,
            JuniorCollege3
        }
        public Level CurrentLevel { get; set; }
        public override string GetLevelName()
        {
            return CurrentLevel.ToString();

        }
    }
}

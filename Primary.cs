namespace FcmsPortal
{
    public class Primary : ClassLevel
    {
        public enum Level
        {
            Primary1,
            Primary2,
            Primary3,
            Primary4,
            Primary5,
            Primary6
        }
        public Level CurrentLevel { get; set; }
        public override string GetLevelName()
        {
            return CurrentLevel.ToString();

        }
    }
}

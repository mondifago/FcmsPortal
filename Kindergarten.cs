namespace FcmsPortal
{
    public class Kindergarten : ClassLevel
    {
        public enum Level
        {
            Daycare,
            PlayGroup,
            PreNursery,
            Nursery
        }
        public Level CurrentLevel { get; set; }
        public override string GetLevelName()
        {
            return CurrentLevel.ToString();

        }
    }
}

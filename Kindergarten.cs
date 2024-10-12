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
        public Level GetCurrentLevel { get; set; }
        public override string GetLevelName()
        {
            return GetCurrentLevel.ToString();

        }
    }
}

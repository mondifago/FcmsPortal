namespace FcmsPortal
{
    public class Kindergarten : ClassLevel
    {
        public enum Level
        {
            KG_Daycare,
            KG_KG_PlayGroup,
            KG_PreNursery,
            KG_Nursery
        }
        public Level CurrentLevel { get; set; }
        public override string GetLevelName()
        {
            return CurrentLevel.ToString();

        }
    }
}

namespace FcmsPortal
{
    public class Kindergarten : ClassLevel
    {
        public enum Classes
        {
            Daycare,
            PlayGroup,
            PreNursery,
            Nursery
        }

        public Classes GetClass { get; set; }

        public override string GetLevelName()
        {
            return GetClass.ToString();
        }
    }


}

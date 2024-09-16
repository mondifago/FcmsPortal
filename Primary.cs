namespace FcmsPortal
{
    public class Primary : ClassLevel
    {
        public enum Classes
        {
            Primary1,
            Primary2,
            Primary3,
            Primary4,
            Primary5,
            Primary6
        }

        public Classes GetClass { get; set; }

        public override string GetLevelName()
        {
            return GetClass.ToString();
        }
    }

}

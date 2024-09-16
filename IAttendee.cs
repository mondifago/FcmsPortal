namespace FcmsPortal
{
    public interface IAttendee
    {
        Person PersonInfo { get; }
        string Role { get; }
        int Id { get; }
    }
}

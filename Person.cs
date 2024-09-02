namespace FcmsPortal
{
    public class Person
    {
        public byte[] ProfilePicture { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public Address HomeAddress { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }            // Active status of the staff member
    }
}

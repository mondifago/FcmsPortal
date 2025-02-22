using FcmsPortal.Enums;
using FcmsPortal.Models;

namespace FcmsPortal.Services
{
    public class AddressService
    {
        private List<Address> _addresses = new List<Address>
        {
            // Sample data for testing
            new Address { Id = 1, Street = "123 Main St", City = "Springfield", State = "IL", PostalCode = "62701", Country = "USA", AddressType = AddressType.Home },
            new Address { Id = 2, Street = "456 Oak Ave", City = "Boston", State = "MA", PostalCode = "02108", Country = "USA", AddressType = AddressType.Office }
        };

        public List<Address> GetAddresses() => _addresses;

        public void AddAddress(Address address)
        {
            address.Id = _addresses.Any() ? _addresses.Max(a => a.Id) + 1 : 1;
            _addresses.Add(address);
        }

        public void DeleteAddress(int id)
        {
            var address = _addresses.FirstOrDefault(a => a.Id == id);
            if (address != null)
            {
                _addresses.Remove(address);
            }
        }
    }
}

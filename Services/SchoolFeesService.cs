using FcmsPortal.Enums;
using FcmsPortal.Models;

namespace FcmsPortal.Services
{
    public class SchoolFeesService
    {
        private static List<SchoolFees> _schoolFees = new List<SchoolFees>
        {
            new SchoolFees
            {
                Id = 1,
                TotalAmount = 5000.00,
                Payments = new List<Payment>
                {
                    new Payment
                    {
                        Id = 1,
                        Amount = 500.00,
                        Date = DateTime.Now.AddDays(-30),
                        PaymentMethod = PaymentMethod.BankTransfer,
                        Reference = 12345
                    },
                    new Payment
                    {
                        Id = 2,
                        Amount = 750.00,
                        Date = DateTime.Now.AddDays(-15),
                        PaymentMethod = PaymentMethod.Card,
                        Reference = 23456
                    }
                }
            },
            new SchoolFees
            {
                Id = 2,
                TotalAmount = 3500.00,
                Payments = new List<Payment>()
            }
        };

        public static List<SchoolFees> GetAllSchoolFees()
        {
            return _schoolFees;
        }

        public static SchoolFees GetSchoolFees(int id)
        {
            return _schoolFees.FirstOrDefault(sf => sf.Id == id);
        }

        public static void AddSchoolFees(SchoolFees schoolFees)
        {
            schoolFees.Id = GetNextId();

            if (schoolFees.Payments == null)
            {
                schoolFees.Payments = new List<Payment>();
            }
            _schoolFees.Add(schoolFees);
        }

        public static void UpdateSchoolFees(SchoolFees schoolFees)
        {
            var existingSchoolFees = _schoolFees.FirstOrDefault(sf => sf.Id == schoolFees.Id);
            if (existingSchoolFees != null)
            {
                int index = _schoolFees.IndexOf(existingSchoolFees);
                _schoolFees[index] = schoolFees;
            }
        }

        public static void DeleteSchoolFees(int id)
        {
            var schoolFees = _schoolFees.FirstOrDefault(sf => sf.Id == id);
            if (schoolFees != null)
            {
                _schoolFees.Remove(schoolFees);
            }
        }

        public static int GetNextId()
        {
            return _schoolFees.Count > 0 ? _schoolFees.Max(sf => sf.Id) + 1 : 1;
        }
    }
}
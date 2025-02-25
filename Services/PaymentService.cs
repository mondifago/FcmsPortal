using FcmsPortal.Enums;
using FcmsPortal.Models;

namespace FcmsPortal.Services
{
    public class PaymentService
    {
        private static List<Payment> _payments = new List<Payment>
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
        };

        public static List<Payment> GetAllPayments()
        {
            return _payments;
        }

        public static Payment GetPayment(int id)
        {
            return _payments.FirstOrDefault(p => p.Id == id);
        }

        // Add a new payment
        public static void AddPayment(Payment payment)
        {
            _payments.Add(payment);

            // Update the corresponding SchoolFees if needed
            // This would require a reference to which SchoolFees this payment belongs to
            // For example:
            //var schoolFees = SchoolFeesService.GetSchoolFees(payment.SchoolFeesId);
            //schoolFees.Payments.Add(payment);
            //SchoolFeesService.UpdateSchoolFees(schoolFees);
        }

        public static void UpdatePayment(Payment payment)
        {
            var existingPayment = _payments.FirstOrDefault(p => p.Id == payment.Id);
            if (existingPayment != null)
            {
                int index = _payments.IndexOf(existingPayment);
                _payments[index] = payment;
            }
        }

        public static void DeletePayment(int id)
        {
            var payment = _payments.FirstOrDefault(p => p.Id == id);
            if (payment != null)
            {
                _payments.Remove(payment);
            }
        }

        public static int GetNextId()
        {
            return _payments.Count > 0 ? _payments.Max(p => p.Id) + 1 : 1;
        }
    }
}
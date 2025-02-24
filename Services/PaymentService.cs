using FcmsPortal.Enums;
using FcmsPortal.Models;

namespace FcmsPortal.Services
{
    public class PaymentService
    {
        private List<Payment> _payments = new List<Payment>
        {
            new Payment { Id = 1, Amount = 150.00, Date = DateTime.Now.AddDays(-10), PaymentMethod = PaymentMethod.Cash, Reference = 1001 },
            new Payment { Id = 2, Amount = 200.50, Date = DateTime.Now.AddDays(-5), PaymentMethod = PaymentMethod.Card, Reference = 1002 }
        };

        public List<Payment> GetPayments() => _payments;

        public void AddPayment(Payment payment)
        {
            payment.Id = _payments.Any() ? _payments.Max(p => p.Id) + 1 : 1;
            _payments.Add(payment);
        }

        public void UpdatePayment(Payment updatedPayment)
        {
            var existingPayment = _payments.FirstOrDefault(p => p.Id == updatedPayment.Id);
            if (existingPayment != null)
            {
                existingPayment.Amount = updatedPayment.Amount;
                existingPayment.Date = updatedPayment.Date;
                existingPayment.PaymentMethod = updatedPayment.PaymentMethod;
                existingPayment.Reference = updatedPayment.Reference;
            }
        }

        public void DeletePayment(int id)
        {
            var payment = _payments.FirstOrDefault(p => p.Id == id);
            if (payment != null)
            {
                _payments.Remove(payment);
            }
        }
    }
}
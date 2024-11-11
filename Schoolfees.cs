namespace FcmsPortal
{
    public class Schoolfees
    {
        public double TotalAmount { get; set; }
        public double Balance
        {
            get
            {
                double totalPayments = Payments.Sum(p => p.Amount);
                return TotalAmount - totalPayments;
            }
        }
        public List<Payment> Payments { get; set; } = new List<Payment>();
    }
}

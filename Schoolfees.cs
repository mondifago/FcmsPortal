namespace FcmsPortal
{
    public class Schoolfees
    {
        public double TotalAmount { get; set; }
        public List<Payment> Payments { get; set; } = new List<Payment>();
    }
    public class Payment
    {
        public double Amount { get; set; }
        public DateTime Date { get; set; }
    }
}

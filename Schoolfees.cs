namespace FcmsPortal
{
    public class Schoolfees
    {
        public double totalamount;
        public List<Payment> Payments;
    }
    public class Payment
    {
        public double Amount { get; set; }
        public DateTime Date { get; set; }
    }
}

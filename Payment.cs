namespace FcmsPortal
{
    public class Payment
    {
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string PaymentMethod { get; set; }
        public int Reference { get; set; }
    }
}

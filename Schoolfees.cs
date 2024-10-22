namespace FcmsPortal
{
    public class Schoolfees
    {
        public double TotalAmount { get; set; }
        public List<Payment> Payments { get; set; } = new List<Payment>();
    }
}

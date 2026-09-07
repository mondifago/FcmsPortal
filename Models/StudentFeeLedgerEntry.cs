using FcmsPortal.Enums;

namespace FcmsPortal.Models
{
    public class StudentFeeLedgerEntry
    {
        public string AcademicYear { get; set; } = string.Empty;
        public ClassLevel ClassLevel { get; set; }
        public Semester Semester { get; set; }
        public double BroughtForward { get; set; }
        public double TermFee { get; set; }
        public double Discount { get; set; }
        public double TermPayable { get; set; }
        public double TotalPayable { get; set; }
        public double Paid { get; set; }
        public double CarriedForward { get; set; } 
    }
}

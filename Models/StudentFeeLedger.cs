namespace FcmsPortal.Models
{
    public class StudentFeeLedger
    {
        public DateTime DateAndTimeLedgerGenerated { get; set; }
        public double ClosingBalance { get; set; }
        public List<StudentFeeLedgerEntry> Entries { get; set; } = new();
    }
}

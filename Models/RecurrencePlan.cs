using FcmsPortal.Enums;

namespace FcmsPortal.Models
{
    public class RecurrencePlan
    {
        public DateTime StartDateTime { get; set; }
        public RecurrenceType Pattern { get; set; }
        public int Interval { get; set; } = 1;
        public DateTime EndDate { get; set; }
    }
}

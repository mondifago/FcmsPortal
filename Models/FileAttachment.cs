namespace FcmsPortal.Models
{
    public class FileAttachment
    {
        public int Id { get; set; }

        public string FileName { get; set; } = string.Empty;

        public string FilePath { get; set; } = string.Empty;

        public DateTime UploadDate { get; set; } = DateTime.Now;

        public long FileSize { get; set; }
    }
}

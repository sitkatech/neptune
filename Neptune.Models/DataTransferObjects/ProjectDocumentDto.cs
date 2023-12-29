namespace Neptune.Models.DataTransferObjects
{
    public class ProjectDocumentDto
    {
        public int ProjectDocumentID { get; set; }
        public int FileResourceID { get; set; }
        public int ProjectID { get; set; }
        public string DisplayName { get; set; }
        public DateOnly UploadDate { get; set; }
        public string DocumentDescription { get; set; }
        public FileResourceSimpleDto FileResource { get; set; }
    }
}

namespace Neptune.Models.DataTransferObjects
{
    public class FileResourceSimpleDto
    {
        public int FileResourceID { get; set; }
        public int FileResourceMimeTypeID { get; set; }
        public Guid FileResourceGUID { get; set; }
        public int CreatePersonID { get; set; }
        public DateTime CreateDate { get; set; }
        public long ContentLength { get; set; }
        public string OriginalBaseFilename { get; set; }
        public string OriginalFileExtension { get; set; }
        public string OriginalFilename { get; set; }
    }
}
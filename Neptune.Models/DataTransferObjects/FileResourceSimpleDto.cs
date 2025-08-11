//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FileResource]

namespace Neptune.Models.DataTransferObjects
{
    public partial class FileResourceSimpleDto
    {
        public int FileResourceID { get; set; }
        public int FileResourceMimeTypeID { get; set; }
        public string OriginalBaseFilename { get; set; }
        public string OriginalFileExtension { get; set; }
        public Guid FileResourceGUID { get; set; }
        public int CreatePersonID { get; set; }
        public DateTime CreateDate { get; set; }
        public long ContentLength { get; set; }
    }
}
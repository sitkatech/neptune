//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectDocument]

namespace Neptune.Models.DataTransferObjects
{
    public partial class ProjectDocumentSimpleDto
    {
        public int ProjectDocumentID { get; set; }
        public int FileResourceID { get; set; }
        public int ProjectID { get; set; }
        public string DisplayName { get; set; }
        public DateOnly UploadDate { get; set; }
        public string DocumentDescription { get; set; }
    }
}
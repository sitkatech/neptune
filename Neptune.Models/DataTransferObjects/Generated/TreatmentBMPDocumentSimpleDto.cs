//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPDocument]

namespace Neptune.Models.DataTransferObjects
{
    public partial class TreatmentBMPDocumentSimpleDto
    {
        public int TreatmentBMPDocumentID { get; set; }
        public int FileResourceID { get; set; }
        public int TreatmentBMPID { get; set; }
        public string DisplayName { get; set; }
        public DateOnly UploadDate { get; set; }
        public string DocumentDescription { get; set; }
    }
}
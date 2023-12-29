//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPImage]

namespace Neptune.Models.DataTransferObjects
{
    public partial class TreatmentBMPImageSimpleDto
    {
        public int TreatmentBMPImageID { get; set; }
        public int FileResourceID { get; set; }
        public int TreatmentBMPID { get; set; }
        public string Caption { get; set; }
        public DateOnly UploadDate { get; set; }
    }
}
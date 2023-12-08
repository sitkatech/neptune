//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPDocument]
using System;


namespace Neptune.Models.DataTransferObjects
{
    public partial class TreatmentBMPDocumentDto
    {
        public int TreatmentBMPDocumentID { get; set; }
        public FileResourceDto FileResource { get; set; }
        public TreatmentBMPDto TreatmentBMP { get; set; }
        public string DisplayName { get; set; }
        public DateOnly UploadDate { get; set; }
        public string DocumentDescription { get; set; }
    }

    public partial class TreatmentBMPDocumentSimpleDto
    {
        public int TreatmentBMPDocumentID { get; set; }
        public System.Int32 FileResourceID { get; set; }
        public System.Int32 TreatmentBMPID { get; set; }
        public string DisplayName { get; set; }
        public DateOnly UploadDate { get; set; }
        public string DocumentDescription { get; set; }
    }

}
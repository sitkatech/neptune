//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPDocument]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class TreatmentBMPDocumentDto
    {
        public int TreatmentBMPDocumentID { get; set; }
        public FileResourceDto FileResource { get; set; }
        public TreatmentBMPDto TreatmentBMP { get; set; }
        public string DisplayName { get; set; }
        public DateTime UploadDate { get; set; }
        public string DocumentDescription { get; set; }
    }

    public partial class TreatmentBMPDocumentSimpleDto
    {
        public int TreatmentBMPDocumentID { get; set; }
        public int FileResourceID { get; set; }
        public int TreatmentBMPID { get; set; }
        public string DisplayName { get; set; }
        public DateTime UploadDate { get; set; }
        public string DocumentDescription { get; set; }
    }

}
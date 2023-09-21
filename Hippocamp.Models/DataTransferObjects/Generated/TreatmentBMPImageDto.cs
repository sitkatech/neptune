//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPImage]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class TreatmentBMPImageDto
    {
        public int TreatmentBMPImageID { get; set; }
        public FileResourceDto FileResource { get; set; }
        public TreatmentBMPDto TreatmentBMP { get; set; }
        public string Caption { get; set; }
        public DateTime UploadDate { get; set; }
    }

    public partial class TreatmentBMPImageSimpleDto
    {
        public int TreatmentBMPImageID { get; set; }
        public int FileResourceID { get; set; }
        public int TreatmentBMPID { get; set; }
        public string Caption { get; set; }
        public DateTime UploadDate { get; set; }
    }

}
//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPAssessmentPhoto]
using System;


namespace Neptune.Models.DataTransferObjects
{
    public partial class TreatmentBMPAssessmentPhotoDto
    {
        public int TreatmentBMPAssessmentPhotoID { get; set; }
        public FileResourceDto FileResource { get; set; }
        public TreatmentBMPAssessmentDto TreatmentBMPAssessment { get; set; }
        public string Caption { get; set; }
    }

    public partial class TreatmentBMPAssessmentPhotoSimpleDto
    {
        public int TreatmentBMPAssessmentPhotoID { get; set; }
        public System.Int32 FileResourceID { get; set; }
        public System.Int32 TreatmentBMPAssessmentID { get; set; }
        public string Caption { get; set; }
    }

}
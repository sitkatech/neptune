//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPAssessmentPhoto]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class TreatmentBMPAssessmentPhotoExtensionMethods
    {
        public static TreatmentBMPAssessmentPhotoSimpleDto AsSimpleDto(this TreatmentBMPAssessmentPhoto treatmentBMPAssessmentPhoto)
        {
            var dto = new TreatmentBMPAssessmentPhotoSimpleDto()
            {
                TreatmentBMPAssessmentPhotoID = treatmentBMPAssessmentPhoto.TreatmentBMPAssessmentPhotoID,
                FileResourceID = treatmentBMPAssessmentPhoto.FileResourceID,
                TreatmentBMPAssessmentID = treatmentBMPAssessmentPhoto.TreatmentBMPAssessmentID,
                Caption = treatmentBMPAssessmentPhoto.Caption
            };
            return dto;
        }
    }
}
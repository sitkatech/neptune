//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPAssessmentPhoto]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class TreatmentBMPAssessmentPhotoExtensionMethods
    {
        public static TreatmentBMPAssessmentPhotoDto AsDto(this TreatmentBMPAssessmentPhoto treatmentBMPAssessmentPhoto)
        {
            var treatmentBMPAssessmentPhotoDto = new TreatmentBMPAssessmentPhotoDto()
            {
                TreatmentBMPAssessmentPhotoID = treatmentBMPAssessmentPhoto.TreatmentBMPAssessmentPhotoID,
                FileResource = treatmentBMPAssessmentPhoto.FileResource.AsDto(),
                TreatmentBMPAssessment = treatmentBMPAssessmentPhoto.TreatmentBMPAssessment.AsDto(),
                Caption = treatmentBMPAssessmentPhoto.Caption
            };
            DoCustomMappings(treatmentBMPAssessmentPhoto, treatmentBMPAssessmentPhotoDto);
            return treatmentBMPAssessmentPhotoDto;
        }

        static partial void DoCustomMappings(TreatmentBMPAssessmentPhoto treatmentBMPAssessmentPhoto, TreatmentBMPAssessmentPhotoDto treatmentBMPAssessmentPhotoDto);

        public static TreatmentBMPAssessmentPhotoSimpleDto AsSimpleDto(this TreatmentBMPAssessmentPhoto treatmentBMPAssessmentPhoto)
        {
            var treatmentBMPAssessmentPhotoSimpleDto = new TreatmentBMPAssessmentPhotoSimpleDto()
            {
                TreatmentBMPAssessmentPhotoID = treatmentBMPAssessmentPhoto.TreatmentBMPAssessmentPhotoID,
                FileResourceID = treatmentBMPAssessmentPhoto.FileResourceID,
                TreatmentBMPAssessmentID = treatmentBMPAssessmentPhoto.TreatmentBMPAssessmentID,
                Caption = treatmentBMPAssessmentPhoto.Caption
            };
            DoCustomSimpleDtoMappings(treatmentBMPAssessmentPhoto, treatmentBMPAssessmentPhotoSimpleDto);
            return treatmentBMPAssessmentPhotoSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(TreatmentBMPAssessmentPhoto treatmentBMPAssessmentPhoto, TreatmentBMPAssessmentPhotoSimpleDto treatmentBMPAssessmentPhotoSimpleDto);
    }
}
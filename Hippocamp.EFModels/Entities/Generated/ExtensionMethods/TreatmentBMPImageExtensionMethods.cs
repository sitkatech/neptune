//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPImage]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class TreatmentBMPImageExtensionMethods
    {
        public static TreatmentBMPImageDto AsDto(this TreatmentBMPImage treatmentBMPImage)
        {
            var treatmentBMPImageDto = new TreatmentBMPImageDto()
            {
                TreatmentBMPImageID = treatmentBMPImage.TreatmentBMPImageID,
                FileResource = treatmentBMPImage.FileResource.AsDto(),
                TreatmentBMP = treatmentBMPImage.TreatmentBMP.AsDto(),
                Caption = treatmentBMPImage.Caption,
                UploadDate = treatmentBMPImage.UploadDate
            };
            DoCustomMappings(treatmentBMPImage, treatmentBMPImageDto);
            return treatmentBMPImageDto;
        }

        static partial void DoCustomMappings(TreatmentBMPImage treatmentBMPImage, TreatmentBMPImageDto treatmentBMPImageDto);

        public static TreatmentBMPImageSimpleDto AsSimpleDto(this TreatmentBMPImage treatmentBMPImage)
        {
            var treatmentBMPImageSimpleDto = new TreatmentBMPImageSimpleDto()
            {
                TreatmentBMPImageID = treatmentBMPImage.TreatmentBMPImageID,
                FileResourceID = treatmentBMPImage.FileResourceID,
                TreatmentBMPID = treatmentBMPImage.TreatmentBMPID,
                Caption = treatmentBMPImage.Caption,
                UploadDate = treatmentBMPImage.UploadDate
            };
            DoCustomSimpleDtoMappings(treatmentBMPImage, treatmentBMPImageSimpleDto);
            return treatmentBMPImageSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(TreatmentBMPImage treatmentBMPImage, TreatmentBMPImageSimpleDto treatmentBMPImageSimpleDto);
    }
}
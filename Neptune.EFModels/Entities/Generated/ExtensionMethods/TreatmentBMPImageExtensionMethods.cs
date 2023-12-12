//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPImage]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class TreatmentBMPImageExtensionMethods
    {

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
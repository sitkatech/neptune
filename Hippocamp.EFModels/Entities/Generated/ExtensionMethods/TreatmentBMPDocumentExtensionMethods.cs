//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPDocument]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class TreatmentBMPDocumentExtensionMethods
    {
        public static TreatmentBMPDocumentDto AsDto(this TreatmentBMPDocument treatmentBMPDocument)
        {
            var treatmentBMPDocumentDto = new TreatmentBMPDocumentDto()
            {
                TreatmentBMPDocumentID = treatmentBMPDocument.TreatmentBMPDocumentID,
                FileResource = treatmentBMPDocument.FileResource.AsDto(),
                TreatmentBMP = treatmentBMPDocument.TreatmentBMP.AsDto(),
                DisplayName = treatmentBMPDocument.DisplayName,
                UploadDate = treatmentBMPDocument.UploadDate,
                DocumentDescription = treatmentBMPDocument.DocumentDescription
            };
            DoCustomMappings(treatmentBMPDocument, treatmentBMPDocumentDto);
            return treatmentBMPDocumentDto;
        }

        static partial void DoCustomMappings(TreatmentBMPDocument treatmentBMPDocument, TreatmentBMPDocumentDto treatmentBMPDocumentDto);

        public static TreatmentBMPDocumentSimpleDto AsSimpleDto(this TreatmentBMPDocument treatmentBMPDocument)
        {
            var treatmentBMPDocumentSimpleDto = new TreatmentBMPDocumentSimpleDto()
            {
                TreatmentBMPDocumentID = treatmentBMPDocument.TreatmentBMPDocumentID,
                FileResourceID = treatmentBMPDocument.FileResourceID,
                TreatmentBMPID = treatmentBMPDocument.TreatmentBMPID,
                DisplayName = treatmentBMPDocument.DisplayName,
                UploadDate = treatmentBMPDocument.UploadDate,
                DocumentDescription = treatmentBMPDocument.DocumentDescription
            };
            DoCustomSimpleDtoMappings(treatmentBMPDocument, treatmentBMPDocumentSimpleDto);
            return treatmentBMPDocumentSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(TreatmentBMPDocument treatmentBMPDocument, TreatmentBMPDocumentSimpleDto treatmentBMPDocumentSimpleDto);
    }
}
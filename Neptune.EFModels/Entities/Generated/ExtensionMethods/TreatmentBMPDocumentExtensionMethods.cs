//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPDocument]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class TreatmentBMPDocumentExtensionMethods
    {

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
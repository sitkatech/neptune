using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static class TreatmentBMPDocumentExtensionMethods
    {
        public static TreatmentBMPDocumentDto AsDto(this TreatmentBMPDocument entity)
        {
            return new TreatmentBMPDocumentDto
            {
                TreatmentBMPDocumentID = entity.TreatmentBMPDocumentID,
                DisplayName = entity.DisplayName
            };
        }
    }
}

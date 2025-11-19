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
                FileResourceID = entity.FileResourceID,
                FileResourceGUID = entity.FileResource.FileResourceGUID.ToString(),
                OriginalBaseFilename = entity.FileResource.OriginalBaseFilename,
                OriginalFileExtension = entity.FileResource.OriginalFileExtension,
                DisplayName = entity.DisplayName,
                DocumentDescription = entity.DocumentDescription,
                CreateDate = entity.FileResource.CreateDate
            };
        }
    }
}

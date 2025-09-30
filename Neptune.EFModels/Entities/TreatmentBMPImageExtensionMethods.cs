using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static class TreatmentBMPImageExtensionMethods
    {
        public static TreatmentBMPImageDto AsDto(this TreatmentBMPImage entity)
        {
            return new TreatmentBMPImageDto
            {
                TreatmentBMPImageID = entity.TreatmentBMPImageID,
                Caption = entity.Caption
            };
        }
    }
}

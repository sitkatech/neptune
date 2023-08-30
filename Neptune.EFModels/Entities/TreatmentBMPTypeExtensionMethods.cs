using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static partial class TreatmentBMPTypeExtensionMethods
{
    public static TreatmentBMPTypeDisplayDto AsDisplayDto(this TreatmentBMPType treatmentBMPType)
    {
        var treatmentBMPTypeDisplayDto = new TreatmentBMPTypeDisplayDto()
        {
            TreatmentBMPTypeID = treatmentBMPType.TreatmentBMPTypeID,
            TreatmentBMPTypeName = treatmentBMPType.TreatmentBMPTypeName,
        };
        return treatmentBMPTypeDisplayDto;
    }

}
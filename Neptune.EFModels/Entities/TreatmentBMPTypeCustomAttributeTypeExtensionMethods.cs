using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static partial class TreatmentBMPTypeCustomAttributeTypeExtensionMethods
{
    public static TreatmentBMPTypeAttributeTypeDto AsTreatmentBMPTypeAttributeTypeDtoDto(this TreatmentBMPTypeCustomAttributeType treatmentBMPTypeCustomAttributeType)
    {
        var treatmentBMPTypeAttributeTypeDto = new TreatmentBMPTypeAttributeTypeDto()
        {
            TreatmentBMPTypeID = treatmentBMPTypeCustomAttributeType.TreatmentBMPTypeID,
            CustomAttributeTypeID = treatmentBMPTypeCustomAttributeType.CustomAttributeTypeID
        };
        return treatmentBMPTypeAttributeTypeDto;
    }
}
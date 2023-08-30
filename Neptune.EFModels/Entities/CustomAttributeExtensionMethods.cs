using Neptune.Models.DataTransferObjects;
using System.Reflection.Metadata;

namespace Neptune.EFModels.Entities;

public static partial class CustomAttributeExtensionMethods
{
    public static CustomAttributeUpsertDto AsUpsertDto(this CustomAttribute customAttribute)
    {
        var customAttributeUpsertDto = new CustomAttributeUpsertDto(
            customAttribute.TreatmentBMPTypeCustomAttributeTypeID, customAttribute.CustomAttributeTypeID,
            customAttribute.CustomAttributeValues.Select(x => x.AttributeValue).ToList());
        return customAttributeUpsertDto;
    }
}
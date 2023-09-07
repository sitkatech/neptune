using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static partial class SourceControlBMPAttributeExtensionMethods
{
    public static SourceControlBMPUpsertDto AsUpsertDto(
        this SourceControlBMPAttribute sourceControlBMPAttribute)
    {
        var sourceControlBMPUpsertDto = new SourceControlBMPUpsertDto()
        {
            SourceControlBMPID = null,
            SourceControlBMPAttributeCategoryID = sourceControlBMPAttribute.SourceControlBMPAttributeCategoryID,
            SourceControlBMPAttributeCategoryName = sourceControlBMPAttribute.SourceControlBMPAttributeCategory.SourceControlBMPAttributeCategoryName,
            SourceControlBMPAttributeID = sourceControlBMPAttribute.SourceControlBMPAttributeID,
            SourceControlBMPAttributeName = sourceControlBMPAttribute.SourceControlBMPAttributeName,
            IsPresent = null,
            SourceControlBMPNote = null
        };
        return sourceControlBMPUpsertDto;
    }
}
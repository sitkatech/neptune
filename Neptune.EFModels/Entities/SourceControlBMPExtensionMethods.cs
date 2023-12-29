using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static partial class SourceControlBMPExtensionMethods
{
    public static SourceControlBMPUpsertDto AsUpsertDto(
        this SourceControlBMP sourceControlBMP)
    {
        var sourceControlBMPUpsertDto = new SourceControlBMPUpsertDto()
        {
            SourceControlBMPID = sourceControlBMP.SourceControlBMPID,
            SourceControlBMPAttributeCategoryID = sourceControlBMP.SourceControlBMPAttribute
                .SourceControlBMPAttributeCategory.SourceControlBMPAttributeCategoryID,
            SourceControlBMPAttributeCategoryName = sourceControlBMP.SourceControlBMPAttribute
                .SourceControlBMPAttributeCategory.SourceControlBMPAttributeCategoryName,
            SourceControlBMPAttributeID = sourceControlBMP.SourceControlBMPAttributeID,
            SourceControlBMPAttributeName = sourceControlBMP.SourceControlBMPAttribute.SourceControlBMPAttributeName,
            IsPresent = sourceControlBMP.IsPresent,
            SourceControlBMPNote = sourceControlBMP.SourceControlBMPNote
        };
        return sourceControlBMPUpsertDto;
    }
}
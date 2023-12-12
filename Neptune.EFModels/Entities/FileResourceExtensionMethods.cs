using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static partial class FileResourceExtensionMethods
{
    public static FileResourceDto AsDto(this FileResource fileResource)
    {
        var fileResourceDto = new FileResourceDto()
        {
            FileResourceID = fileResource.FileResourceID,
            FileResourceMimeType = fileResource.FileResourceMimeType.AsSimpleDto(),
            OriginalBaseFilename = fileResource.OriginalBaseFilename,
            OriginalFileExtension = fileResource.OriginalFileExtension,
            FileResourceGUID = fileResource.FileResourceGUID,
            CreatePerson = fileResource.CreatePerson.AsDto(),
            CreateDate = fileResource.CreateDate,
            ContentLength = fileResource.ContentLength
        };
        return fileResourceDto;
    }
}
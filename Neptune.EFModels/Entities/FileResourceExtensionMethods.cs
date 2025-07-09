using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static class FileResourceExtensionMethods
{
    public static FileResourceSimpleDto AsSimpleDto(this FileResource fileResource)
    {
        var dto = new FileResourceSimpleDto()
        {
            FileResourceID = fileResource.FileResourceID,
            FileResourceMimeTypeID = fileResource.FileResourceMimeTypeID,
            OriginalBaseFilename = fileResource.OriginalBaseFilename,
            OriginalFileExtension = fileResource.OriginalFileExtension,
            FileResourceGUID = fileResource.FileResourceGUID,
            CreatePersonID = fileResource.CreatePersonID,
            CreateDate = fileResource.CreateDate,
            ContentLength = fileResource.ContentLength
        };
        return dto;
    }

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
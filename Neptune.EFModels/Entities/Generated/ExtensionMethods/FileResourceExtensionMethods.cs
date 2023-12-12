//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FileResource]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class FileResourceExtensionMethods
    {

        public static FileResourceSimpleDto AsSimpleDto(this FileResource fileResource)
        {
            var fileResourceSimpleDto = new FileResourceSimpleDto()
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
            DoCustomSimpleDtoMappings(fileResource, fileResourceSimpleDto);
            return fileResourceSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(FileResource fileResource, FileResourceSimpleDto fileResourceSimpleDto);
    }
}
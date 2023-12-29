//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FileResourceMimeType]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class FileResourceMimeTypeExtensionMethods
    {
        public static FileResourceMimeTypeSimpleDto AsSimpleDto(this FileResourceMimeType fileResourceMimeType)
        {
            var dto = new FileResourceMimeTypeSimpleDto()
            {
                FileResourceMimeTypeID = fileResourceMimeType.FileResourceMimeTypeID,
                FileResourceMimeTypeName = fileResourceMimeType.FileResourceMimeTypeName,
                FileResourceMimeTypeDisplayName = fileResourceMimeType.FileResourceMimeTypeDisplayName,
                FileResourceMimeTypeContentTypeName = fileResourceMimeType.FileResourceMimeTypeContentTypeName,
                FileResourceMimeTypeIconSmallFilename = fileResourceMimeType.FileResourceMimeTypeIconSmallFilename,
                FileResourceMimeTypeIconNormalFilename = fileResourceMimeType.FileResourceMimeTypeIconNormalFilename
            };
            return dto;
        }
    }
}
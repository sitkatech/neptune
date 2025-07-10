//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[NeptunePageType]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class NeptunePageTypeExtensionMethods
    {
        public static NeptunePageTypeSimpleDto AsSimpleDto(this NeptunePageType neptunePageType)
        {
            var dto = new NeptunePageTypeSimpleDto()
            {
                NeptunePageTypeID = neptunePageType.NeptunePageTypeID,
                NeptunePageTypeName = neptunePageType.NeptunePageTypeName,
                NeptunePageTypeDisplayName = neptunePageType.NeptunePageTypeDisplayName
            };
            return dto;
        }
    }
}
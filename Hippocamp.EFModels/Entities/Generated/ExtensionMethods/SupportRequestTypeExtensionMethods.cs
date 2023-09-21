//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[SupportRequestType]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class SupportRequestTypeExtensionMethods
    {
        public static SupportRequestTypeDto AsDto(this SupportRequestType supportRequestType)
        {
            var supportRequestTypeDto = new SupportRequestTypeDto()
            {
                SupportRequestTypeID = supportRequestType.SupportRequestTypeID,
                SupportRequestTypeName = supportRequestType.SupportRequestTypeName,
                SupportRequestTypeDisplayName = supportRequestType.SupportRequestTypeDisplayName,
                SupportRequestTypeSortOrder = supportRequestType.SupportRequestTypeSortOrder
            };
            DoCustomMappings(supportRequestType, supportRequestTypeDto);
            return supportRequestTypeDto;
        }

        static partial void DoCustomMappings(SupportRequestType supportRequestType, SupportRequestTypeDto supportRequestTypeDto);

        public static SupportRequestTypeSimpleDto AsSimpleDto(this SupportRequestType supportRequestType)
        {
            var supportRequestTypeSimpleDto = new SupportRequestTypeSimpleDto()
            {
                SupportRequestTypeID = supportRequestType.SupportRequestTypeID,
                SupportRequestTypeName = supportRequestType.SupportRequestTypeName,
                SupportRequestTypeDisplayName = supportRequestType.SupportRequestTypeDisplayName,
                SupportRequestTypeSortOrder = supportRequestType.SupportRequestTypeSortOrder
            };
            DoCustomSimpleDtoMappings(supportRequestType, supportRequestTypeSimpleDto);
            return supportRequestTypeSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(SupportRequestType supportRequestType, SupportRequestTypeSimpleDto supportRequestTypeSimpleDto);
    }
}
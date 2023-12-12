//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[SupportRequestType]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class SupportRequestTypeExtensionMethods
    {

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
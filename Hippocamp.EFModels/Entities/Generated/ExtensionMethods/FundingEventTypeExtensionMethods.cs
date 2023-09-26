//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FundingEventType]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class FundingEventTypeExtensionMethods
    {
        public static FundingEventTypeDto AsDto(this FundingEventType fundingEventType)
        {
            var fundingEventTypeDto = new FundingEventTypeDto()
            {
                FundingEventTypeID = fundingEventType.FundingEventTypeID,
                FundingEventTypeName = fundingEventType.FundingEventTypeName,
                FundingEventTypeDisplayName = fundingEventType.FundingEventTypeDisplayName,
                SortOrder = fundingEventType.SortOrder
            };
            DoCustomMappings(fundingEventType, fundingEventTypeDto);
            return fundingEventTypeDto;
        }

        static partial void DoCustomMappings(FundingEventType fundingEventType, FundingEventTypeDto fundingEventTypeDto);

        public static FundingEventTypeSimpleDto AsSimpleDto(this FundingEventType fundingEventType)
        {
            var fundingEventTypeSimpleDto = new FundingEventTypeSimpleDto()
            {
                FundingEventTypeID = fundingEventType.FundingEventTypeID,
                FundingEventTypeName = fundingEventType.FundingEventTypeName,
                FundingEventTypeDisplayName = fundingEventType.FundingEventTypeDisplayName,
                SortOrder = fundingEventType.SortOrder
            };
            DoCustomSimpleDtoMappings(fundingEventType, fundingEventTypeSimpleDto);
            return fundingEventTypeSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(FundingEventType fundingEventType, FundingEventTypeSimpleDto fundingEventTypeSimpleDto);
    }
}
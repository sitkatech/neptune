//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FundingEventType]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class FundingEventTypeExtensionMethods
    {

        public static FundingEventTypeSimpleDto AsSimpleDto(this FundingEventType fundingEventType)
        {
            var fundingEventTypeSimpleDto = new FundingEventTypeSimpleDto()
            {
                FundingEventTypeID = fundingEventType.FundingEventTypeID,
                FundingEventTypeName = fundingEventType.FundingEventTypeName,
                FundingEventTypeDisplayName = fundingEventType.FundingEventTypeDisplayName
            };
            DoCustomSimpleDtoMappings(fundingEventType, fundingEventTypeSimpleDto);
            return fundingEventTypeSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(FundingEventType fundingEventType, FundingEventTypeSimpleDto fundingEventTypeSimpleDto);
    }
}
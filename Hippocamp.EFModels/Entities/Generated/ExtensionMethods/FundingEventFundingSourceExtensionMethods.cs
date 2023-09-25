//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FundingEventFundingSource]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class FundingEventFundingSourceExtensionMethods
    {
        public static FundingEventFundingSourceDto AsDto(this FundingEventFundingSource fundingEventFundingSource)
        {
            var fundingEventFundingSourceDto = new FundingEventFundingSourceDto()
            {
                FundingEventFundingSourceID = fundingEventFundingSource.FundingEventFundingSourceID,
                FundingSource = fundingEventFundingSource.FundingSource.AsDto(),
                FundingEvent = fundingEventFundingSource.FundingEvent.AsDto(),
                Amount = fundingEventFundingSource.Amount
            };
            DoCustomMappings(fundingEventFundingSource, fundingEventFundingSourceDto);
            return fundingEventFundingSourceDto;
        }

        static partial void DoCustomMappings(FundingEventFundingSource fundingEventFundingSource, FundingEventFundingSourceDto fundingEventFundingSourceDto);

        public static FundingEventFundingSourceSimpleDto AsSimpleDto(this FundingEventFundingSource fundingEventFundingSource)
        {
            var fundingEventFundingSourceSimpleDto = new FundingEventFundingSourceSimpleDto()
            {
                FundingEventFundingSourceID = fundingEventFundingSource.FundingEventFundingSourceID,
                FundingSourceID = fundingEventFundingSource.FundingSourceID,
                FundingEventID = fundingEventFundingSource.FundingEventID,
                Amount = fundingEventFundingSource.Amount
            };
            DoCustomSimpleDtoMappings(fundingEventFundingSource, fundingEventFundingSourceSimpleDto);
            return fundingEventFundingSourceSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(FundingEventFundingSource fundingEventFundingSource, FundingEventFundingSourceSimpleDto fundingEventFundingSourceSimpleDto);
    }
}
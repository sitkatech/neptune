//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FundingEventFundingSource]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class FundingEventFundingSourceExtensionMethods
    {

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
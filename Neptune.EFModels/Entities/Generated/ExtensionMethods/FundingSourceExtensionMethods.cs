//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FundingSource]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class FundingSourceExtensionMethods
    {
        public static FundingSourceDto AsDto(this FundingSource fundingSource)
        {
            var fundingSourceDto = new FundingSourceDto()
            {
                FundingSourceID = fundingSource.FundingSourceID,
                Organization = fundingSource.Organization.AsDto(),
                FundingSourceName = fundingSource.FundingSourceName,
                IsActive = fundingSource.IsActive,
                FundingSourceDescription = fundingSource.FundingSourceDescription
            };
            DoCustomMappings(fundingSource, fundingSourceDto);
            return fundingSourceDto;
        }

        static partial void DoCustomMappings(FundingSource fundingSource, FundingSourceDto fundingSourceDto);

        public static FundingSourceSimpleDto AsSimpleDto(this FundingSource fundingSource)
        {
            var fundingSourceSimpleDto = new FundingSourceSimpleDto()
            {
                FundingSourceID = fundingSource.FundingSourceID,
                OrganizationID = fundingSource.OrganizationID,
                FundingSourceName = fundingSource.FundingSourceName,
                IsActive = fundingSource.IsActive,
                FundingSourceDescription = fundingSource.FundingSourceDescription
            };
            DoCustomSimpleDtoMappings(fundingSource, fundingSourceSimpleDto);
            return fundingSourceSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(FundingSource fundingSource, FundingSourceSimpleDto fundingSourceSimpleDto);
    }
}
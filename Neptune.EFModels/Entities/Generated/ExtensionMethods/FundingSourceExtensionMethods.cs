//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FundingSource]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class FundingSourceExtensionMethods
    {
        public static FundingSourceSimpleDto AsSimpleDto(this FundingSource fundingSource)
        {
            var dto = new FundingSourceSimpleDto()
            {
                FundingSourceID = fundingSource.FundingSourceID,
                OrganizationID = fundingSource.OrganizationID,
                FundingSourceName = fundingSource.FundingSourceName,
                IsActive = fundingSource.IsActive,
                FundingSourceDescription = fundingSource.FundingSourceDescription
            };
            return dto;
        }
    }
}
using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static class FundingEventFundingSourceExtensionMethods
    {
        public static FundingEventFundingSourceSimpleDto AsSimpleDto(this FundingEventFundingSource entity)
        {
            var fundingSourceName = entity.FundingSource?.GetDisplayName() ?? string.Empty;
            return new FundingEventFundingSourceSimpleDto
            {
                FundingSourceID = entity.FundingSourceID,
                Amount = entity.Amount,
                FundingSourceName = fundingSourceName
            };
        }

    }
}

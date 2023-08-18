//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.FundingEventFundingSource


namespace Neptune.EFModels.Entities
{
    public class FundingEventFundingSourcePrimaryKey : EntityPrimaryKey<FundingEventFundingSource>
    {
        public FundingEventFundingSourcePrimaryKey() : base(){}
        public FundingEventFundingSourcePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public FundingEventFundingSourcePrimaryKey(FundingEventFundingSource fundingEventFundingSource) : base(fundingEventFundingSource){}

        public static implicit operator FundingEventFundingSourcePrimaryKey(int primaryKeyValue)
        {
            return new FundingEventFundingSourcePrimaryKey(primaryKeyValue);
        }

        public static implicit operator FundingEventFundingSourcePrimaryKey(FundingEventFundingSource fundingEventFundingSource)
        {
            return new FundingEventFundingSourcePrimaryKey(fundingEventFundingSource);
        }
    }
}
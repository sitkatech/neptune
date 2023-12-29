//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.FundingEventType


namespace Neptune.EFModels.Entities
{
    public class FundingEventTypePrimaryKey : EntityPrimaryKey<FundingEventType>
    {
        public FundingEventTypePrimaryKey() : base(){}
        public FundingEventTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public FundingEventTypePrimaryKey(FundingEventType fundingEventType) : base(fundingEventType){}

        public static implicit operator FundingEventTypePrimaryKey(int primaryKeyValue)
        {
            return new FundingEventTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator FundingEventTypePrimaryKey(FundingEventType fundingEventType)
        {
            return new FundingEventTypePrimaryKey(fundingEventType);
        }
    }
}
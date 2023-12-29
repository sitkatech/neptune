//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.FundingEvent


namespace Neptune.EFModels.Entities
{
    public class FundingEventPrimaryKey : EntityPrimaryKey<FundingEvent>
    {
        public FundingEventPrimaryKey() : base(){}
        public FundingEventPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public FundingEventPrimaryKey(FundingEvent fundingEvent) : base(fundingEvent){}

        public static implicit operator FundingEventPrimaryKey(int primaryKeyValue)
        {
            return new FundingEventPrimaryKey(primaryKeyValue);
        }

        public static implicit operator FundingEventPrimaryKey(FundingEvent fundingEvent)
        {
            return new FundingEventPrimaryKey(fundingEvent);
        }
    }
}
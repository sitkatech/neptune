//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.FundingEvent
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class FundingEventPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<FundingEvent>
    {
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
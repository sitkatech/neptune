//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.FundingEventFundingSource
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class FundingEventFundingSourcePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<FundingEventFundingSource>
    {
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
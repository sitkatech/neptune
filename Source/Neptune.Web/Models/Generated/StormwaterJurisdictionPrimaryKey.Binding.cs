//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.StormwaterJurisdiction
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class StormwaterJurisdictionPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<StormwaterJurisdiction>
    {
        public StormwaterJurisdictionPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public StormwaterJurisdictionPrimaryKey(StormwaterJurisdiction stormwaterJurisdiction) : base(stormwaterJurisdiction){}

        public static implicit operator StormwaterJurisdictionPrimaryKey(int primaryKeyValue)
        {
            return new StormwaterJurisdictionPrimaryKey(primaryKeyValue);
        }

        public static implicit operator StormwaterJurisdictionPrimaryKey(StormwaterJurisdiction stormwaterJurisdiction)
        {
            return new StormwaterJurisdictionPrimaryKey(stormwaterJurisdiction);
        }
    }
}
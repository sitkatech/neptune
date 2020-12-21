//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.StormwaterJurisdictionPublicBMPVisibilityType
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class StormwaterJurisdictionPublicBMPVisibilityTypePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<StormwaterJurisdictionPublicBMPVisibilityType>
    {
        public StormwaterJurisdictionPublicBMPVisibilityTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public StormwaterJurisdictionPublicBMPVisibilityTypePrimaryKey(StormwaterJurisdictionPublicBMPVisibilityType stormwaterJurisdictionPublicBMPVisibilityType) : base(stormwaterJurisdictionPublicBMPVisibilityType){}

        public static implicit operator StormwaterJurisdictionPublicBMPVisibilityTypePrimaryKey(int primaryKeyValue)
        {
            return new StormwaterJurisdictionPublicBMPVisibilityTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator StormwaterJurisdictionPublicBMPVisibilityTypePrimaryKey(StormwaterJurisdictionPublicBMPVisibilityType stormwaterJurisdictionPublicBMPVisibilityType)
        {
            return new StormwaterJurisdictionPublicBMPVisibilityTypePrimaryKey(stormwaterJurisdictionPublicBMPVisibilityType);
        }
    }
}
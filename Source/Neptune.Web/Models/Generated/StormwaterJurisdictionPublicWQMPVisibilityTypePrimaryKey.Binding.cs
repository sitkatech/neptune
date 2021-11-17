//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.StormwaterJurisdictionPublicWQMPVisibilityType
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class StormwaterJurisdictionPublicWQMPVisibilityTypePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<StormwaterJurisdictionPublicWQMPVisibilityType>
    {
        public StormwaterJurisdictionPublicWQMPVisibilityTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public StormwaterJurisdictionPublicWQMPVisibilityTypePrimaryKey(StormwaterJurisdictionPublicWQMPVisibilityType stormwaterJurisdictionPublicWQMPVisibilityType) : base(stormwaterJurisdictionPublicWQMPVisibilityType){}

        public static implicit operator StormwaterJurisdictionPublicWQMPVisibilityTypePrimaryKey(int primaryKeyValue)
        {
            return new StormwaterJurisdictionPublicWQMPVisibilityTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator StormwaterJurisdictionPublicWQMPVisibilityTypePrimaryKey(StormwaterJurisdictionPublicWQMPVisibilityType stormwaterJurisdictionPublicWQMPVisibilityType)
        {
            return new StormwaterJurisdictionPublicWQMPVisibilityTypePrimaryKey(stormwaterJurisdictionPublicWQMPVisibilityType);
        }
    }
}
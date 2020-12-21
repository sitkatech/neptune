//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.StormwaterJurisdictionBMPPublicVisibilityType
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class StormwaterJurisdictionBMPPublicVisibilityTypePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<StormwaterJurisdictionBMPPublicVisibilityType>
    {
        public StormwaterJurisdictionBMPPublicVisibilityTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public StormwaterJurisdictionBMPPublicVisibilityTypePrimaryKey(StormwaterJurisdictionBMPPublicVisibilityType stormwaterJurisdictionBMPPublicVisibilityType) : base(stormwaterJurisdictionBMPPublicVisibilityType){}

        public static implicit operator StormwaterJurisdictionBMPPublicVisibilityTypePrimaryKey(int primaryKeyValue)
        {
            return new StormwaterJurisdictionBMPPublicVisibilityTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator StormwaterJurisdictionBMPPublicVisibilityTypePrimaryKey(StormwaterJurisdictionBMPPublicVisibilityType stormwaterJurisdictionBMPPublicVisibilityType)
        {
            return new StormwaterJurisdictionBMPPublicVisibilityTypePrimaryKey(stormwaterJurisdictionBMPPublicVisibilityType);
        }
    }
}
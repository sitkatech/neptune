//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.StormwaterJurisdictionPublicBMPVisibilityType


namespace Neptune.EFModels.Entities
{
    public class StormwaterJurisdictionPublicBMPVisibilityTypePrimaryKey : EntityPrimaryKey<StormwaterJurisdictionPublicBMPVisibilityType>
    {
        public StormwaterJurisdictionPublicBMPVisibilityTypePrimaryKey() : base(){}
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
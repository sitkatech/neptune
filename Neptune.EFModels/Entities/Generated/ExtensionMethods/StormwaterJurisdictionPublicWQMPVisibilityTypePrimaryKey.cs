//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.StormwaterJurisdictionPublicWQMPVisibilityType


namespace Neptune.EFModels.Entities
{
    public class StormwaterJurisdictionPublicWQMPVisibilityTypePrimaryKey : EntityPrimaryKey<StormwaterJurisdictionPublicWQMPVisibilityType>
    {
        public StormwaterJurisdictionPublicWQMPVisibilityTypePrimaryKey() : base(){}
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
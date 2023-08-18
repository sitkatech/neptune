//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.StormwaterJurisdictionPerson


namespace Neptune.EFModels.Entities
{
    public class StormwaterJurisdictionPersonPrimaryKey : EntityPrimaryKey<StormwaterJurisdictionPerson>
    {
        public StormwaterJurisdictionPersonPrimaryKey() : base(){}
        public StormwaterJurisdictionPersonPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public StormwaterJurisdictionPersonPrimaryKey(StormwaterJurisdictionPerson stormwaterJurisdictionPerson) : base(stormwaterJurisdictionPerson){}

        public static implicit operator StormwaterJurisdictionPersonPrimaryKey(int primaryKeyValue)
        {
            return new StormwaterJurisdictionPersonPrimaryKey(primaryKeyValue);
        }

        public static implicit operator StormwaterJurisdictionPersonPrimaryKey(StormwaterJurisdictionPerson stormwaterJurisdictionPerson)
        {
            return new StormwaterJurisdictionPersonPrimaryKey(stormwaterJurisdictionPerson);
        }
    }
}
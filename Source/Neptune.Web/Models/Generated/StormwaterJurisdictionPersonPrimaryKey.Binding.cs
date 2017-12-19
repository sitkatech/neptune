//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.StormwaterJurisdictionPerson
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class StormwaterJurisdictionPersonPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<StormwaterJurisdictionPerson>
    {
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
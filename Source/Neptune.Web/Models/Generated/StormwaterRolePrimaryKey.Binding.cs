//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.StormwaterRole
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class StormwaterRolePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<StormwaterRole>
    {
        public StormwaterRolePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public StormwaterRolePrimaryKey(StormwaterRole stormwaterRole) : base(stormwaterRole){}

        public static implicit operator StormwaterRolePrimaryKey(int primaryKeyValue)
        {
            return new StormwaterRolePrimaryKey(primaryKeyValue);
        }

        public static implicit operator StormwaterRolePrimaryKey(StormwaterRole stormwaterRole)
        {
            return new StormwaterRolePrimaryKey(stormwaterRole);
        }
    }
}
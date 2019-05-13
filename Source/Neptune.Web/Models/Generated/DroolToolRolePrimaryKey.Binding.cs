//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.DroolToolRole
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class DroolToolRolePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<DroolToolRole>
    {
        public DroolToolRolePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public DroolToolRolePrimaryKey(DroolToolRole droolToolRole) : base(droolToolRole){}

        public static implicit operator DroolToolRolePrimaryKey(int primaryKeyValue)
        {
            return new DroolToolRolePrimaryKey(primaryKeyValue);
        }

        public static implicit operator DroolToolRolePrimaryKey(DroolToolRole droolToolRole)
        {
            return new DroolToolRolePrimaryKey(droolToolRole);
        }
    }
}
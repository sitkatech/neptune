//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.SystemAttribute
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class SystemAttributePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<SystemAttribute>
    {
        public SystemAttributePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public SystemAttributePrimaryKey(SystemAttribute systemAttribute) : base(systemAttribute){}

        public static implicit operator SystemAttributePrimaryKey(int primaryKeyValue)
        {
            return new SystemAttributePrimaryKey(primaryKeyValue);
        }

        public static implicit operator SystemAttributePrimaryKey(SystemAttribute systemAttribute)
        {
            return new SystemAttributePrimaryKey(systemAttribute);
        }
    }
}
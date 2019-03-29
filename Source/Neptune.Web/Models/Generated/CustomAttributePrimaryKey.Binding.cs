//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.CustomAttribute
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class CustomAttributePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<CustomAttribute>
    {
        public CustomAttributePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public CustomAttributePrimaryKey(CustomAttribute customAttribute) : base(customAttribute){}

        public static implicit operator CustomAttributePrimaryKey(int primaryKeyValue)
        {
            return new CustomAttributePrimaryKey(primaryKeyValue);
        }

        public static implicit operator CustomAttributePrimaryKey(CustomAttribute customAttribute)
        {
            return new CustomAttributePrimaryKey(customAttribute);
        }
    }
}
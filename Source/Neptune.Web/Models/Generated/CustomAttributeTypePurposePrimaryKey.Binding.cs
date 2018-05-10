//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.CustomAttributeTypePurpose
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class CustomAttributeTypePurposePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<CustomAttributeTypePurpose>
    {
        public CustomAttributeTypePurposePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public CustomAttributeTypePurposePrimaryKey(CustomAttributeTypePurpose customAttributeTypePurpose) : base(customAttributeTypePurpose){}

        public static implicit operator CustomAttributeTypePurposePrimaryKey(int primaryKeyValue)
        {
            return new CustomAttributeTypePurposePrimaryKey(primaryKeyValue);
        }

        public static implicit operator CustomAttributeTypePurposePrimaryKey(CustomAttributeTypePurpose customAttributeTypePurpose)
        {
            return new CustomAttributeTypePurposePrimaryKey(customAttributeTypePurpose);
        }
    }
}
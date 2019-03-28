//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.CustomAttributeDataType
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class CustomAttributeDataTypePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<CustomAttributeDataType>
    {
        public CustomAttributeDataTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public CustomAttributeDataTypePrimaryKey(CustomAttributeDataType customAttributeDataType) : base(customAttributeDataType){}

        public static implicit operator CustomAttributeDataTypePrimaryKey(int primaryKeyValue)
        {
            return new CustomAttributeDataTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator CustomAttributeDataTypePrimaryKey(CustomAttributeDataType customAttributeDataType)
        {
            return new CustomAttributeDataTypePrimaryKey(customAttributeDataType);
        }
    }
}
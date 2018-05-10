//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.CustomAttributeValue
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class CustomAttributeValuePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<CustomAttributeValue>
    {
        public CustomAttributeValuePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public CustomAttributeValuePrimaryKey(CustomAttributeValue customAttributeValue) : base(customAttributeValue){}

        public static implicit operator CustomAttributeValuePrimaryKey(int primaryKeyValue)
        {
            return new CustomAttributeValuePrimaryKey(primaryKeyValue);
        }

        public static implicit operator CustomAttributeValuePrimaryKey(CustomAttributeValue customAttributeValue)
        {
            return new CustomAttributeValuePrimaryKey(customAttributeValue);
        }
    }
}
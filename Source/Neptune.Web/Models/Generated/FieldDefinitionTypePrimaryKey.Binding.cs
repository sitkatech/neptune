//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.FieldDefinitionType
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class FieldDefinitionTypePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<FieldDefinitionType>
    {
        public FieldDefinitionTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public FieldDefinitionTypePrimaryKey(FieldDefinitionType fieldDefinitionType) : base(fieldDefinitionType){}

        public static implicit operator FieldDefinitionTypePrimaryKey(int primaryKeyValue)
        {
            return new FieldDefinitionTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator FieldDefinitionTypePrimaryKey(FieldDefinitionType fieldDefinitionType)
        {
            return new FieldDefinitionTypePrimaryKey(fieldDefinitionType);
        }
    }
}
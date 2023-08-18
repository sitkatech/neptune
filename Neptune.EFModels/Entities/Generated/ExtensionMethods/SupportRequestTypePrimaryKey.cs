//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.SupportRequestType


namespace Neptune.EFModels.Entities
{
    public class SupportRequestTypePrimaryKey : EntityPrimaryKey<SupportRequestType>
    {
        public SupportRequestTypePrimaryKey() : base(){}
        public SupportRequestTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public SupportRequestTypePrimaryKey(SupportRequestType supportRequestType) : base(supportRequestType){}

        public static implicit operator SupportRequestTypePrimaryKey(int primaryKeyValue)
        {
            return new SupportRequestTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator SupportRequestTypePrimaryKey(SupportRequestType supportRequestType)
        {
            return new SupportRequestTypePrimaryKey(supportRequestType);
        }
    }
}
//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.PermitType


namespace Neptune.EFModels.Entities
{
    public class PermitTypePrimaryKey : EntityPrimaryKey<PermitType>
    {
        public PermitTypePrimaryKey() : base(){}
        public PermitTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public PermitTypePrimaryKey(PermitType permitType) : base(permitType){}

        public static implicit operator PermitTypePrimaryKey(int primaryKeyValue)
        {
            return new PermitTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator PermitTypePrimaryKey(PermitType permitType)
        {
            return new PermitTypePrimaryKey(permitType);
        }
    }
}
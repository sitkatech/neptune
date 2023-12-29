//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.FieldVisitType


namespace Neptune.EFModels.Entities
{
    public class FieldVisitTypePrimaryKey : EntityPrimaryKey<FieldVisitType>
    {
        public FieldVisitTypePrimaryKey() : base(){}
        public FieldVisitTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public FieldVisitTypePrimaryKey(FieldVisitType fieldVisitType) : base(fieldVisitType){}

        public static implicit operator FieldVisitTypePrimaryKey(int primaryKeyValue)
        {
            return new FieldVisitTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator FieldVisitTypePrimaryKey(FieldVisitType fieldVisitType)
        {
            return new FieldVisitTypePrimaryKey(fieldVisitType);
        }
    }
}
//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.FieldVisit


namespace Neptune.EFModels.Entities
{
    public class FieldVisitPrimaryKey : EntityPrimaryKey<FieldVisit>
    {
        public FieldVisitPrimaryKey() : base(){}
        public FieldVisitPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public FieldVisitPrimaryKey(FieldVisit fieldVisit) : base(fieldVisit){}

        public static implicit operator FieldVisitPrimaryKey(int primaryKeyValue)
        {
            return new FieldVisitPrimaryKey(primaryKeyValue);
        }

        public static implicit operator FieldVisitPrimaryKey(FieldVisit fieldVisit)
        {
            return new FieldVisitPrimaryKey(fieldVisit);
        }
    }
}
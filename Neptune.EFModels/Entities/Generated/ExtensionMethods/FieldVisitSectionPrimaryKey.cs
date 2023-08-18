//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.FieldVisitSection


namespace Neptune.EFModels.Entities
{
    public class FieldVisitSectionPrimaryKey : EntityPrimaryKey<FieldVisitSection>
    {
        public FieldVisitSectionPrimaryKey() : base(){}
        public FieldVisitSectionPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public FieldVisitSectionPrimaryKey(FieldVisitSection fieldVisitSection) : base(fieldVisitSection){}

        public static implicit operator FieldVisitSectionPrimaryKey(int primaryKeyValue)
        {
            return new FieldVisitSectionPrimaryKey(primaryKeyValue);
        }

        public static implicit operator FieldVisitSectionPrimaryKey(FieldVisitSection fieldVisitSection)
        {
            return new FieldVisitSectionPrimaryKey(fieldVisitSection);
        }
    }
}
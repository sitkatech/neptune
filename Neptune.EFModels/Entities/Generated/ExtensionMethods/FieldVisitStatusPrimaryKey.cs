//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.FieldVisitStatus


namespace Neptune.EFModels.Entities
{
    public class FieldVisitStatusPrimaryKey : EntityPrimaryKey<FieldVisitStatus>
    {
        public FieldVisitStatusPrimaryKey() : base(){}
        public FieldVisitStatusPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public FieldVisitStatusPrimaryKey(FieldVisitStatus fieldVisitStatus) : base(fieldVisitStatus){}

        public static implicit operator FieldVisitStatusPrimaryKey(int primaryKeyValue)
        {
            return new FieldVisitStatusPrimaryKey(primaryKeyValue);
        }

        public static implicit operator FieldVisitStatusPrimaryKey(FieldVisitStatus fieldVisitStatus)
        {
            return new FieldVisitStatusPrimaryKey(fieldVisitStatus);
        }
    }
}
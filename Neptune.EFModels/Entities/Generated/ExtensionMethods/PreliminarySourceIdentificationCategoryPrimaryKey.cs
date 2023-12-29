//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.PreliminarySourceIdentificationCategory


namespace Neptune.EFModels.Entities
{
    public class PreliminarySourceIdentificationCategoryPrimaryKey : EntityPrimaryKey<PreliminarySourceIdentificationCategory>
    {
        public PreliminarySourceIdentificationCategoryPrimaryKey() : base(){}
        public PreliminarySourceIdentificationCategoryPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public PreliminarySourceIdentificationCategoryPrimaryKey(PreliminarySourceIdentificationCategory preliminarySourceIdentificationCategory) : base(preliminarySourceIdentificationCategory){}

        public static implicit operator PreliminarySourceIdentificationCategoryPrimaryKey(int primaryKeyValue)
        {
            return new PreliminarySourceIdentificationCategoryPrimaryKey(primaryKeyValue);
        }

        public static implicit operator PreliminarySourceIdentificationCategoryPrimaryKey(PreliminarySourceIdentificationCategory preliminarySourceIdentificationCategory)
        {
            return new PreliminarySourceIdentificationCategoryPrimaryKey(preliminarySourceIdentificationCategory);
        }
    }
}
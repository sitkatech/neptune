//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.PreliminarySourceIdentificationCategory
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class PreliminarySourceIdentificationCategoryPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<PreliminarySourceIdentificationCategory>
    {
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
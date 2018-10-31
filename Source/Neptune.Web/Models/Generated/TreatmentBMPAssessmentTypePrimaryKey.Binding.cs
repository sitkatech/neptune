//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TreatmentBMPAssessmentType
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class TreatmentBMPAssessmentTypePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<TreatmentBMPAssessmentType>
    {
        public TreatmentBMPAssessmentTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public TreatmentBMPAssessmentTypePrimaryKey(TreatmentBMPAssessmentType treatmentBMPAssessmentType) : base(treatmentBMPAssessmentType){}

        public static implicit operator TreatmentBMPAssessmentTypePrimaryKey(int primaryKeyValue)
        {
            return new TreatmentBMPAssessmentTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator TreatmentBMPAssessmentTypePrimaryKey(TreatmentBMPAssessmentType treatmentBMPAssessmentType)
        {
            return new TreatmentBMPAssessmentTypePrimaryKey(treatmentBMPAssessmentType);
        }
    }
}
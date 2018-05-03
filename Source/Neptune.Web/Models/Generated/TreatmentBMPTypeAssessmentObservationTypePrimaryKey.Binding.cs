//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TreatmentBMPTypeAssessmentObservationType
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class TreatmentBMPTypeAssessmentObservationTypePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<TreatmentBMPTypeAssessmentObservationType>
    {
        public TreatmentBMPTypeAssessmentObservationTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public TreatmentBMPTypeAssessmentObservationTypePrimaryKey(TreatmentBMPTypeAssessmentObservationType treatmentBMPTypeAssessmentObservationType) : base(treatmentBMPTypeAssessmentObservationType){}

        public static implicit operator TreatmentBMPTypeAssessmentObservationTypePrimaryKey(int primaryKeyValue)
        {
            return new TreatmentBMPTypeAssessmentObservationTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator TreatmentBMPTypeAssessmentObservationTypePrimaryKey(TreatmentBMPTypeAssessmentObservationType treatmentBMPTypeAssessmentObservationType)
        {
            return new TreatmentBMPTypeAssessmentObservationTypePrimaryKey(treatmentBMPTypeAssessmentObservationType);
        }
    }
}
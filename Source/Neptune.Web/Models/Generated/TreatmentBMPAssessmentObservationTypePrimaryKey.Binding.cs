//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TreatmentBMPAssessmentObservationType
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class TreatmentBMPAssessmentObservationTypePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<TreatmentBMPAssessmentObservationType>
    {
        public TreatmentBMPAssessmentObservationTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public TreatmentBMPAssessmentObservationTypePrimaryKey(TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType) : base(treatmentBMPAssessmentObservationType){}

        public static implicit operator TreatmentBMPAssessmentObservationTypePrimaryKey(int primaryKeyValue)
        {
            return new TreatmentBMPAssessmentObservationTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator TreatmentBMPAssessmentObservationTypePrimaryKey(TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType)
        {
            return new TreatmentBMPAssessmentObservationTypePrimaryKey(treatmentBMPAssessmentObservationType);
        }
    }
}
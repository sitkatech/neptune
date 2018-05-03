//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TreatmentBMPAssessmentObservationType
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class ObservationTypePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<TreatmentBMPAssessmentObservationType>
    {
        public ObservationTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ObservationTypePrimaryKey(TreatmentBMPAssessmentObservationType TreatmentBMPAssessmentObservationType) : base(TreatmentBMPAssessmentObservationType){}

        public static implicit operator ObservationTypePrimaryKey(int primaryKeyValue)
        {
            return new ObservationTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator ObservationTypePrimaryKey(TreatmentBMPAssessmentObservationType TreatmentBMPAssessmentObservationType)
        {
            return new ObservationTypePrimaryKey(TreatmentBMPAssessmentObservationType);
        }
    }
}
//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TreatmentBMPTypeObservationType
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class TreatmentBMPTypeObservationTypePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<TreatmentBMPTypeObservationType>
    {
        public TreatmentBMPTypeObservationTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public TreatmentBMPTypeObservationTypePrimaryKey(TreatmentBMPTypeObservationType treatmentBMPTypeObservationType) : base(treatmentBMPTypeObservationType){}

        public static implicit operator TreatmentBMPTypeObservationTypePrimaryKey(int primaryKeyValue)
        {
            return new TreatmentBMPTypeObservationTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator TreatmentBMPTypeObservationTypePrimaryKey(TreatmentBMPTypeObservationType treatmentBMPTypeObservationType)
        {
            return new TreatmentBMPTypeObservationTypePrimaryKey(treatmentBMPTypeObservationType);
        }
    }
}
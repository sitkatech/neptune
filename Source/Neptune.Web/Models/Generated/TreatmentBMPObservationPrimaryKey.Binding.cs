//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TreatmentBMPObservation
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class TreatmentBMPObservationPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<TreatmentBMPObservation>
    {
        public TreatmentBMPObservationPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public TreatmentBMPObservationPrimaryKey(TreatmentBMPObservation treatmentBMPObservation) : base(treatmentBMPObservation){}

        public static implicit operator TreatmentBMPObservationPrimaryKey(int primaryKeyValue)
        {
            return new TreatmentBMPObservationPrimaryKey(primaryKeyValue);
        }

        public static implicit operator TreatmentBMPObservationPrimaryKey(TreatmentBMPObservation treatmentBMPObservation)
        {
            return new TreatmentBMPObservationPrimaryKey(treatmentBMPObservation);
        }
    }
}
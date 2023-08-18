//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TreatmentBMPObservation


namespace Neptune.EFModels.Entities
{
    public class TreatmentBMPObservationPrimaryKey : EntityPrimaryKey<TreatmentBMPObservation>
    {
        public TreatmentBMPObservationPrimaryKey() : base(){}
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
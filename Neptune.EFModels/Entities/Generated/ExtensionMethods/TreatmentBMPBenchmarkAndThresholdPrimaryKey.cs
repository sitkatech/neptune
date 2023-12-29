//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TreatmentBMPBenchmarkAndThreshold


namespace Neptune.EFModels.Entities
{
    public class TreatmentBMPBenchmarkAndThresholdPrimaryKey : EntityPrimaryKey<TreatmentBMPBenchmarkAndThreshold>
    {
        public TreatmentBMPBenchmarkAndThresholdPrimaryKey() : base(){}
        public TreatmentBMPBenchmarkAndThresholdPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public TreatmentBMPBenchmarkAndThresholdPrimaryKey(TreatmentBMPBenchmarkAndThreshold treatmentBMPBenchmarkAndThreshold) : base(treatmentBMPBenchmarkAndThreshold){}

        public static implicit operator TreatmentBMPBenchmarkAndThresholdPrimaryKey(int primaryKeyValue)
        {
            return new TreatmentBMPBenchmarkAndThresholdPrimaryKey(primaryKeyValue);
        }

        public static implicit operator TreatmentBMPBenchmarkAndThresholdPrimaryKey(TreatmentBMPBenchmarkAndThreshold treatmentBMPBenchmarkAndThreshold)
        {
            return new TreatmentBMPBenchmarkAndThresholdPrimaryKey(treatmentBMPBenchmarkAndThreshold);
        }
    }
}
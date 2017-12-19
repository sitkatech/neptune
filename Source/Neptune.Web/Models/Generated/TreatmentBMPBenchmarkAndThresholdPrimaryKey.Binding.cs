//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TreatmentBMPBenchmarkAndThreshold
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class TreatmentBMPBenchmarkAndThresholdPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<TreatmentBMPBenchmarkAndThreshold>
    {
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
using System;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Models
{
    public partial class ObservationThresholdType
    {
        public abstract string GetBenchmarkAndThresholdUrl(TreatmentBMP treatmentBMP, ObservationType observationType);
       
    }
    public partial class ObservationThresholdTypeSpecificValue : ObservationThresholdType
    {
        public override string GetBenchmarkAndThresholdUrl(TreatmentBMP treatmentBMP, ObservationType observationType)
        {
            return SitkaRoute<TreatmentBMPBenchmarkAndThresholdController>.BuildUrlFromExpression(c => c.EditBenchmarkAndThreshold(treatmentBMP, observationType));
        }
    }

    public partial class ObservationThresholdTypeRelativeToBenchmark : ObservationThresholdType
    {
        public override string GetBenchmarkAndThresholdUrl(TreatmentBMP treatmentBMP, ObservationType observationType)
        {
            return SitkaRoute<TreatmentBMPBenchmarkAndThresholdController>.BuildUrlFromExpression(c => c.EditBenchmarkAndThreshold(treatmentBMP, observationType));
        }
    }

    public partial class ObservationThresholdTypeNone : ObservationThresholdType
    {
        public override string GetBenchmarkAndThresholdUrl(TreatmentBMP treatmentBMP, ObservationType observationType)
        {
            throw new NotImplementedException();
        }
    }
}
using System;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Models
{
    public partial class ObservationThresholdType
    {
        public abstract string GetBenchmarkAndThresholdUrl(TreatmentBMP treatmentBMP, TreatmentBMPAssessmentObservationType TreatmentBMPAssessmentObservationType);
       
    }
    public partial class ObservationThresholdTypeSpecificValue : ObservationThresholdType
    {
        public override string GetBenchmarkAndThresholdUrl(TreatmentBMP treatmentBMP, TreatmentBMPAssessmentObservationType TreatmentBMPAssessmentObservationType)
        {
            return SitkaRoute<TreatmentBMPBenchmarkAndThresholdController>.BuildUrlFromExpression(c => c.EditBenchmarkAndThreshold(treatmentBMP, TreatmentBMPAssessmentObservationType));
        }
    }

    public partial class ObservationThresholdTypeRelativeToBenchmark : ObservationThresholdType
    {
        public override string GetBenchmarkAndThresholdUrl(TreatmentBMP treatmentBMP, TreatmentBMPAssessmentObservationType TreatmentBMPAssessmentObservationType)
        {
            return SitkaRoute<TreatmentBMPBenchmarkAndThresholdController>.BuildUrlFromExpression(c => c.EditBenchmarkAndThreshold(treatmentBMP, TreatmentBMPAssessmentObservationType));
        }
    }

    public partial class ObservationThresholdTypeNone : ObservationThresholdType
    {
        public override string GetBenchmarkAndThresholdUrl(TreatmentBMP treatmentBMP, TreatmentBMPAssessmentObservationType TreatmentBMPAssessmentObservationType)
        {
            throw new NotImplementedException();
        }
    }
}
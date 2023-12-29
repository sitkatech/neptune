using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static partial class TreatmentBMPTypeAssessmentObservationTypeExtensionMethods
{
    public static TreatmentBMPTypeObservationTypeDto AsTreatmentBMPTypeObservationTypeDto(this TreatmentBMPTypeAssessmentObservationType treatmentBMPTypeAssessmentObservationType)
    {
        var treatmentBMPTypeObservationTypeDto = new TreatmentBMPTypeObservationTypeDto()
        {
            TreatmentBMPTypeID = treatmentBMPTypeAssessmentObservationType.TreatmentBMPTypeID,
            TreatmentBMPAssessmentObservationTypeID = treatmentBMPTypeAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID,
            AssessmentScoreWeight = treatmentBMPTypeAssessmentObservationType.AssessmentScoreWeight * 100,
            DefaultThresholdValue = treatmentBMPTypeAssessmentObservationType.DefaultThresholdValue,
            DefaultBenchmarkValue = treatmentBMPTypeAssessmentObservationType.DefaultBenchmarkValue,
            OverrideAssessmentScoreIfFailing = treatmentBMPTypeAssessmentObservationType.OverrideAssessmentScoreIfFailing
        };
        return treatmentBMPTypeObservationTypeDto;
    }
}
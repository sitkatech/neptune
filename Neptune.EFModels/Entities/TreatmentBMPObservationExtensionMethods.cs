using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static partial class TreatmentBMPObservationExtensionMethods
{
    public static ObservationScoreDto AsObservationScoreDto(this TreatmentBMPObservation treatmentBMPObservation, bool overrideAssessmentScoreIfFailing)
    {
        var isComplete = treatmentBMPObservation.IsComplete();
        var treatmentBMPTypeObservationTypeDto = new ObservationScoreDto
        {
            ObservationScore = overrideAssessmentScoreIfFailing ? string.Empty : treatmentBMPObservation.FormattedObservationScore(),
            ObservationValue = treatmentBMPObservation.CalculateObservationValue(),
            IsComplete = isComplete,
            OverrideScore = overrideAssessmentScoreIfFailing && isComplete && treatmentBMPObservation.OverrideScoreForFailingObservation(),
            OverrideScoreText = treatmentBMPObservation.CalculateOverrideScoreText(overrideAssessmentScoreIfFailing),
        };
        return treatmentBMPTypeObservationTypeDto;
    }

}
using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static partial class TreatmentBMPAssessmentObservationTypeExtensionMethods
{
    public static TreatmentBMPAssessmentObservationTypeDetailedDto AsDetailedDto(this TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType)
    {
        var treatmentBMPAssessmentObservationTypeDetailedDto = new TreatmentBMPAssessmentObservationTypeDetailedDto()
        {
            TreatmentBMPAssessmentObservationTypeID = treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID,
            HasBenchmarkAndThresholds = treatmentBMPAssessmentObservationType.GetHasBenchmarkAndThreshold(),
            TargetIsSweetSpot = treatmentBMPAssessmentObservationType.GetTargetIsSweetSpot(),
            TreatmentBMPAssessmentObservationTypeName = $"{treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeName}",
            BenchmarkUnitLegendDisplayName = treatmentBMPAssessmentObservationType.BenchmarkMeasurementUnitType()?.LegendDisplayName ?? string.Empty,
            ThresholdUnitLegendDisplayName = treatmentBMPAssessmentObservationType.ThresholdMeasurementUnitType()?.LegendDisplayName ?? string.Empty,
            CollectionMethodDisplayName = treatmentBMPAssessmentObservationType.ObservationTypeSpecification.ObservationTypeCollectionMethod.ObservationTypeCollectionMethodDisplayName
        };
        return treatmentBMPAssessmentObservationTypeDetailedDto;
    }

}
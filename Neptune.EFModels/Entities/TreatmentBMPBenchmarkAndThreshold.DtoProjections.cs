using System.Linq.Expressions;
using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static class TreatmentBMPBenchmarkAndThresholdDtoProjections
{
    public static readonly Expression<Func<TreatmentBMPBenchmarkAndThreshold, TreatmentBMPBenchmarkAndThresholdDto>> AsDto = x => new TreatmentBMPBenchmarkAndThresholdDto
    {
        TreatmentBMPBenchmarkAndThresholdID = x.TreatmentBMPBenchmarkAndThresholdID,
        TreatmentBMPID = x.TreatmentBMPID,
        TreatmentBMPTypeAssessmentObservationTypeID = x.TreatmentBMPTypeAssessmentObservationTypeID,
        TreatmentBMPTypeID = x.TreatmentBMPTypeID,
        TreatmentBMPAssessmentObservationTypeID = x.TreatmentBMPAssessmentObservationTypeID,
        BenchmarkValue = x.BenchmarkValue,
        ThresholdValue = x.ThresholdValue,
        ObservationTypeName = x.TreatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeName
    };
}

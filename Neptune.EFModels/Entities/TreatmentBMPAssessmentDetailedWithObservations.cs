using System.Globalization;

namespace Neptune.EFModels.Entities;

public class TreatmentBMPAssessmentDetailedWithObservations
{
    public vTreatmentBMPAssessmentDetailed TreatmentBMPAssessment { get; }
    public IEnumerable<vTreatmentBMPObservation> TreatmentBMPObservations { get; }

    public TreatmentBMPAssessmentDetailedWithObservations(vTreatmentBMPAssessmentDetailed treatmentBMPAssessment, IEnumerable<vTreatmentBMPObservation> treatmentBMPObservations)
    {
        TreatmentBMPAssessment = treatmentBMPAssessment;
        TreatmentBMPObservations = treatmentBMPObservations;
    }

    public string CalculateObservationValueForObservationType(TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType)
    {
        if (treatmentBMPAssessmentObservationType.TreatmentBMPTypeAssessmentObservationTypes.All(x => x.TreatmentBMPTypeID != TreatmentBMPAssessment.TreatmentBMPTypeID))
        {
            return "n/a";
        }

        var observationTypeCollectionMethodEnum = treatmentBMPAssessmentObservationType.ObservationTypeSpecification.ObservationTypeCollectionMethod.ToEnum;
        var treatmentBMPObservations = TreatmentBMPObservations.Where(x => x.TreatmentBMPAssessmentObservationTypeID == treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID && x.ObservationValue != null).ToList();
        if (!treatmentBMPObservations.Any())
        {
            return "not provided";
        }
        switch (observationTypeCollectionMethodEnum)
        {
            case ObservationTypeCollectionMethodEnum.DiscreteValue:
                var average = treatmentBMPObservations.Average(x => double.Parse(x.ObservationValue));
                return average.ToString(CultureInfo.InvariantCulture);
            case ObservationTypeCollectionMethodEnum.PassFail:
                var conveyanceFails = treatmentBMPObservations.Any(x => bool.Parse(x.ObservationValue) == false);
                var observationValue = conveyanceFails ? 0 : 5;
                return Math.Abs(observationValue - 5) < 0.0001 ? "Pass" : "Fail";
            case ObservationTypeCollectionMethodEnum.Percentage:
                return (treatmentBMPObservations.Sum(x => double.Parse(x.ObservationValue))/ 100).ToString("P");
            default:
                throw new ArgumentOutOfRangeException(
                    $"Unknown ObservationTypeCollectionMethodEnum {observationTypeCollectionMethodEnum}");
        }
    }

}
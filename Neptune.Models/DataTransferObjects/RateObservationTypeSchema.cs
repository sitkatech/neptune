namespace Neptune.Models.DataTransferObjects;

public class RateObservationTypeSchema
{
    public string DiscreteRateMeasurementUnitLabel { get; set; }
    public int DiscreteRateMeasurementUnitTypeID { get; set; }

    public string TimeMeasurementUnitLabel { get; set; }
    public int TimeMeasurementUnitTypeID { get; set; }

    public string ReadingMeasurementUnitLabel { get; set; }
    public int ReadingMeasurementUnitTypeID { get; set; }

    public List<string> PropertiesToObserve { get; set; }

    public int DiscreteRateMinimumNumberOfObservations { get; set; }
    public int? DiscreteRateMaximumNumberOfObservations { get; set; }

    public double DiscreteRateMinimumValueOfObservations { get; set; }
    public double? DiscreteRateMaximumValueOfObservations { get; set; }

    public int TimeReadingMinimumNumberOfObservations { get; set; }
    public int? TimeReadingMaximumNumberOfObservations { get; set; }

    public double TimeReadingMinimumValueOfObservations { get; set; }
    public double? TimeReadingMaximumValueOfObservations { get; set; }
    public string BenchmarkDescription { get; set; }
    public string ThresholdDescription { get; set; }
    public string AssessmentDescription { get; set; }
}
using Neptune.Web.Models;
using System.Collections.Generic;

namespace Neptune.Web.Views.ObservationType
{
    public class DiscreteObservationTypeSchema
    {
        public string MeasurementUnitLabel { get; set; }
        public int MeasurementUnitTypeID { get; set; }

        public List<string> PropertiesToObserve { get; set; }

        public int MinimumNumberOfObservations { get; set; }
        public int? MaximumNumberOfObservations { get; set; }

        public double MinimumValueOfObservations { get; set; }
        public double? MaximumValueOfObservations { get; set; }

        public string BenchmarkDescription { get; set; }
        public string ThresholdDescription { get; set; }
        public string AssessmentDescription { get; set; }
    }

    public class RateObservationTypeSchema
    {
        public string DiscreteRateMeasurementUnitLabel { get; set; }
        public int DiscreteRateMeasurementUnitTypeID { get; set; }

        public string TimeMeasurementUnitLabel { get; set; }
        public int TimeMeasurementUnitTypeID { get; set; }

        public string ReadingMeasurementUnitLabel { get; set; }
        public int ReadingMeasurementUnitTypeID { get; set; }

        public List<string> PropertiesToObserve { get; set; }

        public int DiscretRateMinimumNumberOfObservations { get; set; }
        public int? DiscretRateMaximumNumberOfObservations { get; set; }

        public double DiscretRateMinimumValueOfObservations { get; set; }
        public double? DiscretRateMaximumValueOfObservations { get; set; }

        public int TimeReadingMinimumNumberOfObservations { get; set; }
        public int? TimeReadingMaximumNumberOfObservations { get; set; }

        public double TimeReadingMinimumValueOfObservations { get; set; }
        public double? TimeReadingMaximumValueOfObservations { get; set; }
        public string BenchmarkDescription { get; set; }
        public string ThresholdDescription { get; set; }
        public string AssessmentDescription { get; set; }
    }

    public class PassFailObservationTypeSchema
    {
        public List<string> PropertiesToObserve { get; set; }
        public string AssessmentDescription { get; set; }
    }

    public class PercentageObservationTypeSchema
    {
        public string MeasurementUnitLabel { get; set; }

        public List<string> PropertiesToObserve { get; set; }
        public string BenchmarkDescription { get; set; }
        public string ThresholdDescription { get; set; }
        public string AssessmentDescription { get; set; }
    }

    public class DiscreteObservationSchema {
        public List<SingleValueObservation> SingleValueObservations { get; set; }
    }

    public class SingleValueObservation
    {
        public string PropertyObserved { get; set; }
        public double ObservationValue { get; set; }
        public string Notes { get; set; }
    }


}
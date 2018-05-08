using Neptune.Web.Models;
using System.Collections.Generic;

namespace Neptune.Web.Views.CustomAttributeType
{
    public class DiscreteCustomAttributeTypeSchema
    {
        public string MeasurementUnitLabel { get; set; }
        public int MeasurementUnitTypeID { get; set; }

        public List<string> PropertiesToObserve { get; set; }

        public int MinimumNumberOfCustomAttributes { get; set; }
        public int? MaximumNumberOfCustomAttributes { get; set; }

        public double MinimumValueOfCustomAttributes { get; set; }
        public double? MaximumValueOfCustomAttributes { get; set; }

        public string BenchmarkDescription { get; set; }
        public string ThresholdDescription { get; set; }
        public string AssessmentDescription { get; set; }
    }

    public class RateCustomAttributeTypeSchema
    {
        public string DiscreteRateMeasurementUnitLabel { get; set; }
        public int DiscreteRateMeasurementUnitTypeID { get; set; }

        public string TimeMeasurementUnitLabel { get; set; }
        public int TimeMeasurementUnitTypeID { get; set; }

        public string ReadingMeasurementUnitLabel { get; set; }
        public int ReadingMeasurementUnitTypeID { get; set; }

        public List<string> PropertiesToObserve { get; set; }

        public int DiscreteRateMinimumNumberOfCustomAttributes { get; set; }
        public int? DiscreteRateMaximumNumberOfCustomAttributes { get; set; }

        public double DiscreteRateMinimumValueOfCustomAttributes { get; set; }
        public double? DiscreteRateMaximumValueOfCustomAttributes { get; set; }

        public int TimeReadingMinimumNumberOfCustomAttributes { get; set; }
        public int? TimeReadingMaximumNumberOfCustomAttributes { get; set; }

        public double TimeReadingMinimumValueOfCustomAttributes { get; set; }
        public double? TimeReadingMaximumValueOfCustomAttributes { get; set; }
        public string BenchmarkDescription { get; set; }
        public string ThresholdDescription { get; set; }
        public string AssessmentDescription { get; set; }
    }

    public class PassFailCustomAttributeTypeSchema
    {
        public List<string> PropertiesToObserve { get; set; }
        public string AssessmentDescription { get; set; }
        public string PassingScoreLabel { get; set; }
        public string FailingScoreLabel { get; set; }
    }

    public class PercentageCustomAttributeTypeSchema
    {
        public string MeasurementUnitLabel { get; set; }

        public List<string> PropertiesToObserve { get; set; }
        public string BenchmarkDescription { get; set; }
        public string ThresholdDescription { get; set; }
        public string AssessmentDescription { get; set; }
    }

    public class DiscreteCustomAttributeSchema {
        public List<SingleValueCustomAttribute> SingleValueCustomAttributes { get; set; }
    }

    public class RateCustomAttributeSchema
    {
        public List<SingleValueCustomAttribute> SingleValueCustomAttributes { get; set; }
        public List<TimeValuePairCustomAttribute> TimeValuePairCustomAttributes { get; set; }
    }

    public class PassFailCustomAttributeSchema
    {
        public List<PassFailCustomAttribute> PassFailCustomAttributes { get; set; }
    }


    public class PercentageCustomAttributeSchema
    {
        public List<SingleValueCustomAttribute> SingleValueCustomAttributes { get; set; }
    }

    public class SingleValueCustomAttribute
    {
        public string PropertyObserved { get; set; }
        public double CustomAttributeValue { get; set; }
        public string Notes { get; set; }
    }

    public class TimeValuePairCustomAttribute
    {
        public string PropertyObserved { get; set; }
        public double CustomAttributeValue { get; set; }
        public double CustomAttributeTime { get; set; }
        public string Notes { get; set; }
    }

    public class PassFailCustomAttribute
    {
        public string PropertyObserved { get; set; }
        public bool CustomAttributeValue { get; set; }
        public string Notes { get; set; }
    }

}
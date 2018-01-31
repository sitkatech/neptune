using Neptune.Web.Models;
using System.Collections.Generic;

namespace Neptune.Web.Views.TreatmentBMPAttributeType
{
    public class DiscreteTreatmentBMPAttributeTypeSchema
    {
        public string MeasurementUnitLabel { get; set; }
        public int MeasurementUnitTypeID { get; set; }

        public List<string> PropertiesToObserve { get; set; }

        public int MinimumNumberOfTreatmentBMPAttributes { get; set; }
        public int? MaximumNumberOfTreatmentBMPAttributes { get; set; }

        public double MinimumValueOfTreatmentBMPAttributes { get; set; }
        public double? MaximumValueOfTreatmentBMPAttributes { get; set; }

        public string BenchmarkDescription { get; set; }
        public string ThresholdDescription { get; set; }
        public string AssessmentDescription { get; set; }
    }

    public class RateTreatmentBMPAttributeTypeSchema
    {
        public string DiscreteRateMeasurementUnitLabel { get; set; }
        public int DiscreteRateMeasurementUnitTypeID { get; set; }

        public string TimeMeasurementUnitLabel { get; set; }
        public int TimeMeasurementUnitTypeID { get; set; }

        public string ReadingMeasurementUnitLabel { get; set; }
        public int ReadingMeasurementUnitTypeID { get; set; }

        public List<string> PropertiesToObserve { get; set; }

        public int DiscreteRateMinimumNumberOfTreatmentBMPAttributes { get; set; }
        public int? DiscreteRateMaximumNumberOfTreatmentBMPAttributes { get; set; }

        public double DiscreteRateMinimumValueOfTreatmentBMPAttributes { get; set; }
        public double? DiscreteRateMaximumValueOfTreatmentBMPAttributes { get; set; }

        public int TimeReadingMinimumNumberOfTreatmentBMPAttributes { get; set; }
        public int? TimeReadingMaximumNumberOfTreatmentBMPAttributes { get; set; }

        public double TimeReadingMinimumValueOfTreatmentBMPAttributes { get; set; }
        public double? TimeReadingMaximumValueOfTreatmentBMPAttributes { get; set; }
        public string BenchmarkDescription { get; set; }
        public string ThresholdDescription { get; set; }
        public string AssessmentDescription { get; set; }
    }

    public class PassFailTreatmentBMPAttributeTypeSchema
    {
        public List<string> PropertiesToObserve { get; set; }
        public string AssessmentDescription { get; set; }
        public string PassingScoreLabel { get; set; }
        public string FailingScoreLabel { get; set; }
    }

    public class PercentageTreatmentBMPAttributeTypeSchema
    {
        public string MeasurementUnitLabel { get; set; }

        public List<string> PropertiesToObserve { get; set; }
        public string BenchmarkDescription { get; set; }
        public string ThresholdDescription { get; set; }
        public string AssessmentDescription { get; set; }
    }

    public class DiscreteTreatmentBMPAttributeSchema {
        public List<SingleValueTreatmentBMPAttribute> SingleValueTreatmentBMPAttributes { get; set; }
    }

    public class RateTreatmentBMPAttributeSchema
    {
        public List<SingleValueTreatmentBMPAttribute> SingleValueTreatmentBMPAttributes { get; set; }
        public List<TimeValuePairTreatmentBMPAttribute> TimeValuePairTreatmentBMPAttributes { get; set; }
    }

    public class PassFailTreatmentBMPAttributeSchema
    {
        public List<PassFailTreatmentBMPAttribute> PassFailTreatmentBMPAttributes { get; set; }
    }


    public class PercentageTreatmentBMPAttributeSchema
    {
        public List<SingleValueTreatmentBMPAttribute> SingleValueTreatmentBMPAttributes { get; set; }
    }

    public class SingleValueTreatmentBMPAttribute
    {
        public string PropertyObserved { get; set; }
        public double TreatmentBMPAttributeValue { get; set; }
        public string Notes { get; set; }
    }

    public class TimeValuePairTreatmentBMPAttribute
    {
        public string PropertyObserved { get; set; }
        public double TreatmentBMPAttributeValue { get; set; }
        public double TreatmentBMPAttributeTime { get; set; }
        public string Notes { get; set; }
    }

    public class PassFailTreatmentBMPAttribute
    {
        public string PropertyObserved { get; set; }
        public bool TreatmentBMPAttributeValue { get; set; }
        public string Notes { get; set; }
    }

}
namespace Neptune.Models.DataTransferObjects
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

    //public class RateObservationSchema
    //{
    //    public List<SingleValueObservation> SingleValueObservations { get; set; }
    //    public List<TimeValuePairObservation> TimeValuePairObservations { get; set; }
    //}


    //public class TimeValuePairObservation
    //{
    //    public string PropertyObserved { get; set; }
    //    public double? ObservationValue { get; set; }
    //    public double ObservationTime { get; set; }
    //    public string Notes { get; set; }
    //}
}
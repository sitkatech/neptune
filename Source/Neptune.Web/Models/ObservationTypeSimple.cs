namespace Neptune.Web.Models
{
    public class ObservationTypeSimple
    {
        public int ObservationTypeID { get; set; }
        public bool HasBenchmarkAndThresholds { get; set; }
        public string DisplayName { get; set; }       

        public ObservationTypeSimple(ObservationType observationType)
        {
            ObservationTypeID = observationType.ObservationTypeID;
            HasBenchmarkAndThresholds = observationType.HasBenchmarkAndThreshold;
            DisplayName = $"{observationType.ObservationTypeName}";                        
        }
    }
}
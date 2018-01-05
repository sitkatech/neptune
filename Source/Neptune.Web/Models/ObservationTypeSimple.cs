namespace Neptune.Web.Models
{
    public class ObservationTypeSimple
    {
        public int ObservationTypeID { get; set; }
        public bool HasBenchmarkAndThresholds { get; set; }
        public string ObservationTypeName { get; set; }       

        public ObservationTypeSimple(ObservationType observationType)
        {
            ObservationTypeID = observationType.ObservationTypeID;
            HasBenchmarkAndThresholds = observationType.HasBenchmarkAndThreshold;
            ObservationTypeName = $"{observationType.ObservationTypeName}";                        
        }
    }
}
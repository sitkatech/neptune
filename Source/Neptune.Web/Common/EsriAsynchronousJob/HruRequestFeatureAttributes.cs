namespace Neptune.Web.Common.EsriAsynchronousJob
{
    public class HruRequestFeatureAttributes
    {
        public int OBJECTID { get; set; }
        public int QueryFeatureID { get; set; } // actually wants to be a string..?
        public double Shape_Length { get; set; }
        public double Shape_Area { get; set; }
    }
}
// Classes for preparing the Esri JSON input to the HRU service.
// Names have to match remote service's expectation, therefore:
// ReSharper disable InconsistentNaming

using System.Collections.Generic;
using Neptune.Web.Models;
using Newtonsoft.Json;

namespace Neptune.Web.Common.EsriAsynchronousJob
{
    public class HRURequestFeature
    {
        [JsonProperty("attributes")]
        public HRURequestFeatureAttributes Attributes { get; set; }
        [JsonProperty("geometry")]
        public EsriPolygonGeometry Geometry { get; set; }

        public HRURequestFeature(TreatmentBMP treatmentBMP)
        {
            Attributes = new HRURequestFeatureAttributes
            {
                ObjectID = treatmentBMP.TreatmentBMPID,
                QueryFeatureID = treatmentBMP.TreatmentBMPID,
                Area = treatmentBMP.Delineation.DelineationGeometry.Area.GetValueOrDefault(),
                Length = treatmentBMP.Delineation.DelineationGeometry.Length.GetValueOrDefault(),
            };

            var coordinates = new List<double[]>();
            for (var i = 1; i <= treatmentBMP.Delineation.DelineationGeometry.ExteriorRing.PointCount; i++)
            {
                var point = treatmentBMP.Delineation.DelineationGeometry.ExteriorRing.PointAt(i);
                var lon = point.XCoordinate.GetValueOrDefault();
                var lat = point.YCoordinate.GetValueOrDefault();

                coordinates.Add(new[] { lon, lat });
            }

            Geometry = new EsriPolygonGeometry
            {
                Rings = new List<List<double[]>> { coordinates }
            };
        }
    }

    public class HRUResponseFeature
    {
        [JsonProperty("attributes")]
        public HRUResponseFeatureAttributes Attributes { get; set; }
        [JsonProperty("geometry")]
        public EsriPolygonGeometry Geometry { get; set; }

        public HRUCharacteristic ToHRUCharacteristic(TreatmentBMP treatmentBMP)
        {
            return new HRUCharacteristic(Attributes.LSPCLandUseDescription,
                Attributes.HydrologicSoilGroup, Attributes.SlopePercentage, Attributes.ImperviousAcres){TreatmentBMPID = treatmentBMP.TreatmentBMPID};
        }
    }

    public class HRUResponseFeatureAttributes
    {
        [JsonProperty("OBJECTID")]
        public int ObjectID { get; set; }
        [JsonProperty("QueryFeatureID")]
        public int QueryFeatureID { get; set; }
        [JsonProperty("HRU_Composite_LSPC_LU_DESC")]
        public string LSPCLandUseDescription { get; set; }
        [JsonProperty("HRU_Composite_soil_hsg")]
        public string HydrologicSoilGroup { get; set; }
        [JsonProperty("HRU_Composite_slope_pct")]
        public int SlopePercentage { get; set; }
        [JsonProperty("SUM_imp_acres")]
        public double ImperviousAcres { get; set; }
        [JsonProperty("Shape_Length")]
        public double Length { get; set; }
        [JsonProperty("Shape_Area")]
        public double Area { get; set; }
    }
}
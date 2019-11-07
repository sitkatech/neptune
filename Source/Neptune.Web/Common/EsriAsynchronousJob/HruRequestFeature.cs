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

        public HRURequestFeature(IHaveHRUCharacteristics iHaveHRUCharacteristics)
        {
            Attributes = new HRURequestFeatureAttributes
            {
                ObjectID = iHaveHRUCharacteristics.PrimaryKey,
                QueryFeatureID = iHaveHRUCharacteristics.PrimaryKey,
                Area = iHaveHRUCharacteristics.GetCatchmentGeometry().Area.GetValueOrDefault(),
                Length = iHaveHRUCharacteristics.GetCatchmentGeometry().Length.GetValueOrDefault(),
            };

            var coordinates = new List<double[]>();
            for (var i = 1; i <= iHaveHRUCharacteristics.GetCatchmentGeometry().ExteriorRing.PointCount; i++)
            {
                var point = iHaveHRUCharacteristics.GetCatchmentGeometry().ExteriorRing.PointAt(i);
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
}
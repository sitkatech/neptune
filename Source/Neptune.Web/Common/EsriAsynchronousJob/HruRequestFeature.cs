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

            var rings = new List<List<double[]>>();

            // todo: make this iterate through the geometry parts since this might be a multipolygon

            var coordinates = new List<double[]>();
            
            // this will break unceremoniously if catchment geometry is multipolygon instead of polygon. 
            // unclear if this is going to break anything in practice.
            // I don't even know if the HRU service will accept a multi-geometry.

            var catchmentGeometry = iHaveHRUCharacteristics.GetCatchmentGeometry();

            for (var i = 1; i <= catchmentGeometry.ExteriorRing.PointCount; i++)
            {
                var point = catchmentGeometry.ExteriorRing.PointAt(i);
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
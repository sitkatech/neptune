// Classes for preparing the Esri JSON input to the HRU service.
// Names have to match remote service's expectation, therefore:
// ReSharper disable InconsistentNaming

using System.Collections.Generic;
using Neptune.Web.Models;

namespace Neptune.Web.Common.EsriAsynchronousJob
{
    public class HruRequestFeature
    {
        public HruRequestFeatureAttributes attributes { get; set; }

        public EsriPolygonGeometry geometry { get; set; }

        public HruRequestFeature(TreatmentBMP treatmentBMP)
        {
            attributes = new HruRequestFeatureAttributes
            {
                OBJECTID = treatmentBMP.TreatmentBMPID,
                QueryFeatureID = treatmentBMP.TreatmentBMPID,
                Shape_Area = treatmentBMP.Delineation.DelineationGeometry.Area.GetValueOrDefault(),
                Shape_Length = treatmentBMP.Delineation.DelineationGeometry.Length.GetValueOrDefault(),
            };

            var coordinates = new List<double[]>();
            for (var i = 1; i <= treatmentBMP.Delineation.DelineationGeometry.ExteriorRing.PointCount; i++)
            {
                var point = treatmentBMP.Delineation.DelineationGeometry.ExteriorRing.PointAt(i);
                var lon = point.XCoordinate.GetValueOrDefault();
                var lat = point.YCoordinate.GetValueOrDefault();

                coordinates.Add(new[] { lon, lat });
            }

            geometry = new EsriPolygonGeometry
            {
                rings = new List<List<double[]>> { coordinates }
            };
        }
    }
}
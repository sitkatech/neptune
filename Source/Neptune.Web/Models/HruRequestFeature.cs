// Classes for preparing the Esri JSON input to the HRU service.
// Names have to match remote service's expectation, therefore:
// ReSharper disable InconsistentNaming

using System.Collections.Generic;
using LtInfo.Common.GeoJson;

namespace Neptune.Web.Models
{
    public class HruRequestFeature
    {
        public HruRequestFeatureAttributes attributes { get; set; }

        public EsriPolygonGeometry geometry { get; set; }

        public HruRequestFeature()
        {
            attributes = new HruRequestFeatureAttributes{OBJECTID = 1, QueryFeatureID = 2, Shape_Area = 4, Shape_Length = 3};
            List<double[]> coordinates = new List<double[]>();
            double[] coord = new[] { 1.0, 2.0 };
            coordinates.Add(coord);
            geometry = new EsriPolygonGeometry { rings = new List<List<double[]>> { coordinates } };
        }

        public HruRequestFeature(TreatmentBMP treatmentBMP)
        {
            attributes = new HruRequestFeatureAttributes
            {
                OBJECTID = treatmentBMP.TreatmentBMPID,
                QueryFeatureID = treatmentBMP.TreatmentBMPID,
                Shape_Area = (double) treatmentBMP.Delineation.DelineationGeometry.Area.GetValueOrDefault(),
                Shape_Length = (double) treatmentBMP.Delineation.DelineationGeometry.Length.GetValueOrDefault(),
            };

            //var geoJsonGeometry = DbGeometryToGeoJsonHelper.FromDbGeometryNoReprojecc(treatmentBMP.Delineation.DelineationGeometry);
            //geoJsonGeometry.Geometry
            var coordinates = new List<double[]>();
            for (var i = 1; i <= treatmentBMP.Delineation.DelineationGeometry.ExteriorRing.PointCount; i++)
            {
                var point = treatmentBMP.Delineation.DelineationGeometry.ExteriorRing.PointAt(i);
                var lon = point.XCoordinate.GetValueOrDefault();
                var lat = point.YCoordinate.GetValueOrDefault();

                coordinates.Add(new double[] { lon, lat});
            }

            geometry = new EsriPolygonGeometry
            {
                rings = new List<List<double[]>> {coordinates}
            };
        }
    }

    public class EsriPolygonGeometry
    {
        public List<List<double[]>> rings { get; set; }
    }

    public class HruRequestFeatureAttributes
    {
        public int OBJECTID { get; set; }
        public int QueryFeatureID { get; set; } // actually wants to be a string..?
        public double Shape_Length { get; set; }
        public double Shape_Area { get; set; }
    }
}
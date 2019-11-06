// Classes for preparing the Esri JSON input to the HRU service.
// Names have to match remote service's expectation, therefore:
// ReSharper disable InconsistentNaming

using LtInfo.Common;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Neptune.Web.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class HruRequest
    {
        [JsonProperty]
        public static string geometryType { get; }
        [JsonProperty]
        public static string exceededTransferLimit { get; }
        [JsonProperty]
        public static List<EsriField> fields { get; }
        [JsonProperty]
        public static EsriSpatialReference spatialReference { get; }
        [JsonProperty]
        public List<HruRequestFeature> features { get; }

        static HruRequest()
        {
            geometryType = "esriGeometryPolygon";
            exceededTransferLimit = "false";
            spatialReference = new EsriSpatialReference { wkid = CoordinateSystemHelper.NAD_83_HARN_CA_ZONE_VI_SRID };
            fields = new List<EsriField>
            {
                new EsriField
                {
                    name = "OBJECTID",
                    type = "esriFieldTypeOID",
                    alias = "OBJECTID"

                },

                new EsriField
                {
                    name = "QueryFeatureID",
                    type = "esriFieldTypeString",
                    alias = "QueryFeatureID",
                    length = 255
                },

                new EsriField
                {
                    name = "Shape_Length",
                    type = "esriFieldTypeDouble",
                    alias = "Shape_Length"
                },

                new EsriField
                {
                    name = "Shape_Area",
                    type = "esriFieldTypeDouble",
                    alias = "Shape_Area"
                }
            };
        }

        public HruRequest(TreatmentBMP treatmentBMP)
        {
            features = new List<HruRequestFeature>{new HruRequestFeature(treatmentBMP)};
        }
    }

    public class EsriField
    {
        public string name { get; set; }
        public string type { get; set; }
        public string alias { get; set; }
        public int length { get; set; }
    }

    public class EsriSpatialReference
    {
        public int wkid { get; set; }
    }

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
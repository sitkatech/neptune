// Classes for preparing the Esri JSON input to the HRU service.
// Names have to match remote service's expectation, therefore:
// ReSharper disable InconsistentNaming

using System.Collections.Generic;
using LtInfo.Common;
using Neptune.Web.Models;
using Newtonsoft.Json;

namespace Neptune.Web.Common.EsriAsynchronousJob
{
    public class EsriGPRecordSetLayer<T>
    {

        [JsonProperty("geometryType")]
        public string GeometryType { get; set; }
        [JsonProperty("exceededTransferLimit")]
        public string ExceededTransferLimit { get; set; }
        [JsonProperty("fields")]
        public List<EsriField> Fields { get; set; }
        [JsonProperty("spatialReference")]
        public EsriSpatialReference SpatialReference { get; set; }
        [JsonProperty("features")]
        public List<T> Features { get; set; }
    }

    public static class EsriGPRecordSetLayer
    {
        public static EsriGPRecordSetLayer<HruRequestFeature> GetGPRecordSetLayer(TreatmentBMP treatmentBMP)
        {
            return new EsriGPRecordSetLayer<HruRequestFeature>
            {

                Features = new List<HruRequestFeature> { new HruRequestFeature(treatmentBMP) },
                GeometryType = "esriGeometryPolygon",
                ExceededTransferLimit = "false",
                SpatialReference = new EsriSpatialReference { wkid = CoordinateSystemHelper.NAD_83_HARN_CA_ZONE_VI_SRID },
                Fields = new List<EsriField>
                {
                    new EsriField
                    {
                        Name = "OBJECTID",
                        Type = "esriFieldTypeOID",
                        Alias = "OBJECTID"

                    },

                    new EsriField
                    {
                        Name = "QueryFeatureID",
                        Type = "esriFieldTypeString",
                        Alias = "QueryFeatureID",
                        Length = 255
                    },

                    new EsriField
                    {
                        Name = "Shape_Length",
                        Type = "esriFieldTypeDouble",
                        Alias = "Shape_Length"
                    },

                    new EsriField
                    {
                        Name = "Shape_Area",
                        Type = "esriFieldTypeDouble",
                        Alias = "Shape_Area"
                    }
                }
            };
        }
    }
}
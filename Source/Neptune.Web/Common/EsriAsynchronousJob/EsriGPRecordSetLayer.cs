// Classes for preparing the Esri JSON input to the HRU service.
// Names have to match remote service's expectation, therefore:
// ReSharper disable InconsistentNaming

using System.Collections.Generic;
using LtInfo.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Common.EsriAsynchronousJob
{
    public class EsriGPRecordSetLayer<T>
    {
        public string geometryType { get; set; }
        public string exceededTransferLimit { get; set; }
        public List<EsriField> fields { get; set; }
        public EsriSpatialReference spatialReference { get; set; }
        public List<T> features { get; set; }
    }

    public static class EsriGPRecordSetLayer
    {
        public static EsriGPRecordSetLayer<HruRequestFeature> GetGPRecordSetLayer(TreatmentBMP treatmentBMP)
        {
            return new EsriGPRecordSetLayer<HruRequestFeature>
            {

                features = new List<HruRequestFeature> { new HruRequestFeature(treatmentBMP) },
                geometryType = "esriGeometryPolygon",
                exceededTransferLimit = "false",
                spatialReference = new EsriSpatialReference { wkid = CoordinateSystemHelper.NAD_83_HARN_CA_ZONE_VI_SRID },
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
                }
            };
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ApprovalUtilities.Utilities;
using LtInfo.Common;
using LtInfo.Common.DesignByContract;
using Neptune.Web.Common.EsriAsynchronousJob;
using Neptune.Web.Models;
using Newtonsoft.Json;

namespace Neptune.Web.Common
{
    public static class HRUHelper
    {
        public static void RetrieveAndSaveHRUCharacteristics(IHaveHRUCharacteristics iHaveHRUCharacteristics, Func<HRUCharacteristic, int?> primaryKeySetterAction)
        {
            Check.Assert(iHaveHRUCharacteristics.GetCatchmentGeometry() != null, "Entity must have a catchment geometry to calculate HRU Characteristics.");
            var postUrl = NeptuneWebConfiguration.HRUServiceBaseUrl;
            var esriAsynchronousJobRunner = new EsriAsynchronousJobRunner(postUrl, "HRU_Composite");

            var hruRequest = GetGPRecordSetLayer(iHaveHRUCharacteristics);

            var serializeObject = new
            {
                Input_Polygons = JsonConvert.SerializeObject(hruRequest),
                returnZ = false,
                returnM = false,
                returnTrueCurves = false,
                f = "pjson"
            };

            HttpRequestStorage.DatabaseEntities.HRUCharacteristics.DeleteHRUCharacteristic(iHaveHRUCharacteristics.HRUCharacteristics);
            HttpRequestStorage.DatabaseEntities.SaveChanges();
            var hruCharacteristics =
                esriAsynchronousJobRunner
                    .RunJob<EsriAsynchronousJobOutputParameter<EsriGPRecordSetLayer<HRUResponseFeature>>>(serializeObject).Value
                    .Features.Select(x =>
                    {
                        var hruCharacteristic = x.ToHRUCharacteristic();
                        primaryKeySetterAction.Invoke(hruCharacteristic);
                        return hruCharacteristic;
                    });
            iHaveHRUCharacteristics.HRUCharacteristics.AddAll(hruCharacteristics);
            HttpRequestStorage.DatabaseEntities.SaveChanges();
        }

        public static EsriGPRecordSetLayer<HRURequestFeature> GetGPRecordSetLayer(IHaveHRUCharacteristics iHaveHRUCharacteristics)
        {
            return new EsriGPRecordSetLayer<HRURequestFeature>
            {
                Features = iHaveHRUCharacteristics.GetHRURequestFeatures().ToList(),
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
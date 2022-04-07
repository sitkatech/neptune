using log4net;
using LtInfo.Common;
using Neptune.Web.Common.EsriAsynchronousJob;
using Neptune.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Neptune.Web.Common
{
    public static class HRUUtilities
    {
        public static IEnumerable<HRUResponseFeature> RetrieveHRUResponseFeatures(
            List<HRURequestFeature> featuresForHRURequest, ILog logger)
        {
            var postUrl = NeptuneWebConfiguration.HRUServiceBaseUrl;
            var esriAsynchronousJobRunner = new EsriAsynchronousJobRunner(postUrl, "output_table");

            var hruRequest = GetGPRecordSetLayer(featuresForHRURequest);

            var serializeObject = new
            {
                input_fc = JsonConvert.SerializeObject(hruRequest),
                returnZ = false,
                returnM = false,
                returnTrueCurves = false,
                f = "pjson"
            };

            var newHRUCharacteristics = new List<HRUResponseFeature>();
            var rawResponse = string.Empty;
            try
            {
                var esriGPRecordSetLayer = esriAsynchronousJobRunner
                .RunJob<EsriAsynchronousJobOutputParameter<EsriGPRecordSetLayer<HRUResponseFeature>>>(
                    // ReSharper disable once UnusedVariable
                    serializeObject, out rawResponse).Value;

                newHRUCharacteristics.AddRange(
                    esriGPRecordSetLayer
                        .Features.Where(x=>x.Attributes.ImperviousAcres!= null));

            }
            catch (Exception ex)
            {
                logger.Warn(ex.Message, ex);
                logger.Warn($"Skipped entities (ProjectLGUs if Project modeling, otherwise LGUs) with these IDs: {string.Join(", ", featuresForHRURequest.Select(x=>x.Attributes.QueryFeatureID.ToString()))}");
                logger.Warn(rawResponse);
            }

            return newHRUCharacteristics;
        }

        public static EsriGPRecordSetLayer<HRURequestFeature> GetGPRecordSetLayer(
            List<HRURequestFeature> features)
        {
            return new EsriGPRecordSetLayer<HRURequestFeature>
            {
                Features = features,
                DisplayFieldName = "",
                GeometryType = "esriGeometryPolygon",
                ExceededTransferLimit = "false",
                SpatialReference = new EsriSpatialReference { wkid = 102646, latestWkid = 2230},
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
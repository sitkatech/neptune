using Microsoft.Extensions.Logging;
using Neptune.Common.GeoSpatial;
using Neptune.Jobs.EsriAsynchronousJob;

namespace Neptune.Jobs.Services
{
    public static class HRUUtilities
    {
        public static IEnumerable<HRUResponseFeature> RetrieveHRUResponseFeatures(
            List<HRURequestFeature> featuresForHRURequest, ILogger logger)
        {
            var postUrl = "";//todo:NeptuneConfiguration.HRUServiceBaseUrl;
            var esriAsynchronousJobRunner = new EsriAsynchronousJobRunner(postUrl, "output_table");

            var hruRequest = GetGPRecordSetLayer(featuresForHRURequest);

            var serializeObject = new
            {
                input_fc = GeoJsonSerializer.Serialize(hruRequest),
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
                logger.LogWarning(ex.Message, ex);
                logger.LogWarning($"Skipped entities (ProjectLGUs if Project modeling, otherwise LGUs) with these IDs: {string.Join(", ", featuresForHRURequest.Select(x=>x.Attributes.QueryFeatureID.ToString()))}");
                logger.LogWarning(rawResponse);
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
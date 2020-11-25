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
        public static IEnumerable<HRUCharacteristic> RetrieveHRUCharacteristics(
            List<LoadGeneratingUnit> loadGeneratingUnits, ILog logger)
        {
            var postUrl = NeptuneWebConfiguration.HRUServiceBaseUrl;
            var esriAsynchronousJobRunner = new EsriAsynchronousJobRunner(postUrl, "output_fc");

            var hruRequest = GetGPRecordSetLayer(loadGeneratingUnits);

            var serializeObject = new
            {
                input_fc = JsonConvert.SerializeObject(hruRequest),
                returnZ = false,
                returnM = false,
                returnTrueCurves = false,
                f = "pjson"
            };

            var newHRUCharacteristics = new List<HRUCharacteristic>();

            try
            {
                var esriGPRecordSetLayer = esriAsynchronousJobRunner
                .RunJob<EsriAsynchronousJobOutputParameter<EsriGPRecordSetLayer<HRUResponseFeature>>>(
                    // ReSharper disable once UnusedVariable
                    serializeObject, out var rawResponse).Value;

                newHRUCharacteristics.AddRange(
                    esriGPRecordSetLayer
                        .Features
                        .Select(x =>
                        {
                            var hruCharacteristic = x.ToHRUCharacteristic();
                            return hruCharacteristic;
                        }));

            }
            catch (Exception ex)
            {
                logger.Warn(ex.Message, ex);
                logger.Warn($"Skipped LGUs with these IDs: {string.Join(", ", loadGeneratingUnits.Select(x=>x.LoadGeneratingUnitID.ToString()))}");
            }

            return newHRUCharacteristics;
        }

        public static EsriGPRecordSetLayer<HRURequestFeature> GetGPRecordSetLayer(
            IEnumerable<LoadGeneratingUnit> loadGeneratingUnits)
        {
            return new EsriGPRecordSetLayer<HRURequestFeature>
            {
                Features = loadGeneratingUnits.GetHRURequestFeatures().ToList(),
                GeometryType = "esriGeometryPolygon",
                ExceededTransferLimit = "false",
                SpatialReference = new EsriSpatialReference { wkid = CoordinateSystemHelper.NAD_83_CA_ZONE_VI_SRID },
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
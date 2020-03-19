using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ApprovalUtilities.SimpleLogger;
using ApprovalUtilities.Utilities;
using log4net;
using LtInfo.Common;
using LtInfo.Common.DesignByContract;
using Neptune.Web.Common.EsriAsynchronousJob;
using Neptune.Web.Models;
using Newtonsoft.Json;

namespace Neptune.Web.Common
{
    public static class HRUUtilities
    {
        // todo: determine whether this is coming back or not
        //public static void RetrieveAndSaveHRUCharacteristics(IHaveHRUCharacteristics iHaveHRUCharacteristics, Func<HRUCharacteristic, int?> primaryKeySetterAction)
        //{
        //    Check.Assert(iHaveHRUCharacteristics.GetCatchmentGeometry() != null, "Entity must have a catchment geometry to calculate HRU Characteristics.");
        //    var postUrl = NeptuneWebConfiguration.HRUServiceBaseUrl;
        //    var esriAsynchronousJobRunner = new EsriAsynchronousJobRunner(postUrl, "HRU_Composite");

        //    var hruRequest = GetGPRecordSetLayer(iHaveHRUCharacteristics);

        //    var serializeObject = new
        //    {
        //        Input_Polygons = JsonConvert.SerializeObject(hruRequest),
        //        returnZ = false,
        //        returnM = false,
        //        returnTrueCurves = false,
        //        f = "pjson"
        //    };

        //    HttpRequestStorage.DatabaseEntities.HRUCharacteristics.DeleteHRUCharacteristic(iHaveHRUCharacteristics.GetHRUCharacteristics().ToList());
        //    HttpRequestStorage.DatabaseEntities.SaveChanges();
        //    var hruCharacteristics =
        //        esriAsynchronousJobRunner
        //            .RunJob<EsriAsynchronousJobOutputParameter<EsriGPRecordSetLayer<HRUResponseFeature>>>(serializeObject).Value
        //            .Features.Select(x =>
        //            {
        //                var hruCharacteristic = x.ToHRUCharacteristic();
        //                primaryKeySetterAction.Invoke(hruCharacteristic);
        //                return hruCharacteristic;
        //            });
        //    iHaveHRUCharacteristics.HRUCharacteristics.AddAll(hruCharacteristics);
        //    HttpRequestStorage.DatabaseEntities.SaveChanges();
        //}

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


        // TODO: eventually this needs to save, and then it needs to be renamed to drop the word "Not" from the name
        public static IEnumerable<HRUCharacteristic> RetrieveHRUCharacteristics(
            List<LoadGeneratingUnit> loadGeneratingUnits,
            DatabaseEntities dbContext, ILog logger)
        {
            var postUrl = NeptuneWebConfiguration.HRUServiceBaseUrl;
            var esriAsynchronousJobRunner = new EsriAsynchronousJobRunner(postUrl, "HRU_Composite");

            var hruRequest = GetGPRecordSetLayer(loadGeneratingUnits);

            var serializeObject = new
            {
                Input_Polygons = JsonConvert.SerializeObject(hruRequest),
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
                Logger.Warning(ex.Message);
                Logger.Warning($"Skipped LGUs with these IDs: {string.Join(", ", loadGeneratingUnits.Select(x=>x.LoadGeneratingUnitID.ToString()))}");
                //throw new EsriAsynchronousJobUnknownErrorException(
                //    $"Esri job succeeded, but results were not usable. Content retrieved is:\n {rawResponse}", ex);
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
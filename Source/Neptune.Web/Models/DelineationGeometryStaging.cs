﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using GeoJSON.Net;
using GeoJSON.Net.Feature;
using LtInfo.Common;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.GdalOgr;
using LtInfo.Common.GeoJson;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public partial class DelineationGeometryStaging : IAuditableEntity
    {
        public static List<DelineationGeometryStaging> CreateDelineationGeometryStagingListFromGdb(FileInfo gdbFile, Person currentPerson)
        {
            var ogr2OgrCommandLineRunner = new Ogr2OgrCommandLineRunner(NeptuneWebConfiguration.Ogr2OgrExecutable,
                Ogr2OgrCommandLineRunner.DefaultCoordinateSystemId,
                NeptuneWebConfiguration.HttpRuntimeExecutionTimeout.TotalMilliseconds);

            var geoJsons =
                OgrInfoCommandLineRunner.GetFeatureClassNamesFromFileGdb(new FileInfo(NeptuneWebConfiguration.OgrInfoExecutable), gdbFile, Ogr2OgrCommandLineRunner.DefaultTimeOut)
                    .ToDictionary(x => x, x => ogr2OgrCommandLineRunner.ImportFileGdbToGeoJson(gdbFile, x, false))
                    .Where(x => IsUsableFeatureCollectionGeoJson(JsonTools.DeserializeObject<FeatureCollection>(x.Value)))
                    .ToDictionary(x => x.Key, x => new FeatureCollection(JsonTools.DeserializeObject<FeatureCollection>(x.Value).Features.Where(IsUsableFeatureGeoJson).ToList()).ToGeoJsonString());

            Check.Assert(geoJsons.Count != 0, "Number of usable Feature Classes in uploaded file must be greater than 0.");

            return geoJsons.Select(x => new DelineationGeometryStaging(currentPerson, x.Key, x.Value, true)).ToList();
        }

        public string GetAuditDescriptionString()
        {
            return $"Delineation geometry staging {DelineationGeometryStagingID}";
        }

        public FeatureCollection ToGeoJsonFeatureCollection()
        {
            return JsonTools.DeserializeObject<FeatureCollection>(DelineationGeometryStagingGeoJson);
        }

        public static bool IsUsableFeatureCollectionGeoJson(FeatureCollection featureCollection)
        {
            return featureCollection.Features.Any(IsUsableFeatureGeoJson);
        }

        public static bool IsUsableFeatureGeoJson(Feature feature)
        {
            return new List<GeoJSONObjectType> {GeoJSONObjectType.Polygon, GeoJSONObjectType.MultiPolygon}.Contains(feature.Geometry.Type);
        }
    }
}
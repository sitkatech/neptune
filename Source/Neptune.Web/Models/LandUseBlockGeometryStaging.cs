using System.Collections.Generic;
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
    public partial class LandUseBlockGeometryStaging : IAuditableEntity
    {
        public static List<LandUseBlockGeometryStaging> CreateLandUseBlockGeometryStagingListFromGdb(FileInfo gdbFile, Person currentPerson)
        {
            var ogr2OgrCommandLineRunner = new Ogr2OgrCommandLineRunner(NeptuneWebConfiguration.Ogr2OgrExecutable,
                Ogr2OgrCommandLineRunner.DefaultCoordinateSystemId,
                NeptuneWebConfiguration.HttpRuntimeExecutionTimeout.TotalMilliseconds*10);

            var geoJsons =
                OgrInfoCommandLineRunner.GetFeatureClassNamesFromFileGdb(new FileInfo(NeptuneWebConfiguration.OgrInfoExecutable), gdbFile, Ogr2OgrCommandLineRunner.DefaultTimeOut)
                    .ToDictionary(x => x, x => ogr2OgrCommandLineRunner.ImportFileGdbToGeoJson(gdbFile, x, false))
                    .Where(x => IsUsableFeatureCollectionGeoJson(JsonTools.DeserializeObject<FeatureCollection>(x.Value)))
                    .ToDictionary(x => x.Key, x => new FeatureCollection(JsonTools.DeserializeObject<FeatureCollection>(x.Value).Features.Where(IsUsableFeatureGeoJson).ToList()).ToGeoJsonString());

            Check.Assert(geoJsons.Count != 0, "Number of usable Feature Classes in uploaded file must be greater than 0.");

            return geoJsons.Select(x => new LandUseBlockGeometryStaging(currentPerson, x.Key, x.Value, true)).ToList();
        }

        public string GetAuditDescriptionString()
        {
            return $"Land Use Block geometry staging {LandUseBlockGeometryStagingID}";
        }

        public FeatureCollection ToGeoJsonFeatureCollection()
        {
            return JsonTools.DeserializeObject<FeatureCollection>(LandUseBlockGeometryStagingGeoJson);
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
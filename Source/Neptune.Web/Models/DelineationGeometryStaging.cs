using System.Collections.Generic;
using System.Linq;
using GeoJSON.Net;
using GeoJSON.Net.Feature;

namespace Neptune.Web.Models
{
    public partial class DelineationGeometryStaging 
    {
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
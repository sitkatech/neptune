using LtInfo.Common.GeoJson;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;

namespace Neptune.Web.Models
{
    public static class DelineationGeometryStagingModelExtensions
    {

        public static GeoJSON.Net.Feature.FeatureCollection ToGeoJsonFeatureCollection(this DelineationGeometryStaging delineationGeometryStaging)
        {
            var delineationGeometryStagings = new List<DelineationGeometryStaging> { delineationGeometryStaging };
            return ToGeoJsonFeatureCollection(delineationGeometryStagings);
        }

        public static GeoJSON.Net.Feature.FeatureCollection ToGeoJsonFeatureCollection(this IEnumerable<DelineationGeometryStaging> delineationGeometryStagings)
        {
            var featureCollection = new GeoJSON.Net.Feature.FeatureCollection();
            featureCollection.Features.AddRange(delineationGeometryStagings.Select(x =>
            {
                var feature = DbGeometryToGeoJsonHelper.FromDbGeometryWithReprojectionCheck(DbGeometry.FromText(""));
                
                feature.Properties.Add("FeatureWeight", 1);
                feature.Properties.Add("FillPolygon", true);
                feature.Properties.Add("FeatureColor", "#405d74");
                feature.Properties.Add("FillOpacity", "0.2");
                return feature;
            }));
            return featureCollection;
        }
    }
}
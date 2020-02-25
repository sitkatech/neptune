using LtInfo.Common;
using LtInfo.Common.DbSpatial;
using LtInfo.Common.GeoJson;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GeoJSON.Net.Feature;

namespace Neptune.Web.Models
{
    public static class DelineationModelExtensions
    {
        public static string GetDelineationAreaString(this Delineation delineation)
        {
            return (delineation?.DelineationGeometry.Area * DbSpatialHelper.SquareMetersToAcres)?.ToString("0.00") ?? "-";
        }

        public static readonly UrlTemplate<int> DeleteUrlTemplate =
            new UrlTemplate<int>(
                SitkaRoute<DelineationController>.BuildUrlFromExpression(t => t.Delete(UrlTemplate.Parameter1Int)));

        public static string GetDeleteUrl(this Delineation delineation)
        {
            return DeleteUrlTemplate.ParameterReplace(delineation.DelineationID);
        }

        public static string GetDetailUrl(this Delineation delineation)
        {
            return delineation.TreatmentBMP.GetDelineationMapUrl();
        }

        public static HtmlString GetDetailUrlForGrid(this Delineation delineation)
        {
            return UrlTemplate.MakeHrefString(GetDetailUrl(delineation), "View", new Dictionary<string, string>{{"class", "gridButton"}});
        }

        public static GeoJSON.Net.Feature.FeatureCollection ToGeoJsonFeatureCollection(this IEnumerable<Delineation> delineationGeometryStagings)
        {
            var featureCollection = new GeoJSON.Net.Feature.FeatureCollection();
            featureCollection.Features.AddRange(delineationGeometryStagings.Where(x => x?.DelineationGeometry != null).Select(x =>
            {
                var feature = DbGeometryToGeoJsonHelper.FromDbGeometryWithReprojectionCheck(x.DelineationGeometry);
                feature.Properties.Add("DelineationID", x.DelineationID);
                feature.Properties.Add("Name", x.DelineationID);
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

using System.Collections.Generic;
using System.Linq;
using GeoJSON.Net.Feature;
using LtInfo.Common.GeoJson;
using Neptune.Web.Models;

static internal class OnlandVisualTrashAssessmentAreaModelExtensions
{
    public static FeatureCollection ToGeoJsonFeatureCollection(this IEnumerable<OnlandVisualTrashAssessmentArea> areas)
    {
        var featureCollection = new FeatureCollection();
        featureCollection.Features.AddRange(areas.Select(x =>
        {
            var feature = DbGeometryToGeoJsonHelper.FromDbGeometry(x.OnlandVisualTrashAssessmentAreaGeometry);
            feature.Properties.Add("AreaID", x.OnlandVisualTrashAssessmentAreaID);
            feature.Properties.Add("FeatureColor", "#FF00FF");
            feature.Properties.Add("FeatureGlyph", "water"); // todo?????
            return feature;
        }));
        return featureCollection;
    }
}
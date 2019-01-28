using System.Collections.Generic;
using System.Linq;
using GeoJSON.Net.Feature;
using LtInfo.Common.GeoJson;
using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public static class OnlandVisualTrashAssessmentAreaModelExtensions
    {
        public static FeatureCollection ToGeoJsonFeatureCollection(this IEnumerable<OnlandVisualTrashAssessmentArea> areas)
        {
            var featureCollection = new FeatureCollection();
            featureCollection.Features.AddRange(areas.Select(x =>
            {
                var feature = DbGeometryToGeoJsonHelper.FromDbGeometry(x.OnlandVisualTrashAssessmentAreaGeometry);
                feature.Properties.Add("OnlandVisualTrashAssessmentAreaID", x.OnlandVisualTrashAssessmentAreaID);
                return feature;
            }));
            return featureCollection;
        }
    }
}
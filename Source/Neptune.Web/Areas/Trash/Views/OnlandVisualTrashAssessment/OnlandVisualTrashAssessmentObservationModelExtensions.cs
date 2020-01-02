using System.Collections.Generic;
using System.Linq;
using GeoJSON.Net.Feature;
using LtInfo.Common.GeoJson;
using Neptune.Web.Models;

public static class OnlandVisualTrashAssessmentObservationModelExtensions
{
    public static FeatureCollection ToGeoJsonFeatureCollection(this IEnumerable<OnlandVisualTrashAssessmentObservation> observations)
    {
        var featureCollection = new FeatureCollection();
        featureCollection.Features.AddRange(observations.Select(x =>
        {
            var feature = DbGeometryToGeoJsonHelper.FromDbGeometryWithReprojectionCheck(x.LocationPoint);
            feature.Properties.Add("ObservationID", x.OnlandVisualTrashAssessmentObservationID);
            feature.Properties.Add("FeatureColor", "#FF00FF");
            feature.Properties.Add("FeatureGlyph", "water"); // todo?????
            return feature;
        }));
        return featureCollection;
    }

    public static LayerGeoJson MakeObservationsLayerGeoJson(this IEnumerable<OnlandVisualTrashAssessmentObservation> observations)
    {
        var featureCollection = observations.ToGeoJsonFeatureCollection();
        var observationsLayerGeoJson = new LayerGeoJson("Observations", featureCollection, "#FF00FF", 1, LayerInitialVisibility.Show) { EnablePopups = false };
        return observationsLayerGeoJson;
    }
}
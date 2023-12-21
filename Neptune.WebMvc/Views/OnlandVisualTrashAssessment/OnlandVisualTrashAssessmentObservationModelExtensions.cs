using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using NetTopologySuite.Features;

public static class OnlandVisualTrashAssessmentObservationModelExtensions
{
    public static FeatureCollection ToGeoJsonFeatureCollection(this IEnumerable<OnlandVisualTrashAssessmentObservation> observations)
    {
        var featureCollection = new FeatureCollection();
        foreach (var observation in observations)
        {
            var attributesTable = new AttributesTable
            {
                { "ObservationID", observation.OnlandVisualTrashAssessmentObservationID },
                { "FeatureColor", "#FF00FF" }
            };
            var feature = new Feature(observation.LocationPoint4326, attributesTable);
            featureCollection.Add(feature);
        }
        return featureCollection;
    }

    public static LayerGeoJson MakeObservationsLayerGeoJson(this IEnumerable<OnlandVisualTrashAssessmentObservation> observations)
    {
        var featureCollection = observations.ToGeoJsonFeatureCollection();
        var observationsLayerGeoJson = new LayerGeoJson("Observations", featureCollection, "#FF00FF", 1, LayerInitialVisibility.Show) { EnablePopups = false };
        return observationsLayerGeoJson;
    }
}
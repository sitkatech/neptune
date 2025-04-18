using Microsoft.EntityFrameworkCore;
using Neptune.Common.GeoSpatial;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Views.OnlandVisualTrashAssessment;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;

namespace Neptune.WebMvc.Models
{
    public static class OnlandVisualTrashAssessmentModelExtensions
    {
        public static string ToBaselineProgress(this OnlandVisualTrashAssessment onlandVisualTrashAssessment)
        {
           return onlandVisualTrashAssessment.IsProgressAssessment ? "Progress" : "Baseline";
        }

        public static Geometry GetTransect(this OnlandVisualTrashAssessment ovta)
        {
            if (ovta.OnlandVisualTrashAssessmentObservations.Count > 1)
            {
                var points = string.Join(",",
                    ovta.OnlandVisualTrashAssessmentObservations.OrderBy(x => x.ObservationDatetime)
                        .Select(x => x.LocationPoint).ToList().Select(x => $"{x.Coordinate.X} {x.Coordinate.Y}")
                        .ToList());

                var linestring = $"LINESTRING ({points})";

                // the transect is going to be in 2771 because it was generated from points in 2771
                return GeometryHelper.FromWKT(linestring, Proj4NetHelper.NAD_83_HARN_CA_ZONE_VI_SRID);
            }

            return null;
        }

        public static Geometry GetTransect4326(this OnlandVisualTrashAssessment ovta)
        {
            if (ovta.OnlandVisualTrashAssessmentObservations.Count > 1)
            {
                var points = string.Join(",",
                    ovta.OnlandVisualTrashAssessmentObservations.Where(x => x.LocationPoint4326 != null).OrderBy(x => x.ObservationDatetime)
                        .Select(x => x.LocationPoint4326).ToList().Select(x => $"{x.Coordinate.X} {x.Coordinate.Y}")
                        .ToList());

                var linestring = $"LINESTRING ({points})";

                // the transect is going to be in 2771 because it was generated from points in 2771
                return GeometryHelper.FromWKT(linestring, Proj4NetHelper.WEB_MERCATOR);
            }

            return null;
        }

        public static List<PreliminarySourceIdentificationSimple> GetPreliminarySourceIdentificationSimples(
            this OnlandVisualTrashAssessment onlandVisualTrashAssessment)
        {
            var presentGuys = onlandVisualTrashAssessment
                .OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypes.ToList()
                .Select(x => new PreliminarySourceIdentificationSimple(x)).ToList();

            var missingGuys = PreliminarySourceIdentificationType.All.Except(onlandVisualTrashAssessment.OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypes.Select(x => x.PreliminarySourceIdentificationType)).Select(x => new PreliminarySourceIdentificationSimple(x));
            presentGuys.AddRange(missingGuys);

            return presentGuys;
        }

        public static LayerGeoJson MakeAssessmentAreasLayerGeoJson(this IEnumerable<OnlandVisualTrashAssessmentArea> onlandVisualTrashAssessmentAreas)
        {
            var featureCollection = onlandVisualTrashAssessmentAreas.ToGeoJsonFeatureCollection();
            var observationsLayerGeoJson = new LayerGeoJson("Observations", featureCollection, "#FF00FF", 1, LayerInitialVisibility.Show) { EnablePopups = false };
            return observationsLayerGeoJson;
        }

        public static LayerGeoJson GetTransectLineLayerGeoJson(this OnlandVisualTrashAssessment onlandVisualTrashAssessment)
        {
            LayerGeoJson transectLineLayerGeoJson;

            if (onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea?.TransectLine4326 != null)
            {
                transectLineLayerGeoJson = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea
                    .GetTransectLineLayerGeoJson();
            }
            else
            {
                var featureCollection = new FeatureCollection();
                var geometry = onlandVisualTrashAssessment.GetTransect4326();
                if (geometry == null)
                {
                    return null;
                }

                var feature = new Feature(geometry, new AttributesTable());
                featureCollection.Add(feature);
                transectLineLayerGeoJson = new LayerGeoJson("transectLine", featureCollection, "#000000", 1, LayerInitialVisibility.Show);
            }

            return transectLineLayerGeoJson;
        }

        public static LayerGeoJson GetAssessmentAreaLayerGeoJson(this OnlandVisualTrashAssessment onlandVisualTrashAssessment, bool reduce)
        {
            FeatureCollection geoJsonFeatureCollection;
            if (onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea != null)
            {
                geoJsonFeatureCollection =
                    new List<OnlandVisualTrashAssessmentArea> { onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea }
                        .ToGeoJsonFeatureCollection();
            }
            else if (onlandVisualTrashAssessment.DraftGeometry != null)
            {
                var draftGeometry = onlandVisualTrashAssessment.DraftGeometry.ProjectTo4326();
                geoJsonFeatureCollection = draftGeometry.MultiPolygonToFeatureCollection();
            }
            else
            {
                geoJsonFeatureCollection = new FeatureCollection();
            }

            var assessmentAreaLayerGeoJson = new LayerGeoJson("parcels", geoJsonFeatureCollection, "#ffff00", 0.5f, LayerInitialVisibility.Show);
            return assessmentAreaLayerGeoJson;
        }
    }
}

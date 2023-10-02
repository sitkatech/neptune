using System.Drawing;
using Neptune.Common;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using NetTopologySuite.Features;

namespace Neptune.Web.Models
{
    public static class DelineationModelExtensions
    {
        public static double? GetDelineationArea(this Delineation? delineation)
        {
            return delineation?.DelineationGeometry.Area != null
                ? Math.Round(delineation.DelineationGeometry.Area * Constants.SquareMetersToAcres, 2)
                : null;
        }

        public static string GetDelineationAreaString(this Delineation? delineation)
        {
            if (delineation == null)
            {
                return "-";
            }
            var delineationAcresAsString = (delineation.DelineationGeometry.Area * Constants.SquareMetersToAcres).ToString("0.00");
            return $"{delineationAcresAsString} ac";
        }

        public static string GetDelineationStatus(this Delineation? delineation)
        {
            return delineation != null ? delineation.IsVerified ? "Verified" : "Provisional" : "None";
        }

        public static FeatureCollection ToGeoJsonFeatureCollection(this IEnumerable<Delineation> delineationGeometryStagings)
        {
            var featureCollection = new FeatureCollection();
            foreach (var x in delineationGeometryStagings.Where(x => x?.DelineationGeometry != null))
            {
                var attributesTable = new AttributesTable
                {
                    { "DelineationID", x.DelineationID },
                    { "Name", x.DelineationID },
                    { "FeatureWeight", 1 },
                    { "FillPolygon", true },
                    { "FeatureColor", GetColorString("blue") },
                    { "FillOpacity", "0.2" }
                };
                var feature = new Feature(x.DelineationGeometry4326, attributesTable);
                featureCollection.Add(feature);
            }
            return featureCollection;
        }

        private static string GetColorString(string colorName)
        {
            var color = Color.FromName(colorName);
            return $"#{color.R:x2}{color.G:x2}{color.B:x2}";
        }

        /// <summary>
        /// The preference over delete-full for delineation. Nulls the DelineationID on any LGUs,
        /// deletes any overlaps, deletes the delineation itself, and marks the delineation's
        /// TreatmentBMP for a model run.
        /// </summary>
        /// <param name="treatmentBMPDelineation"></param>
        /// <param name="dbContext"></param>
        public static async Task DeleteDelineation(this Delineation treatmentBMPDelineation, NeptuneDbContext dbContext)
        {
            var treatmentBMP = treatmentBMPDelineation.TreatmentBMP;

            foreach (var delineationLoadGeneratingUnit in treatmentBMPDelineation.LoadGeneratingUnits)
            {
                delineationLoadGeneratingUnit.DelineationID = null;
            }

            await dbContext.SaveChangesAsync();
            dbContext.DelineationOverlaps.RemoveRange(treatmentBMPDelineation.DelineationOverlapDelineations);
            dbContext.DelineationOverlaps.RemoveRange(treatmentBMPDelineation.DelineationOverlapOverlappingDelineations);
            dbContext.Delineations.Remove(treatmentBMPDelineation);
            await dbContext.SaveChangesAsync();

            await NereidUtilities.MarkTreatmentBMPDirty(treatmentBMP, dbContext);
        }
    }
}

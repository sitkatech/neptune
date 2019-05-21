using System;
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DbSpatial;
using Neptune.Web.Models;

namespace Neptune.Web.Common
{
    public static class AreaCalculationsHelper
    {
        private const int DecimalPlacesToDisplay = 0;

        public static double GetArea(this IEnumerable<TrashGeneratingUnit> trashGeneratingUnits)
        {
            return Math.Round(trashGeneratingUnits
                .Select(x => x.TrashGeneratingUnitGeometry.Area * DbSpatialHelper.SqlGeometryAreaToAcres).Sum().GetValueOrDefault(), DecimalPlacesToDisplay); // will never be null
        }

        public static double GetAreaLoadBased(this IQueryable<TrashGeneratingUnit> trashGeneratingUnits)
        {
            var loadOfTrashGeneratingUnitsWithNoBaselineAssessmentScore = trashGeneratingUnits.ToList()
                .Where(x => x.OnlandVisualTrashAssessmentArea == null || x.OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentBaselineScore == null).Select(
                    x => x.TrashGeneratingUnitGeometry.Area * DbSpatialHelper.SqlGeometryAreaToAcres *
                         (double)x.LandUseBlock.TrashGenerationRate).Sum().GetValueOrDefault();

            var loadOfTrashGeneratingUnitsWithBaselineAssessmentScore = trashGeneratingUnits.Where(x =>
                    x.OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentBaselineScoreID != null).ToList()
                .Select(x => x.TrashGeneratingUnitGeometry.Area * DbSpatialHelper.SqlGeometryAreaToAcres * (double)
                             (x.LandUseBlock.TrashGenerationRate >= x.OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentBaselineScore.TrashGenerationRate ? x.LandUseBlock.TrashGenerationRate : x.OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentBaselineScore.TrashGenerationRate)).Sum().GetValueOrDefault();

            return Math.Round(loadOfTrashGeneratingUnitsWithBaselineAssessmentScore + loadOfTrashGeneratingUnitsWithNoBaselineAssessmentScore, DecimalPlacesToDisplay); // will never be null
        }

        public static double GetAreaPartialCaptureLoadBased(this IEnumerable<TrashGeneratingUnit> trashGeneratingUnits)
        {
            var loadOfTrashGeneratingUnitsWithNoBaselineAssessmentScore = trashGeneratingUnits.ToList().Where(x => x.OnlandVisualTrashAssessmentArea == null || x.OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentBaselineScore == null)
                .Select(x =>
                    (double)x.TrashGeneratingUnitGeometry.Area * DbSpatialHelper.SqlGeometryAreaToAcres *
                    (double)OnlandVisualTrashAssessmentScore.A.TrashGenerationRate).Sum();

            var loadOfTrashGeneratingUnitsWithBaselineAssessmentScore = trashGeneratingUnits.Where(x => x.OnlandVisualTrashAssessmentArea != null &&
                                                                                                        x.OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentBaselineScore != null).ToList().Select(x =>
                    x.TrashGeneratingUnitGeometry.Area * DbSpatialHelper.SqlGeometryAreaToAcres *
                    (double)(OnlandVisualTrashAssessmentScore.A.TrashGenerationRate >=
                             (1 - x.TreatmentBMP.TrashCaptureEffectiveness.Value / 100) *
                             x.OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentBaselineScore
                                 .TrashGenerationRate
                        ? OnlandVisualTrashAssessmentScore.A.TrashGenerationRate
                        : (1 - x.TreatmentBMP.TrashCaptureEffectiveness.Value / 100) *
                          x.OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentBaselineScore
                              .TrashGenerationRate))
                .Sum().GetValueOrDefault();

            return Math.Round(
                loadOfTrashGeneratingUnitsWithNoBaselineAssessmentScore +
                loadOfTrashGeneratingUnitsWithBaselineAssessmentScore, DecimalPlacesToDisplay);
        }

        public static double GetAreaBaselineScoreLoadBased(this IEnumerable<OnlandVisualTrashAssessmentArea> ovtaas)
        {

            return Math.Round(ovtaas.ToList()
                .Select(x =>
                    x.OnlandVisualTrashAssessmentAreaGeometry.Area * DbSpatialHelper.SqlGeometryAreaToAcres *
                    (double)x.OnlandVisualTrashAssessmentBaselineScore.TrashGenerationRate).Sum().GetValueOrDefault(), DecimalPlacesToDisplay);
        }

        public static double GetAreaProgressScoreLoadBased(this IEnumerable<OnlandVisualTrashAssessmentArea> ovtaas)
        {

            return Math.Round(ovtaas.ToList()
                .Select(x =>
                    x.OnlandVisualTrashAssessmentAreaGeometry.Area * DbSpatialHelper.SqlGeometryAreaToAcres *
                    (double)x.OnlandVisualTrashAssessmentBaselineScore.TrashGenerationRate).Sum().GetValueOrDefault(), DecimalPlacesToDisplay);
        }
    }
}
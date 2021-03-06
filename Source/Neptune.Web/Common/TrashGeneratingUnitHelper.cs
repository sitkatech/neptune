﻿using LtInfo.Common.DbSpatial;
using Neptune.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace Neptune.Web.Common
{
    public static partial class TrashGeneratingUnitHelper
    {
        private const decimal FullTrashCaptureLoading = 2.5m;

        public static double LoadBasedFullCapture(StormwaterJurisdiction jurisdiction)
        {
            var vTrashGeneratingUnitLoadStatistics = HttpRequestStorage.DatabaseEntities.vTrashGeneratingUnitLoadStatistics.Where(x =>
                x.StormwaterJurisdictionID == jurisdiction.StormwaterJurisdictionID
                && x.IsFullTrashCapture );

            return vTrashGeneratingUnitLoadStatistics.Any()
                ? vTrashGeneratingUnitLoadStatistics.Sum(x =>
                    x.Area * (double) (x.BaselineLoadingRate - FullTrashCaptureLoading) *
                    DbSpatialHelper.SquareMetersToAcres)
                : 0;
        }

        public static double LoadBasedPartialCapture(StormwaterJurisdiction jurisdiction)
        {
            var vTrashGeneratingUnitLoadStatistics = HttpRequestStorage.DatabaseEntities.vTrashGeneratingUnitLoadStatistics.Where(x =>
                x.StormwaterJurisdictionID == jurisdiction.StormwaterJurisdictionID && x.IsPartialTrashCapture);

            return vTrashGeneratingUnitLoadStatistics.Any()
                ? vTrashGeneratingUnitLoadStatistics.Sum(x =>
                    x.Area * (double) (x.BaselineLoadingRate - x.CurrentLoadingRate) *
                    DbSpatialHelper.SquareMetersToAcres)
                : 0;
        }

        public static double LoadBasedOVTAProgressScores(StormwaterJurisdiction jurisdiction)
        {
            var vTrashGeneratingUnitLoadStatistics =
                HttpRequestStorage.DatabaseEntities.vTrashGeneratingUnitLoadStatistics.Where(x =>
                    x.StormwaterJurisdictionID == jurisdiction.StormwaterJurisdictionID
                    && x.HasBaselineScore == true && x.HasProgressScore == true);

            return vTrashGeneratingUnitLoadStatistics.Any()
                ? vTrashGeneratingUnitLoadStatistics.Sum(x =>
                    x.Area * (double) (x.BaselineLoadingRate - x.ProgressLoadingRate) *
                    DbSpatialHelper.SquareMetersToAcres)
                : 0;
        }

        // done
        public static double TargetLoadReduction(StormwaterJurisdiction jurisdiction)
        {
            var vTrashGeneratingUnitLoadStatistics = HttpRequestStorage.DatabaseEntities.vTrashGeneratingUnitLoadStatistics.Where(x =>
                x.StormwaterJurisdictionID == jurisdiction.StormwaterJurisdictionID && x.PriorityLandUseTypeID != PriorityLandUseType.ALU.PriorityLandUseTypeID);

            return vTrashGeneratingUnitLoadStatistics.Any()
                ? vTrashGeneratingUnitLoadStatistics.Sum(x =>
                    x.Area * (double) (x.BaselineLoadingRate - FullTrashCaptureLoading) *
                    DbSpatialHelper.SquareMetersToAcres)
                : 0;
        }


        public static double EquivalentAreaAcreage(this List<TrashGeneratingUnit> trashGeneratingUnits)
        {
            return trashGeneratingUnits.Where(x =>
                x.OnlandVisualTrashAssessmentArea?.OnlandVisualTrashAssessmentBaselineScoreID ==
                OnlandVisualTrashAssessmentScore.A.OnlandVisualTrashAssessmentScoreID &&
                !x.IsFullTrashCapture &&
                // This is how to check "PLU == true"
                x.LandUseBlock.PriorityLandUseTypeID != PriorityLandUseType.ALU.PriorityLandUseTypeID
            ).GetArea();
        }

        public static double FullTrashCaptureAcreage(this List<TrashGeneratingUnit> trashGeneratingUnits)
        {
            return trashGeneratingUnits.Where(x =>
                x.IsFullTrashCapture &&
                // This is how to check "PLU == true"
                x.LandUseBlock.PriorityLandUseTypeID != PriorityLandUseType.ALU.PriorityLandUseTypeID
            ).GetArea();
        }

        // OVTA-based calculations

        public static double AlternateOVTAScoreDAcreage(this List<TrashGeneratingUnit> trashGeneratingUnits)
        {
            return GetAlternativeOVTAScoreAcreageImpl(trashGeneratingUnits, OnlandVisualTrashAssessmentScore.D);
        }

        public static double AlternateOVTAScoreBAcreage(this List<TrashGeneratingUnit> trashGeneratingUnits)
        {
            return GetAlternativeOVTAScoreAcreageImpl(trashGeneratingUnits, OnlandVisualTrashAssessmentScore.B);
        }

        public static double PriorityOVTAScoreDAcreage(this List<TrashGeneratingUnit> trashGeneratingUnits)
        {
            return GetPriorityOVTAScoreAcreageImpl(trashGeneratingUnits, OnlandVisualTrashAssessmentScore.D);
        }

        public static double PriorityOVTAScoreBAcreage(this List<TrashGeneratingUnit> trashGeneratingUnits)
        {
            return GetPriorityOVTAScoreAcreageImpl(trashGeneratingUnits, OnlandVisualTrashAssessmentScore.B);
        }

        public static double AlternateOVTAScoreCAcreage(this List<TrashGeneratingUnit> trashGeneratingUnits)
        {
            return GetAlternativeOVTAScoreAcreageImpl(trashGeneratingUnits, OnlandVisualTrashAssessmentScore.C);
        }

        public static double AlternateOVTAScoreAAcreage(this List<TrashGeneratingUnit> trashGeneratingUnits)
        {
            return GetAlternativeOVTAScoreAcreageImpl(trashGeneratingUnits, OnlandVisualTrashAssessmentScore.A);
        }

        public static double PriorityOVTAScoreCAcreage(this List<TrashGeneratingUnit> trashGeneratingUnits)
        {
            return GetPriorityOVTAScoreAcreageImpl(trashGeneratingUnits, OnlandVisualTrashAssessmentScore.C);
        }

        public static double PriorityOVTAScoreAAcreage(this List<TrashGeneratingUnit> trashGeneratingUnits)
        {
            return GetPriorityOVTAScoreAcreageImpl(trashGeneratingUnits, OnlandVisualTrashAssessmentScore.A);
        }

        private static double GetAlternativeOVTAScoreAcreageImpl(List<TrashGeneratingUnit> trashGeneratingUnits,
            OnlandVisualTrashAssessmentScore onlandVisualTrashAssessmentScore)
        {
            return trashGeneratingUnits.Where(x =>
                x.OnlandVisualTrashAssessmentArea?.OnlandVisualTrashAssessmentBaselineScoreID ==
                onlandVisualTrashAssessmentScore.OnlandVisualTrashAssessmentScoreID &&
                // This is how to check "PLU == true"
                x.LandUseBlock.PriorityLandUseTypeID == PriorityLandUseType.ALU.PriorityLandUseTypeID).GetArea();
        }

        private static double GetPriorityOVTAScoreAcreageImpl(List<TrashGeneratingUnit> trashGeneratingUnits,
            OnlandVisualTrashAssessmentScore onlandVisualTrashAssessmentScore)
        {
            return trashGeneratingUnits.Where(x =>
                x.OnlandVisualTrashAssessmentArea?.OnlandVisualTrashAssessmentBaselineScoreID ==
                onlandVisualTrashAssessmentScore.OnlandVisualTrashAssessmentScoreID &&
                // This is how to check "PLU == true"
                x.LandUseBlock.PriorityLandUseTypeID != PriorityLandUseType.ALU.PriorityLandUseTypeID).GetArea();
        }
    }
}

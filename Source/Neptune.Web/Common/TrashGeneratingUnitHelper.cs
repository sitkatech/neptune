using System;
using Neptune.Web.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using LtInfo.Common.DbSpatial;
using MoreLinq;
using Neptune.Web.Areas.Trash.Controllers;

namespace Neptune.Web.Common
{
    public static partial class TrashGeneratingUnitHelper
    {
        private const decimal FullTrashCaptureLoading = 2.5m;

        public static bool UpdateTrashGeneratingUnits(this Delineation delineation)
        {
            return true;
            // TODO: neutered under 367. Will bring back once job scheduling is in place
        }
        public static bool UpdateTrashGeneratingUnitsAfterDelete(this Delineation delineation)
        {

            return true;
            // TODO: neutered under 367. Will bring back once job scheduling is in place
        }
        public static bool UpdateTrashGeneratingUnitsAfterDelete(this OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea)
        {
            return true;
            // TODO: neutered under 367. Will bring back once job scheduling is in place
        }



        public static bool UpdateTrashGeneratingUnits(this IEnumerable<Delineation> delineations)
        {
            return true;
            // TODO: neutered under 367. Will bring back once job scheduling is in place
        }

        public static bool UpdateTrashGeneratingUnits(this OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea)
        {
            return true;
            // TODO: neutered under 367. Will bring back once job scheduling is in place
        }

        public static double LoadBasedFullCapture(StormwaterJurisdiction jurisdiction)
        {
            var vTrashGeneratingUnitLoadBasedFullCaptures = HttpRequestStorage.DatabaseEntities.vTrashGeneratingUnitLoadBasedFullCaptures.Where(x =>
                x.StormwaterJurisdictionID == jurisdiction.StormwaterJurisdictionID);

            return vTrashGeneratingUnitLoadBasedFullCaptures.Any()
                ? vTrashGeneratingUnitLoadBasedFullCaptures.Sum(x =>
                    x.Area * (double) (x.BaselineLoadingRate - FullTrashCaptureLoading) *
                    DbSpatialHelper.SqlGeometryAreaToAcres)
                : 0;
        }

        public static double LoadBasedPartialCapture(StormwaterJurisdiction jurisdiction)
        {
            var vTrashGeneratingUnitLoadBasedPartialCaptures = HttpRequestStorage.DatabaseEntities.vTrashGeneratingUnitLoadBasedPartialCaptures.Where(x =>
                x.StormwaterJurisdictionID == jurisdiction.StormwaterJurisdictionID);

            return vTrashGeneratingUnitLoadBasedPartialCaptures.Any()
                ? vTrashGeneratingUnitLoadBasedPartialCaptures.Sum(x =>
                    x.Area * (double) (x.BaselineLoadingRate - x.ActualLoadingAfterTrashCapture) *
                    DbSpatialHelper.SqlGeometryAreaToAcres)
                : 0;
        }

        public static double LoadBasedOVTAProgressScores(StormwaterJurisdiction jurisdiction)
        {
            var vTrashGeneratingUnitLoadBasedTrashAssessments = HttpRequestStorage.DatabaseEntities.vTrashGeneratingUnitLoadBasedTrashAssessments.Where(x =>
                x.StormwaterJurisdictionID == jurisdiction.StormwaterJurisdictionID);

            return vTrashGeneratingUnitLoadBasedTrashAssessments.Any()
                ? vTrashGeneratingUnitLoadBasedTrashAssessments.Sum(x =>
                    x.Area * (double) (x.BaselineLoadingRate - x.ProgressLoadingRate) *
                    DbSpatialHelper.SqlGeometryAreaToAcres)
                : 0;
        }

        public static double TargetLoadReduction(this DbSet<TrashGeneratingUnit> trashGeneratingUnits,
            StormwaterJurisdiction jurisdiction)
        {
            var vTrashGeneratingUnitLoadBasedTargetReductions =
                HttpRequestStorage.DatabaseEntities.vTrashGeneratingUnitLoadBasedTargetReductions.Where(x =>
                    x.StormwaterJurisdictionID == jurisdiction.StormwaterJurisdictionID);

            return vTrashGeneratingUnitLoadBasedTargetReductions.Any()
                ? vTrashGeneratingUnitLoadBasedTargetReductions.Sum(x =>
                    x.Area * (double) (x.BaselineLoadingRate - FullTrashCaptureLoading) *
                    DbSpatialHelper.SqlGeometryAreaToAcres)
                : 0;
        }


        public static double TotalPLUAcreage(this DbSet<TrashGeneratingUnit> trashGeneratingUnits,
            StormwaterJurisdiction jurisdiction)
        {
            return trashGeneratingUnits.Where(x =>
                x.StormwaterJurisdictionID == jurisdiction.StormwaterJurisdictionID &&
                x.LandUseBlock != null &&
                x.LandUseBlock.PriorityLandUseTypeID != PriorityLandUseType.ALU.PriorityLandUseTypeID).GetArea();
        }

        public static double EquivalentAreaAcreage(this DbSet<TrashGeneratingUnit> trashGeneratingUnits,
            StormwaterJurisdiction jurisdiction)
        {
            return trashGeneratingUnits.Where(x =>
                x.StormwaterJurisdictionID == jurisdiction.StormwaterJurisdictionID &&
                x.OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentBaselineScoreID ==
                OnlandVisualTrashAssessmentScore.A.OnlandVisualTrashAssessmentScoreID &&
                x.TreatmentBMP.TrashCaptureStatusTypeID != TrashCaptureStatusType.Full.TrashCaptureStatusTypeID &&
                // This is how to check "PLU == true"
                x.LandUseBlock != null &&
                x.LandUseBlock.PriorityLandUseTypeID != PriorityLandUseType.ALU.PriorityLandUseTypeID
            ).GetArea();
        }

        public static double FullTrashCaptureAcreage(this DbSet<TrashGeneratingUnit> trashGeneratingUnits,
            StormwaterJurisdiction jurisdiction)
        {
            return trashGeneratingUnits.Where(x =>
                x.StormwaterJurisdictionID == jurisdiction.StormwaterJurisdictionID &&
                x.TreatmentBMP.TrashCaptureStatusTypeID ==
                TrashCaptureStatusType.Full.TrashCaptureStatusTypeID &&
                // This is how to check "PLU == true"
                x.LandUseBlock != null &&
                x.LandUseBlock.PriorityLandUseTypeID != PriorityLandUseType.ALU.PriorityLandUseTypeID
            ).GetArea();
        }

        // OVTA-based calculations

        public static double AlternateOVTAScoreDAcreage(this IQueryable<TrashGeneratingUnit> trashGeneratingUnits, StormwaterJurisdiction jurisdiction)
        {
            return trashGeneratingUnits.Where(x =>
                x.StormwaterJurisdictionID == jurisdiction.StormwaterJurisdictionID &&
                x.OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentBaselineScoreID ==
                OnlandVisualTrashAssessmentScore.D.OnlandVisualTrashAssessmentScoreID &&
                // This is how to check "PLU == true"
                x.LandUseBlock != null &&
                x.LandUseBlock.PriorityLandUseTypeID == PriorityLandUseType.ALU.PriorityLandUseTypeID
            ).GetArea();
        }

        public static double AlternateOVTAScoreBAcreage(this IQueryable<TrashGeneratingUnit> trashGeneratingUnits, StormwaterJurisdiction jurisdiction)
        {
            return trashGeneratingUnits.Where(x =>
                x.StormwaterJurisdictionID == jurisdiction.StormwaterJurisdictionID &&
                x.OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentBaselineScoreID == OnlandVisualTrashAssessmentScore.B.OnlandVisualTrashAssessmentScoreID &&
                // This is how to check "PLU == true"
                x.LandUseBlock != null &&
                x.LandUseBlock.PriorityLandUseTypeID == PriorityLandUseType.ALU.PriorityLandUseTypeID
            ).GetArea();
        }

        public static double PriorityOVTAScoreDAcreage(this IQueryable<TrashGeneratingUnit> trashGeneratingUnits, StormwaterJurisdiction jurisdiction)
        {
            return trashGeneratingUnits.Where(x =>
                x.StormwaterJurisdictionID == jurisdiction.StormwaterJurisdictionID &&
                x.OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentBaselineScoreID == OnlandVisualTrashAssessmentScore.D.OnlandVisualTrashAssessmentScoreID &&
                // This is how to check "PLU == true"
                x.LandUseBlock != null &&
                x.LandUseBlock.PriorityLandUseTypeID != PriorityLandUseType.ALU.PriorityLandUseTypeID
            ).GetArea();
        }

        public static double PriorityOVTAScoreBAcreage(this IQueryable<TrashGeneratingUnit> trashGeneratingUnits, StormwaterJurisdiction jurisdiction)
        {
            return trashGeneratingUnits.Where(x =>
                x.StormwaterJurisdictionID == jurisdiction.StormwaterJurisdictionID &&
                x.OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentBaselineScoreID == OnlandVisualTrashAssessmentScore.B.OnlandVisualTrashAssessmentScoreID &&
                // This is how to check "PLU == true"
                x.LandUseBlock != null &&
                x.LandUseBlock.PriorityLandUseTypeID != PriorityLandUseType.ALU.PriorityLandUseTypeID
            ).GetArea();
        }

        public static double AlternateOVTAScoreCAcreage(this IQueryable<TrashGeneratingUnit> trashGeneratingUnits, StormwaterJurisdiction jurisdiction)
        {
            return trashGeneratingUnits.Where(x =>
                x.StormwaterJurisdictionID == jurisdiction.StormwaterJurisdictionID &&
                x.OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentBaselineScoreID == OnlandVisualTrashAssessmentScore.C.OnlandVisualTrashAssessmentScoreID &&
                // This is how to check "PLU == true"
                x.LandUseBlock != null &&
                x.LandUseBlock.PriorityLandUseTypeID == PriorityLandUseType.ALU.PriorityLandUseTypeID
            ).GetArea();
        }

        public static double AlternateOVTAScoreAAcreage(this IQueryable<TrashGeneratingUnit> trashGeneratingUnits, StormwaterJurisdiction jurisdiction)
        {
            return trashGeneratingUnits.Where(x =>
                x.StormwaterJurisdictionID == jurisdiction.StormwaterJurisdictionID &&
                x.OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentBaselineScoreID == OnlandVisualTrashAssessmentScore.A.OnlandVisualTrashAssessmentScoreID &&
                // This is how to check "PLU == true"
                x.LandUseBlock != null &&
                x.LandUseBlock.PriorityLandUseTypeID == PriorityLandUseType.ALU.PriorityLandUseTypeID).GetArea();
        }

        public static double PriorityOVTAScoreCAcreage(this IQueryable<TrashGeneratingUnit> trashGeneratingUnits, StormwaterJurisdiction jurisdiction)
        {
            return trashGeneratingUnits.Where(x =>
                x.StormwaterJurisdictionID == jurisdiction.StormwaterJurisdictionID &&
                x.OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentBaselineScoreID == OnlandVisualTrashAssessmentScore.C.OnlandVisualTrashAssessmentScoreID &&
                // This is how to check "PLU == true"
                x.LandUseBlock != null &&
                x.LandUseBlock.PriorityLandUseTypeID != PriorityLandUseType.ALU.PriorityLandUseTypeID
            ).GetArea();
        }

        public static double PriorityOVTAScoreAAcreage(this IQueryable<TrashGeneratingUnit> trashGeneratingUnits, StormwaterJurisdiction jurisdiction)
        {
            return trashGeneratingUnits.Where(x =>
                x.StormwaterJurisdictionID == jurisdiction.StormwaterJurisdictionID &&
                x.OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentBaselineScoreID == OnlandVisualTrashAssessmentScore.A.OnlandVisualTrashAssessmentScoreID &&
                // This is how to check "PLU == true"
                x.LandUseBlock != null &&
                x.LandUseBlock.PriorityLandUseTypeID != PriorityLandUseType.ALU.PriorityLandUseTypeID
            ).GetArea();
        }
    }
}

using System;
using Neptune.Web.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Spatial;
using System.Linq;
using Hangfire;
using LtInfo.Common.DbSpatial;
using MoreLinq;
using Neptune.Web.Areas.Trash.Controllers;
using Neptune.Web.ScheduledJobs;

namespace Neptune.Web.Common
{
    public static partial class TrashGeneratingUnitHelper
    {
        private const decimal FullTrashCaptureLoading = 2.5m;

        // In contrast to the original implementation of these methods, the caller is now responsible for all SaveChangeses
        public static void UpdateTrashGeneratingUnits(this Delineation delineation, Person currentPerson)
        {
            var trashGeneratingUnitAdjustment = new TrashGeneratingUnitAdjustment(DateTime.Now, currentPerson, false)
            {
                AdjustedDelineationID = delineation.DelineationID
            };
            HttpRequestStorage.DatabaseEntities.TrashGeneratingUnitAdjustments.Add(trashGeneratingUnitAdjustment);
            HttpRequestStorage.DatabaseEntities.SaveChanges();

            BackgroundJob.Schedule(() =>
                ScheduledBackgroundJobBootstrapper.RunTrashGeneratingUnitAdjustmentScheduledBackgroundJob(), TimeSpan.FromSeconds(30));


        }

        public static void UpdateTrashGeneratingUnitsAfterDelete(this DbGeometry deletedGeometry, Person currentPerson)
        {
            var trashGeneratingUnitAdjustment = new TrashGeneratingUnitAdjustment(DateTime.Now, currentPerson, false)
            {
                DeletedGeometry = deletedGeometry
            };
            HttpRequestStorage.DatabaseEntities.TrashGeneratingUnitAdjustments.Add(trashGeneratingUnitAdjustment);
            HttpRequestStorage.DatabaseEntities.SaveChanges();

            BackgroundJob.Schedule(() =>
                ScheduledBackgroundJobBootstrapper.RunTrashGeneratingUnitAdjustmentScheduledBackgroundJob(), TimeSpan.FromSeconds(30));
        }
        

        public static void UpdateTrashGeneratingUnits(this IEnumerable<Delineation> delineations, Person currentPerson)
        {
            var trashGeneratingUnitAdjustments = delineations.Select(delineation => new TrashGeneratingUnitAdjustment(DateTime.Now, currentPerson, false)
            {
                AdjustedDelineationID = delineation.DelineationID
            });
            HttpRequestStorage.DatabaseEntities.TrashGeneratingUnitAdjustments.AddRange(trashGeneratingUnitAdjustments);
            HttpRequestStorage.DatabaseEntities.SaveChanges();

            BackgroundJob.Schedule(() =>
                ScheduledBackgroundJobBootstrapper.RunTrashGeneratingUnitAdjustmentScheduledBackgroundJob(), TimeSpan.FromSeconds(30));
        }

        public static void UpdateTrashGeneratingUnits(this OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea, Person currentPerson)
        {
            var trashGeneratingUnitAdjustment = new TrashGeneratingUnitAdjustment(DateTime.Now, currentPerson, false)
            {
                AdjustedOnlandVisualTrashAssessmentAreaID = onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaID
            };
            HttpRequestStorage.DatabaseEntities.TrashGeneratingUnitAdjustments.Add(trashGeneratingUnitAdjustment);
            HttpRequestStorage.DatabaseEntities.SaveChanges();

            BackgroundJob.Schedule(() =>
                ScheduledBackgroundJobBootstrapper.RunTrashGeneratingUnitAdjustmentScheduledBackgroundJob(), TimeSpan.FromSeconds(30));
        }

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
                x.LandUseBlock != null &&
                x.LandUseBlock.PriorityLandUseTypeID != PriorityLandUseType.ALU.PriorityLandUseTypeID
            ).GetArea();
        }

        public static double FullTrashCaptureAcreage(this List<TrashGeneratingUnit> trashGeneratingUnits)
        {
            return trashGeneratingUnits.Where(x =>
                x.IsFullTrashCapture &&
                // This is how to check "PLU == true"
                x.LandUseBlock != null &&
                x.LandUseBlock.PriorityLandUseTypeID != PriorityLandUseType.ALU.PriorityLandUseTypeID
            ).GetArea();
        }

        // OVTA-based calculations

        public static double AlternateOVTAScoreDAcreage(this List<TrashGeneratingUnit> trashGeneratingUnits)
        {
            return trashGeneratingUnits.Where(x =>
                x.OnlandVisualTrashAssessmentArea?.OnlandVisualTrashAssessmentBaselineScoreID ==
                OnlandVisualTrashAssessmentScore.D.OnlandVisualTrashAssessmentScoreID &&
                // This is how to check "PLU == true"
                x.LandUseBlock != null &&
                x.LandUseBlock.PriorityLandUseTypeID == PriorityLandUseType.ALU.PriorityLandUseTypeID
            ).GetArea();
        }

        public static double AlternateOVTAScoreBAcreage(this List<TrashGeneratingUnit> trashGeneratingUnits)
        {
            return trashGeneratingUnits.Where(x =>
                x.OnlandVisualTrashAssessmentArea?.OnlandVisualTrashAssessmentBaselineScoreID == OnlandVisualTrashAssessmentScore.B.OnlandVisualTrashAssessmentScoreID &&
                // This is how to check "PLU == true"
                x.LandUseBlock != null &&
                x.LandUseBlock.PriorityLandUseTypeID == PriorityLandUseType.ALU.PriorityLandUseTypeID
            ).GetArea();
        }

        public static double PriorityOVTAScoreDAcreage(this List<TrashGeneratingUnit> trashGeneratingUnits)
        {
            return trashGeneratingUnits.Where(x =>
                x.OnlandVisualTrashAssessmentArea?.OnlandVisualTrashAssessmentBaselineScoreID == OnlandVisualTrashAssessmentScore.D.OnlandVisualTrashAssessmentScoreID &&
                // This is how to check "PLU == true"
                x.LandUseBlock != null &&
                x.LandUseBlock.PriorityLandUseTypeID != PriorityLandUseType.ALU.PriorityLandUseTypeID
            ).GetArea();
        }

        public static double PriorityOVTAScoreBAcreage(this List<TrashGeneratingUnit> trashGeneratingUnits)
        {
            return trashGeneratingUnits.Where(x =>
                x.OnlandVisualTrashAssessmentArea?.OnlandVisualTrashAssessmentBaselineScoreID == OnlandVisualTrashAssessmentScore.B.OnlandVisualTrashAssessmentScoreID &&
                // This is how to check "PLU == true"
                x.LandUseBlock != null &&
                x.LandUseBlock.PriorityLandUseTypeID != PriorityLandUseType.ALU.PriorityLandUseTypeID
            ).GetArea();
        }

        public static double AlternateOVTAScoreCAcreage(this List<TrashGeneratingUnit> trashGeneratingUnits)
        {
            return trashGeneratingUnits.Where(x =>
                x.OnlandVisualTrashAssessmentArea?.OnlandVisualTrashAssessmentBaselineScoreID == OnlandVisualTrashAssessmentScore.C.OnlandVisualTrashAssessmentScoreID &&
                // This is how to check "PLU == true"
                x.LandUseBlock != null &&
                x.LandUseBlock.PriorityLandUseTypeID == PriorityLandUseType.ALU.PriorityLandUseTypeID
            ).GetArea();
        }

        public static double AlternateOVTAScoreAAcreage(this List<TrashGeneratingUnit> trashGeneratingUnits)
        {
            return trashGeneratingUnits.Where(x =>
                x.OnlandVisualTrashAssessmentArea?.OnlandVisualTrashAssessmentBaselineScoreID == OnlandVisualTrashAssessmentScore.A.OnlandVisualTrashAssessmentScoreID &&
                // This is how to check "PLU == true"
                x.LandUseBlock != null &&
                x.LandUseBlock.PriorityLandUseTypeID == PriorityLandUseType.ALU.PriorityLandUseTypeID).GetArea();
        }

        public static double PriorityOVTAScoreCAcreage(this List<TrashGeneratingUnit> trashGeneratingUnits)
        {
            return trashGeneratingUnits.Where(x =>
                x.OnlandVisualTrashAssessmentArea?.OnlandVisualTrashAssessmentBaselineScoreID == OnlandVisualTrashAssessmentScore.C.OnlandVisualTrashAssessmentScoreID &&
                // This is how to check "PLU == true"
                x.LandUseBlock != null &&
                x.LandUseBlock.PriorityLandUseTypeID != PriorityLandUseType.ALU.PriorityLandUseTypeID
            ).GetArea();
        }

        public static double PriorityOVTAScoreAAcreage(this List<TrashGeneratingUnit> trashGeneratingUnits)
        {
            return trashGeneratingUnits.Where(x =>
                x.OnlandVisualTrashAssessmentArea?.OnlandVisualTrashAssessmentBaselineScoreID == OnlandVisualTrashAssessmentScore.A.OnlandVisualTrashAssessmentScoreID &&
                // This is how to check "PLU == true"
                x.LandUseBlock != null &&
                x.LandUseBlock.PriorityLandUseTypeID != PriorityLandUseType.ALU.PriorityLandUseTypeID
            ).GetArea();
        }
    }
}

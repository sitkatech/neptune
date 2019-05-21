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
        public const string DelineationObjectType = "Delineation";
        public const string OnlandVisualTrashAssessmentAreaObjectType = "OnlandVisualTrashAssessmentArea";

        public static bool UpdateTrashGeneratingUnits(this Delineation delineation)
        {
            return true;
            // TODO: neutered under 367. Will bring back once job scheduling is in place
            //// calling save changes here so the caller can't forget to
            //HttpRequestStorage.DatabaseEntities.SaveChanges();

            //try
            //{
            //    var objectIDs =
            //        new SqlParameter("@ObjectIDs", FormatIDString(new List<int> { delineation.DelineationID }));
            //    var objectType = new SqlParameter("@ObjectType", DelineationObjectType);

            //    HttpRequestStorage.DatabaseEntities.Database.CommandTimeout = 600;
            //    HttpRequestStorage.DatabaseEntities.Database.ExecuteSqlCommand(
            //        "dbo.pRebuildTrashGeneratingUnitTableRelative @ObjectIDs, @ObjectType", objectIDs, objectType);

            //    return true;
            //}
            //catch
            //{
            //    return false;
            //}
        }
        public static bool UpdateTrashGeneratingUnitsAfterDelete(this Delineation delineation)
        {

            return true;
            // TODO: neutered under 367. Will bring back once job scheduling is in place

            //var wellKnownText = delineation.DelineationGeometry.ToString();
            //wellKnownText = wellKnownText.Substring(wellKnownText.IndexOf("POLYGON", StringComparison.InvariantCulture));

            //// calling save changes here so the caller can't forget to
            //HttpRequestStorage.DatabaseEntities.SaveChanges();

            //try
            //{
            //    var geometryWKT = new SqlParameter("@GeometryWKT", wellKnownText);

            //    HttpRequestStorage.DatabaseEntities.Database.CommandTimeout = 600;
            //    HttpRequestStorage.DatabaseEntities.Database.ExecuteSqlCommand(
            //        "dbo.pRebuildTrashGeneratingUnitTableRelative @GeometryWKT", geometryWKT);

            //    return true;
            //}
            //catch
            //{
            //    return false;
            //}
        }

        public static bool UpdateTrashGeneratingUnits(this IEnumerable<Delineation> delineations)
        {
            return true;
            // TODO: neutered under 367. Will bring back once job scheduling is in place

            //// calling save changes here so the caller can't forget to
            //HttpRequestStorage.DatabaseEntities.SaveChanges();

            //try
            //{
            //    var objectIDs =
            //        new SqlParameter("@ObjectIDs", FormatIDString(delineations.Select(x => x.DelineationID)));
            //    var objectType = new SqlParameter("@ObjectType", DelineationObjectType);

            //    HttpRequestStorage.DatabaseEntities.Database.ExecuteSqlCommand(
            //        "dbo.pRebuildTrashGeneratingUnitTableRelative @ObjectIDs, @ObjectType", objectIDs, objectType);
            //    return true;
            //}
            //catch
            //{
            //    return false;
            //}
        }

        public static bool UpdateTrashGeneratingUnits(this OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea)
        {
            return true;
            // TODO: neutered under 367. Will bring back once job scheduling is in place

            //// calling save changes here so the caller can't forget to
            //HttpRequestStorage.DatabaseEntities.SaveChanges();

            //try
            //{
            //    var objectIDs = new SqlParameter("@ObjectIDs", FormatIDString(new List<int> { onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaID }));
            //    var objectType = new SqlParameter("@ObjectType", OnlandVisualTrashAssessmentAreaObjectType);

            //    HttpRequestStorage.DatabaseEntities.Database.ExecuteSqlCommand(
            //        "dbo.pRebuildTrashGeneratingUnitTableRelative @ObjectIDs, @ObjectType", objectIDs, objectType);
            //    return true;
            //}
            //catch
            //{
            //    return false;
            //}
        }

        public static string FormatIDString(IEnumerable<int> idList)
        {
            return String.Join(",", idList);
        }
        public static double LoadBasedFullCapture(StormwaterJurisdiction jurisdiction)
        {
            var vTrashGeneratingUnitLoadBasedFullCaptures = HttpRequestStorage.DatabaseEntities.vTrashGeneratingUnitLoadBasedFullCaptures.Where(x =>
                x.StormwaterJurisdictionID == jurisdiction.StormwaterJurisdictionID);

            return vTrashGeneratingUnitLoadBasedFullCaptures.Sum(x =>
                x.Area * (double) (x.BaselineLoadingRate - FullTrashCaptureLoading) * DbSpatialHelper.SqlGeometryAreaToAcres);
        }

        public static double LoadBasedPartialCapture(StormwaterJurisdiction jurisdiction)
        {
            var vTrashGeneratingUnitLoadBasedPartialCaptures = HttpRequestStorage.DatabaseEntities.vTrashGeneratingUnitLoadBasedPartialCaptures.Where(x =>
                x.StormwaterJurisdictionID == jurisdiction.StormwaterJurisdictionID);

            return vTrashGeneratingUnitLoadBasedPartialCaptures.Sum(x =>
                x.Area * (double) (x.BaselineLoadingRate - x.ActualLoadingAfterTrashCapture) *
                DbSpatialHelper.SqlGeometryAreaToAcres);
        }

        public static double LoadBasedOVTAProgressScores(StormwaterJurisdiction jurisdiction)
        {
            var vTrashGeneratingUnitLoadBasedTrashAssessments = HttpRequestStorage.DatabaseEntities.vTrashGeneratingUnitLoadBasedTrashAssessments.Where(x =>
                x.StormwaterJurisdictionID == jurisdiction.StormwaterJurisdictionID);

            return vTrashGeneratingUnitLoadBasedTrashAssessments.Sum(x =>
                x.Area * (double) (x.BaselineLoadingRate - x.ProgressLoadingRate) *
                DbSpatialHelper.SqlGeometryAreaToAcres);
        }

        public static double TargetLoadReduction(this DbSet<TrashGeneratingUnit> trashGeneratingUnits,
            StormwaterJurisdiction jurisdiction)
        {
            var vTrashGeneratingUnitLoadBasedTargetReductions =
                HttpRequestStorage.DatabaseEntities.vTrashGeneratingUnitLoadBasedTargetReductions.Where(x =>
                    x.StormwaterJurisdictionID == jurisdiction.StormwaterJurisdictionID);

            return vTrashGeneratingUnitLoadBasedTargetReductions.Sum(x =>
                x.Area * (double) (x.BaselineLoadingRate - FullTrashCaptureLoading) *
                DbSpatialHelper.SqlGeometryAreaToAcres);
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

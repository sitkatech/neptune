using System;
using Neptune.Web.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MoreLinq;
using Neptune.Web.Areas.Trash.Controllers;

namespace Neptune.Web.Common
{
    public static partial class TrashGeneratingUnitHelper
    {
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

        public static double LoadBasedFullCapture(this DbSet<TrashGeneratingUnit> trashGeneratingUnits,
            StormwaterJurisdiction jurisdiction)
        {
            var fullCapture = trashGeneratingUnits.Where(x =>
                x.StormwaterJurisdictionID == jurisdiction.StormwaterJurisdictionID &&
                x.TreatmentBMP.TrashCaptureStatusTypeID ==
                TrashCaptureStatusType.Full.TrashCaptureStatusTypeID &&
                x.LandUseBlock != null);

            var commercial = fullCapture.Where(x =>x.LandUseBlock.PriorityLandUseTypeID == PriorityLandUseType.Commercial.PriorityLandUseTypeID);
            var commercialArea = commercial.Any() ? commercial.GetAreaLoadBased() : 0;

            var highDensityResidential = fullCapture.Where(x => x.LandUseBlock.PriorityLandUseTypeID == PriorityLandUseType.HighDensityResidential.PriorityLandUseTypeID);
            var highDensityResidentialArea = highDensityResidential.Any() ? highDensityResidential.GetAreaLoadBased() : 0;

            var industrial = fullCapture.Where(x => x.LandUseBlock.PriorityLandUseTypeID == PriorityLandUseType.Industrial.PriorityLandUseTypeID);
            var industrialArea = industrial.Any() ? industrial.GetAreaLoadBased() : 0;

            var mixedUrban = fullCapture.Where(x => x.LandUseBlock.PriorityLandUseTypeID == PriorityLandUseType.MixedUrban.PriorityLandUseTypeID);
            var mixedUrbanArea = mixedUrban.Any() ? mixedUrban.GetAreaLoadBased() : 0;

            var commercialRetail = fullCapture.Where(x => x.LandUseBlock.PriorityLandUseTypeID == PriorityLandUseType.CommercialRetail.PriorityLandUseTypeID);
            var commercialRetailArea = commercialRetail.Any() ? commercialRetail.GetAreaLoadBased() : 0;

            var publicTransportationStations = fullCapture.Where(x =>  x.LandUseBlock.PriorityLandUseTypeID == PriorityLandUseType.PublicTransportationStations.PriorityLandUseTypeID);
            var publicTransportationStationsArea = publicTransportationStations.Any() ? publicTransportationStations.GetAreaLoadBased() : 0;

            var alu = fullCapture.Where(x => x.LandUseBlock.PriorityLandUseTypeID == PriorityLandUseType.ALU.PriorityLandUseTypeID);
            var aluArea = alu.Any() ? alu.GetAreaLoadBased() : 0;

            var estimatedLoadBased = commercialArea + highDensityResidentialArea + industrialArea + mixedUrbanArea +
                                       commercialRetailArea + publicTransportationStationsArea + aluArea;
            var actualLoadBased = fullCapture.Where(x => x.LandUseBlockID != null).GetArea() * (double) OnlandVisualTrashAssessmentScore.A.TrashGenerationRate;

            return estimatedLoadBased - actualLoadBased;
        }

        public static double LoadBasedPartialCapture(this DbSet<TrashGeneratingUnit> trashGeneratingUnits,
            StormwaterJurisdiction jurisdiction)
        {
            var partialCapture = trashGeneratingUnits.Where(x =>
                x.StormwaterJurisdictionID == jurisdiction.StormwaterJurisdictionID &&
                x.TreatmentBMP.TrashCaptureStatusTypeID ==
                TrashCaptureStatusType.Partial.TrashCaptureStatusTypeID &&
                x.LandUseBlock != null);

            var commercial = partialCapture.Where(x => x.LandUseBlock.PriorityLandUseTypeID == PriorityLandUseType.Commercial.PriorityLandUseTypeID);
            var commercialArea = commercial.Any() ? commercial.GetAreaLoadBased() : 0;

            var highDensityResidential = partialCapture.Where(x => x.LandUseBlock.PriorityLandUseTypeID == PriorityLandUseType.HighDensityResidential.PriorityLandUseTypeID);
            var highDensityResidentialArea = highDensityResidential.Any() ? highDensityResidential.GetAreaLoadBased() : 0;

            var industrial = partialCapture.Where(x => x.LandUseBlock.PriorityLandUseTypeID == PriorityLandUseType.Industrial.PriorityLandUseTypeID);
            var industrialArea = industrial.Any() ? industrial.GetAreaLoadBased() : 0;

            var mixedUrban = partialCapture.Where(x => x.LandUseBlock.PriorityLandUseTypeID == PriorityLandUseType.MixedUrban.PriorityLandUseTypeID);
            var mixedUrbanArea = mixedUrban.Any() ? mixedUrban.GetAreaLoadBased() : 0;

            var commercialRetail = partialCapture.Where(x => x.LandUseBlock.PriorityLandUseTypeID == PriorityLandUseType.CommercialRetail.PriorityLandUseTypeID);
            var commercialRetailArea = commercialRetail.Any() ? commercialRetail.GetAreaLoadBased() : 0;

            var publicTransportationStations = partialCapture.Where(x => x.LandUseBlock.PriorityLandUseTypeID == PriorityLandUseType.PublicTransportationStations.PriorityLandUseTypeID);
            var publicTransportationStationsArea = publicTransportationStations.Any() ? publicTransportationStations.GetAreaLoadBased() : 0;

            var alu = partialCapture.Where(x => x.LandUseBlock.PriorityLandUseTypeID == PriorityLandUseType.ALU.PriorityLandUseTypeID);
            var aluArea = alu.Any() ? alu.GetAreaLoadBased() : 0;

            var estimatedLoadBased = commercialArea + highDensityResidentialArea + industrialArea + mixedUrbanArea +
                                       commercialRetailArea + publicTransportationStationsArea + aluArea;

            var actualLoadBased = partialCapture.Where(x => x.LandUseBlockID != null).GetAreaPartialCaptureLoadBased();

            return estimatedLoadBased - actualLoadBased;
        }

        public static double LoadBasedOVTAProgressScores(this DbSet<OnlandVisualTrashAssessment> onlandVisualTrashAssessments,
            StormwaterJurisdiction jurisdiction)
        {
            var trashGeneratingUnitsFromProgressAssessments = onlandVisualTrashAssessments
                .Where(x => x.IsProgressAssessment)
                .Select(x => x.OnlandVisualTrashAssessmentArea).AsEnumerable().DistinctBy(x=>x.OnlandVisualTrashAssessmentAreaID).ToList();
            
            var estimatedOVTAsArea = trashGeneratingUnitsFromProgressAssessments.GetAreaBaselineScoreLoadBased();
            var actualOVtAsArea = trashGeneratingUnitsFromProgressAssessments.GetAreaProgressScoreLoadBased();

            return estimatedOVTAsArea - actualOVtAsArea;
        }

        public static double TargetLoadReduction(this DbSet<TrashGeneratingUnit> trashGeneratingUnits,
            StormwaterJurisdiction jurisdiction)
        {
            var partialCapture = trashGeneratingUnits.Where(x =>
                x.StormwaterJurisdictionID == jurisdiction.StormwaterJurisdictionID &&
                x.TreatmentBMP.TrashCaptureStatusTypeID ==
                TrashCaptureStatusType.Partial.TrashCaptureStatusTypeID &&
                x.LandUseBlock != null && x.LandUseBlock.PriorityLandUseTypeID != null);



            var baselineLoading = partialCapture.GetAreaLoadBased();

            var hypotheticalLoad = partialCapture.GetArea() *
                                  (double) OnlandVisualTrashAssessmentScore.A.TrashGenerationRate;


            return baselineLoading - hypotheticalLoad;
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

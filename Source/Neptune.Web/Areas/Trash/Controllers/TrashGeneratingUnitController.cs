using LtInfo.Common.DbSpatial;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Neptune.Web.Areas.Trash.Controllers;

namespace Neptune.Web.Areas.Trash.Controllers
{
    public class TrashGeneratingUnitController : NeptuneBaseController
    {
        [HttpGet]
        [JurisdictionEditFeature]
        public JsonResult AcreBasedCalculations(StormwaterJurisdictionPrimaryKey jurisdictionPrimaryKey)
        {
            var jurisdiction = jurisdictionPrimaryKey.EntityObject;
            var trashGeneratingUnits = HttpRequestStorage.DatabaseEntities.TrashGeneratingUnits;

            var fullTrashCapture = trashGeneratingUnits.FullTrashCaptureAcreage(jurisdiction);

            var equivalentArea = trashGeneratingUnits.EquivalentAreaAcreage(jurisdiction);

            var totalAcresCaptured = fullTrashCapture + equivalentArea;

            var totalPLUAcres = trashGeneratingUnits.TotalPLUAcreage(jurisdiction);

            var percentTreated = totalPLUAcres != 0 ? totalAcresCaptured / totalPLUAcres : 0;

            return Json(new AreaBasedAcreCalculationsSimple
            {
                FullTrashCaptureAcreage = fullTrashCapture,
                EquivalentAreaAcreage = equivalentArea,
                TotalAcresCaptured = totalAcresCaptured,
                TotalPLUAcres = totalPLUAcres,
                PercentTreated = percentTreated
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [JurisdictionEditFeature]
        public JsonResult OVTABasedResultsCalculations(StormwaterJurisdictionPrimaryKey jurisdictionPrimaryKey)
        {
            var jurisdiction = jurisdictionPrimaryKey.EntityObject;
            var trashGeneratingUnits = HttpRequestStorage.DatabaseEntities.TrashGeneratingUnits;

            var sumPLUAcresWhereOVTAIsA = TrashGeneratingUnitHelper.PriorityOVTAScoreAAcreage(trashGeneratingUnits, jurisdiction);
           
            var sumPLUAcrexsWhereOVTAIsB = TrashGeneratingUnitHelper.PriorityOVTAScoreBAcreage(trashGeneratingUnits, jurisdiction);

            var sumPLUAcrexsWhereOVTAIsC = TrashGeneratingUnitHelper.PriorityOVTAScoreCAcreage(trashGeneratingUnits, jurisdiction);

            var sumPLUAcrexsWhereOVTAIsD = TrashGeneratingUnitHelper.PriorityOVTAScoreDAcreage(trashGeneratingUnits, jurisdiction);


            var sumALUAcresWhereOVTAIsA = TrashGeneratingUnitHelper.AlternateOVTAScoreAAcreage(trashGeneratingUnits, jurisdiction);

            var sumALUAcresWhereOVTAIsB = TrashGeneratingUnitHelper.AlternateOVTAScoreBAcreage(trashGeneratingUnits, jurisdiction);

            var sumALUAcresWhereOVTAIsC = TrashGeneratingUnitHelper.AlternateOVTAScoreCAcreage(trashGeneratingUnits, jurisdiction);

            var sumALUAcresWhereOVTAIsD = TrashGeneratingUnitHelper.AlternateOVTAScoreDAcreage(trashGeneratingUnits, jurisdiction);

            return Json(new OVTAResultsSimple
            {
                PLUSumAcresWhereOVTAIsA = sumPLUAcresWhereOVTAIsA,
                PLUSumAcresWhereOVTAIsB = sumPLUAcrexsWhereOVTAIsB,
                PLUSumAcresWhereOVTAIsC = sumPLUAcrexsWhereOVTAIsC,
                PLUSumAcresWhereOVTAIsD = sumPLUAcrexsWhereOVTAIsD,
                ALUSumAcresWhereOVTAIsA = sumALUAcresWhereOVTAIsA,
                ALUSumAcresWhereOVTAIsB = sumALUAcresWhereOVTAIsB,
                ALUSumAcresWhereOVTAIsC = sumALUAcresWhereOVTAIsC,
                ALUSumAcresWhereOVTAIsD = sumALUAcresWhereOVTAIsD
            }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        [JurisdictionEditFeature]
        public JsonResult LoadBasedResultsCalculations(StormwaterJurisdictionPrimaryKey jurisdictionPrimaryKey)
        {
            var jurisdiction = jurisdictionPrimaryKey.EntityObject;
            var trashGeneratingUnits = HttpRequestStorage.DatabaseEntities.TrashGeneratingUnits;
            var ovtas = HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessments;

            var viaFullCapture = TrashGeneratingUnitHelper.LoadBasedFullCapture(trashGeneratingUnits, jurisdiction);

            var viaPartialCapture = TrashGeneratingUnitHelper.LoadBasedPartialCapture(trashGeneratingUnits, jurisdiction);
            var viaOVTAs = TrashGeneratingUnitHelper.LoadBasedOVTAProgressScores(ovtas, jurisdiction);
            var totalAchieved = viaFullCapture + viaPartialCapture + viaOVTAs;
            var targetLoadReduction = TrashGeneratingUnitHelper.TargetLoadReduction(trashGeneratingUnits, jurisdiction);



            return Json(new LoadResultsSimple
            {
                LoadFullCapture = viaFullCapture,
                LoadPartialCapture = viaPartialCapture,
                LoadOVTA = viaOVTAs,
                TotalAchieved = totalAchieved,
                TargetLoadReduction = targetLoadReduction

            }, JsonRequestBehavior.AllowGet);
        }

    }

    public static class AreaCalculationsHelper
    {
        private const int decimalPlacesToDisplay = 0;

        public static double GetArea(this IEnumerable<TrashGeneratingUnit> trashGeneratingUnits)
        {
            return trashGeneratingUnits
                .Select(x => x.TrashGeneratingUnitGeometry.Area * DbSpatialHelper.SqlGeometryAreaToAcres).Sum()
                .GetValueOrDefault();
        }

        public static double GetAreaLoadBased(this IQueryable<TrashGeneratingUnit> trashGeneratingUnits)
        {
            var loadOfTrashGeneratingUnitsWithNoBaselineAssessmentScore = trashGeneratingUnits
                .Where(x => x.OnlandVisualTrashAssessmentArea == null || x.OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentBaselineScoreID == null).Select(
                    x => x.TrashGeneratingUnitGeometry.Area * DbSpatialHelper.SqlGeometryAreaToAcres *
                         (double) x.LandUseBlock.TrashGenerationRate).Sum().GetValueOrDefault();

            var loadOfTrashGeneratingUnitsWithBaselineAssessmentScore = trashGeneratingUnits.Where(x =>
                    x.OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentBaselineScoreID != null)
                .Select(x => x.TrashGeneratingUnitGeometry.Area * DbSpatialHelper.SqlGeometryAreaToAcres * (double)
                             Math.Min(x.LandUseBlock.TrashGenerationRate,
                                 x.OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentBaselineScore
                                     .TrashGenerationRate)).Sum().GetValueOrDefault();

            return Math.Round(loadOfTrashGeneratingUnitsWithBaselineAssessmentScore + loadOfTrashGeneratingUnitsWithNoBaselineAssessmentScore, decimalPlacesToDisplay); // will never be null
        }

        public static double GetAreaPartialCaptureLoadBased(this IEnumerable<TrashGeneratingUnit> trashGeneratingUnits)
        {

            return Math.Round(trashGeneratingUnits.Select(x =>
                x.TrashGeneratingUnitGeometry.Area * DbSpatialHelper.SqlGeometryAreaToAcres *
                Math.Min((double) OnlandVisualTrashAssessmentScore.A.TrashGenerationRate ,
                    (1 - x.TreatmentBMP.TrashCaptureEffectiveness.Value / 100) *
                    (double) x.OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentBaselineScore.TrashGenerationRate)).Sum().GetValueOrDefault(), decimalPlacesToDisplay);

        }

        public static double GetAreaBaselineScoreLoadBased(this IEnumerable<OnlandVisualTrashAssessmentArea> ovtaas )
        {

            return Math.Round(ovtaas
                .Select(x =>
                    x.OnlandVisualTrashAssessmentAreaGeometry.Area * DbSpatialHelper.SqlGeometryAreaToAcres *
                    (double)x.OnlandVisualTrashAssessmentBaselineScore.TrashGenerationRate).Sum().GetValueOrDefault(), decimalPlacesToDisplay);
        }

        public static double GetAreaProgressScoreLoadBased(this IEnumerable<OnlandVisualTrashAssessmentArea> ovtaas )
        {

            return Math.Round(ovtaas
                .Select(x =>
                    x.OnlandVisualTrashAssessmentAreaGeometry.Area * DbSpatialHelper.SqlGeometryAreaToAcres *
                    (double)x.OnlandVisualTrashAssessmentBaselineScore.TrashGenerationRate).Sum().GetValueOrDefault(), decimalPlacesToDisplay);
        }
    }
}


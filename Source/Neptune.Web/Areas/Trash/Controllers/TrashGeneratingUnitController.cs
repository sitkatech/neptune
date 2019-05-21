using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;
using System.Web.Mvc;

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

            var sumPLUAcresWhereOVTAIsA = trashGeneratingUnits.PriorityOVTAScoreAAcreage(jurisdiction);

            var sumPLUAcrexsWhereOVTAIsB = trashGeneratingUnits.PriorityOVTAScoreBAcreage(jurisdiction);

            var sumPLUAcrexsWhereOVTAIsC = trashGeneratingUnits.PriorityOVTAScoreCAcreage(jurisdiction);

            var sumPLUAcrexsWhereOVTAIsD = trashGeneratingUnits.PriorityOVTAScoreDAcreage(jurisdiction);


            var sumALUAcresWhereOVTAIsA = trashGeneratingUnits.AlternateOVTAScoreAAcreage(jurisdiction);

            var sumALUAcresWhereOVTAIsB = trashGeneratingUnits.AlternateOVTAScoreBAcreage(jurisdiction);

            var sumALUAcresWhereOVTAIsC = trashGeneratingUnits.AlternateOVTAScoreCAcreage(jurisdiction);

            var sumALUAcresWhereOVTAIsD = trashGeneratingUnits.AlternateOVTAScoreDAcreage(jurisdiction);

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

            var viaFullCapture = trashGeneratingUnits.LoadBasedFullCapture(jurisdiction);

            var viaPartialCapture = trashGeneratingUnits.LoadBasedPartialCapture(jurisdiction);
            var viaOVTAs = ovtas.LoadBasedOVTAProgressScores(jurisdiction);
            var totalAchieved = viaFullCapture + viaPartialCapture + viaOVTAs;
            var targetLoadReduction = trashGeneratingUnits.TargetLoadReduction(jurisdiction);



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
}


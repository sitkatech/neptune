using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;
using System.Web.Mvc;
using LtInfo.Common.DbSpatial;
using LtInfo.Common.MvcResults;
using Neptune.Web.Areas.Trash.Views.TrashGeneratingUnit;

namespace Neptune.Web.Areas.Trash.Controllers
{
    public class TrashGeneratingUnitController : NeptuneBaseController
    {
        [HttpGet]
        [JurisdictionManageFeature]
        public ViewResult Index()
        {
            var viewData = new IndexViewData(CurrentPerson);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [JurisdictionManageFeature]
        public GridJsonNetJObjectResult<TrashGeneratingUnit> TrashGeneratingUnitGridJsonData()
        {
            // ReSharper disable once InconsistentNaming
            var treatmentBMPs = GetTrashGeneratingUnitsAndGridSpec(out var gridSpec);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<TrashGeneratingUnit>(treatmentBMPs, gridSpec);
            return gridJsonNetJObjectResult;
        }

        private List<TrashGeneratingUnit> GetTrashGeneratingUnitsAndGridSpec(out TrashGeneratingUnitGridSpec gridSpec)
        {
            gridSpec = new TrashGeneratingUnitGridSpec();

            var stormwaterJurisdictionIDsCurrentPersonCanView = CurrentPerson.GetStormwaterJurisdictionsPersonCanEdit().Select(x => x.StormwaterJurisdictionID).ToList();

            return HttpRequestStorage.DatabaseEntities.TrashGeneratingUnits.Where(x => stormwaterJurisdictionIDsCurrentPersonCanView.Contains(x.StormwaterJurisdictionID)).OrderByDescending(x => x.LastUpdateDate)
                .Include(x => x.LandUseBlock)
                .Include(x => x.StormwaterJurisdiction.Organization).ToList();
        }

        [HttpGet]
        [JurisdictionEditFeature]
        public JsonResult AcreBasedCalculations(StormwaterJurisdictionPrimaryKey jurisdictionPrimaryKey)
        {
            var jurisdiction = jurisdictionPrimaryKey.EntityObject;
            var trashGeneratingUnits = HttpRequestStorage.DatabaseEntities.TrashGeneratingUnits.Where(x=>x.StormwaterJurisdictionID == jurisdiction.StormwaterJurisdictionID).ToList();

            var fullTrashCapture = trashGeneratingUnits.FullTrashCaptureAcreage();

            var equivalentArea = trashGeneratingUnits.EquivalentAreaAcreage();

            var totalAcresCaptured = fullTrashCapture + equivalentArea;

            var totalPLUAcres = jurisdiction.LandUseBlocks
                .Where(x => x.PriorityLandUseTypeID != PriorityLandUseType.ALU.PriorityLandUseTypeID).Sum(x =>
                    x.LandUseBlockGeometry.Area * DbSpatialHelper.SqlGeometryAreaToAcres) ?? 0;

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
            var trashGeneratingUnits = HttpRequestStorage.DatabaseEntities.TrashGeneratingUnits.Where(x=>x.StormwaterJurisdictionID == jurisdiction.StormwaterJurisdictionID).ToList();

            var sumPLUAcresWhereOVTAIsA = trashGeneratingUnits.PriorityOVTAScoreAAcreage();

            var sumPLUAcrexsWhereOVTAIsB = trashGeneratingUnits.PriorityOVTAScoreBAcreage();

            var sumPLUAcrexsWhereOVTAIsC = trashGeneratingUnits.PriorityOVTAScoreCAcreage();

            var sumPLUAcrexsWhereOVTAIsD = trashGeneratingUnits.PriorityOVTAScoreDAcreage();


            var sumALUAcresWhereOVTAIsA = trashGeneratingUnits.AlternateOVTAScoreAAcreage();

            var sumALUAcresWhereOVTAIsB = trashGeneratingUnits.AlternateOVTAScoreBAcreage();

            var sumALUAcresWhereOVTAIsC = trashGeneratingUnits.AlternateOVTAScoreCAcreage();

            var sumALUAcresWhereOVTAIsD = trashGeneratingUnits.AlternateOVTAScoreDAcreage();

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

            var viaFullCapture = TrashGeneratingUnitHelper.LoadBasedFullCapture(jurisdiction);

            var viaPartialCapture = TrashGeneratingUnitHelper.LoadBasedPartialCapture(jurisdiction);
            var viaOVTAs = TrashGeneratingUnitHelper.LoadBasedOVTAProgressScores(jurisdiction);
            var totalAchieved = viaFullCapture + viaPartialCapture + viaOVTAs;
            var targetLoadReduction = TrashGeneratingUnitHelper.TargetLoadReduction(jurisdiction);

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


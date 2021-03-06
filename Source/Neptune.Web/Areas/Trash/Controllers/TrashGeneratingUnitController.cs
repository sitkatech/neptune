﻿using System.Collections.Generic;
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
using Neptune.Web.Security.Shared;

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
        public GridJsonNetJObjectResult<vTrashGeneratingUnitLoadStatistic> TrashGeneratingUnitGridJsonData()
        {
            var gridSpec = new TrashGeneratingUnitGridSpec();
            var stormwaterJurisdictionIDsCurrentPersonCanView = CurrentPerson.GetStormwaterJurisdictionIDsPersonCanView();
            var treatmentBMPs = HttpRequestStorage.DatabaseEntities.vTrashGeneratingUnitLoadStatistics
                .Where(x => stormwaterJurisdictionIDsCurrentPersonCanView.Contains(x.StormwaterJurisdictionID))
                .OrderByDescending(x => x.LastUpdateDate).ToList();
            return new GridJsonNetJObjectResult<vTrashGeneratingUnitLoadStatistic>(treatmentBMPs, gridSpec);
        }

        [HttpGet]
        [AnonymousUnclassifiedFeature]
        [StormwaterJurisdictionViewFeature]
        public JsonResult AcreBasedCalculations(StormwaterJurisdictionPrimaryKey jurisdictionPrimaryKey)
        {
            var jurisdiction = jurisdictionPrimaryKey.EntityObject;
            var trashGeneratingUnits = HttpRequestStorage.DatabaseEntities.TrashGeneratingUnits.Include(x => x.LandUseBlock).Where(x=>x.StormwaterJurisdictionID == jurisdiction.StormwaterJurisdictionID && x.LandUseBlock != null).ToList();

            var fullTrashCapture = trashGeneratingUnits.FullTrashCaptureAcreage();

            var equivalentArea = trashGeneratingUnits.EquivalentAreaAcreage();

            var totalAcresCaptured = fullTrashCapture + equivalentArea;

            var totalPLUAcres = jurisdiction.LandUseBlocks
                .Where(x => x.PriorityLandUseTypeID != PriorityLandUseType.ALU.PriorityLandUseTypeID && x.PermitTypeID == PermitType.PhaseIMS4.PermitTypeID).Sum(x =>
                    x.LandUseBlockGeometry.Area * DbSpatialHelper.SquareMetersToAcres) ?? 0;

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
        [AnonymousUnclassifiedFeature]
        [StormwaterJurisdictionViewFeature]
        public JsonResult OVTABasedResultsCalculations(StormwaterJurisdictionPrimaryKey jurisdictionPrimaryKey)
        {
            var jurisdiction = jurisdictionPrimaryKey.EntityObject;
            var trashGeneratingUnits = HttpRequestStorage.DatabaseEntities.TrashGeneratingUnits.Include(x => x.LandUseBlock).Where(x=>x.StormwaterJurisdictionID == jurisdiction.StormwaterJurisdictionID && x.LandUseBlock != null).ToList();

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
        [AnonymousUnclassifiedFeature]
        [StormwaterJurisdictionViewFeature]
        public JsonResult LoadBasedResultsCalculations(StormwaterJurisdictionPrimaryKey jurisdictionPrimaryKey)
        {
            var jurisdiction = jurisdictionPrimaryKey.EntityObject;

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


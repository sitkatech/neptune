using LtInfo.Common.DbSpatial;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;
using System;
using System.Collections.Generic;
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
    }

    public static class AreaCalculationsHelper
    {
        public static double GetArea(this IEnumerable<TrashGeneratingUnit> trashGeneratingUnits)
        {
            return trashGeneratingUnits
                .Select(x => x.TrashGeneratingUnitGeometry.Area * DbSpatialHelper.SqlGeometryAreaToAcres).Sum()
                .GetValueOrDefault();
        }
    }
}

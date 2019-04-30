using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.DbSpatial;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;

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

            var fullTrashCapture = trashGeneratingUnits.Where(x =>
                x.StormwaterJurisdictionID == jurisdiction.StormwaterJurisdictionID &&
                x.TreatmentBMP.TrashCaptureStatusTypeID ==
                TrashCaptureStatusType.Full.TrashCaptureStatusTypeID &&
                // This is how to check "PLU == true"
                x.LandUseBlock != null && x.LandUseBlock.PriorityLandUseTypeID != null
                ).GetArea();

            var equivalentArea = trashGeneratingUnits.Where(x =>
                x.StormwaterJurisdictionID == jurisdiction.StormwaterJurisdictionID &&
                x.TreatmentBMP.TrashCaptureStatusTypeID != TrashCaptureStatusType.Full.TrashCaptureStatusTypeID /*&& todo: add PLU != true */ &&
                x.OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentScoreID == OnlandVisualTrashAssessmentScore.A.OnlandVisualTrashAssessmentScoreID).GetArea();

            var totalAcresCaptured = fullTrashCapture + equivalentArea;

            var totalPLUAcres = trashGeneratingUnits.Where(
                x => x.StormwaterJurisdictionID == jurisdiction.StormwaterJurisdictionID /*&& todo add PLU == true */).GetArea();

            return Json(new AreaBasedAcreCalculationsSimple(/* TODO: Initialize this object with the values calculated above*/), JsonRequestBehavior.AllowGet);
        }
    }

    public static class AreaCalculationsHelper
    {


        public static double GetArea(this IEnumerable<TrashGeneratingUnit> trashGeneratingUnits)
        {

            return trashGeneratingUnits
                .Select(x => x.TrashGeneratingUnitGeometry.Area * DbSpatialHelper.SqlGeometryAreaToAcres).Sum().GetValueOrDefault(); // will never be null
        }
    }
}
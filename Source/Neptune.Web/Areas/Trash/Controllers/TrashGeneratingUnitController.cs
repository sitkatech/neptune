using System.Linq;
using System.Web.Mvc;
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
        public JsonResult GetAcreBasedCalculations(StormwaterJurisdictionPrimaryKey jurisdictionPrimaryKey)
        {
            var jurisdiction = jurisdictionPrimaryKey.EntityObject;
            var trashGeneratingUnits = HttpRequestStorage.DatabaseEntities.TrashGeneratingUnits;

            var fullTrashCapture = trashGeneratingUnits.Where(x =>
                x.StormwaterJurisdictionID == jurisdiction.StormwaterJurisdictionID &&
                x.TreatmentBMP.TrashCaptureStatusTypeID == TrashCaptureStatusType.Full.TrashCaptureStatusTypeID /*&& todo add PLU == true */).Select(x=>x.TrashGeneratingUnitGeometry.Area).Sum();

            var equivalentArea = trashGeneratingUnits.Where(x =>
                x.StormwaterJurisdictionID == jurisdiction.StormwaterJurisdictionID &&
                x.TreatmentBMP.TrashCaptureStatusTypeID != TrashCaptureStatusType.Full.TrashCaptureStatusTypeID /*&& todo: add PLU != true */ &&
                x.OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentScoreID == OnlandVisualTrashAssessmentScore.A.OnlandVisualTrashAssessmentScoreID).Select(x => x.TrashGeneratingUnitGeometry.Area).Sum();

            var totalAcresCaptured = fullTrashCapture + equivalentArea;

            var totalPLUAcres = trashGeneratingUnits.Where(
                x => x.StormwaterJurisdictionID == jurisdiction.StormwaterJurisdictionID /*&& todo add PLU == true */).Select(x => x.TrashGeneratingUnitGeometry.Area).Sum();

            return Json(new AreaBasedAcreCalculationsSimple());
        }
    }
}
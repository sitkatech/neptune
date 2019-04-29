using System.Web.Mvc;
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


            return Json(new AreaBasedAcreCalculationsSimple())
        }
    }
}
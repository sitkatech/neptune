using System;
using Neptune.Web.Common;
using Neptune.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace Neptune.Web.Controllers
{
    public class PowerBIController : NeptuneBaseController
    {
        [HttpGet]
        [AllowAnonymous]
        public JsonResult GetHRUCharacteristicsForPowerBI (WebServiceToken webServiceToken, int? treatmentBMPID = null)
        {
            var data = HttpRequestStorage.DatabaseEntities.HRUCharacteristics.Where(x => x.TreatmentBMPID == (treatmentBMPID ?? x.TreatmentBMPID)).ToList()
                .Select(x => new PowerBIHRUCharacteristics(){ 
                    HRUEntityType = x.TreatmentBMP != null ? "Treatment BMP" :
                                     (x.WaterQualityManagementPlan != null ? "Water Quality Management Plan" : "Regional Subbasin"),
                    HydrologicSoilGroup = x.HydrologicSoilGroup,
                    ImperviousAcres = Math.Round(x.ImperviousAcres, 3),
                    LastUpdated = x.LastUpdated.ToLongDateString(),
                    LSPCLandUseDescription = x.HRUCharacteristicLandUseCode.HRUCharacteristicLandUseCodeDisplayName,
                    RegionalSubbasin = x.RegionalSubbasin != null ? x.RegionalSubbasin.Watershed + " - " + x.RegionalSubbasin.DrainID : "N/A",
                    SlopePercentage = x.SlopePercentage,
                    TotalAcres = Math.Round(x.Area, 3),
                    TreatmentBMP = x.TreatmentBMP?.TreatmentBMPName ?? "N/A",
                    WaterQualityManagementPlan = x.WaterQualityManagementPlan?.WaterQualityManagementPlanName ?? "N/A"
                });

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
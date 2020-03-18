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
            var data = HttpRequestStorage.DatabaseEntities.HRUCharacteristics.Where(x => treatmentBMPID == null || x.GetTreatmentBMP().TreatmentBMPID == treatmentBMPID ).ToList()
                .Select(x => new PowerBIHRUCharacteristics(){ 
                    HRUEntityType = x.GetTreatmentBMP() != null ? "Treatment BMP" :
                                     (x.GetWaterQualityManagementPlan() != null ? "Water Quality Management Plan" : "Regional Subbasin"),
                    HydrologicSoilGroup = x.HydrologicSoilGroup,
                    ImperviousAcres = Math.Round(x.ImperviousAcres, 3),
                    LastUpdated = x.LastUpdated.ToLongDateString(),
                    LSPCLandUseDescription = x.HRUCharacteristicLandUseCode.HRUCharacteristicLandUseCodeDisplayName,
                    RegionalSubbasin = x.GetRegionalSubbasin() != null ? x.GetRegionalSubbasin().Watershed + " - " + x.GetRegionalSubbasin().DrainID : "N/A",
                    SlopePercentage = x.SlopePercentage,
                    TotalAcres = Math.Round(x.Area, 3),
                    TreatmentBMP = x.GetTreatmentBMP()?.TreatmentBMPName ?? "N/A",
                    WaterQualityManagementPlan = x.GetWaterQualityManagementPlan()?.WaterQualityManagementPlanName ?? "N/A"
                });

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult TreatmentBMPAttributeSummary(WebServiceToken webServiceToken)
        {
            var data = HttpRequestStorage.DatabaseEntities.TreatmentBMPs.Select(x => new
            {
                x.TreatmentBMPID,
                LocationLon = x.LocationPoint4326.XCoordinate,
                LocationLat = x.LocationPoint4326.YCoordinate,
                Watershed = x.WatershedID,
                x.WaterQualityManagementPlanID
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult WaterQualityManagementPlanAttributeSummary(WebServiceToken webServiceToken)
        {
            // this to-list ought to be okay
            var data = HttpRequestStorage.DatabaseEntities.vPowerBIWaterQualityManagementPlans.ToList().Select(x => new
            {
                x.WaterQualityManagementPlanID,
                Name = x.WaterQualityManagementPlanName,
                Jurisdiction = x.OrganizationName,
                Status = x.WaterQualityManagementPlanStatusDisplayName,
                DevelopmentType = x.WaterQualityManagementPlanDevelopmentTypeDisplayName,
                LandUse = x.WaterQualityManagementPlanLandUseDisplayName,
                PermitTerm = x.WaterQualityManagementPlanPermitTermDisplayName,
                ApprovalDate = x.ApprovalDate,
                DateOfConstruction = x.DateOfConstruction,
                HydromodificationApplies = x.HydromodificationAppliesDisplayName,
                HydrologicSubarea = x.HydrologicSubareaName,
                x.RecordedWQMPAreaInAcres,
                TrashCaptureStatus = x.TrashCaptureStatusTypeDisplayName,
                x.TrashCaptureEffectiveness
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
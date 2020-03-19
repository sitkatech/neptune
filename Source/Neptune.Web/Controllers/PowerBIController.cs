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
        public JsonResult TreatmentBMPParameterizationSummary(WebServiceToken webServiceToken)
        {
            var data = HttpRequestStorage.DatabaseEntities.TreatmentBMPs
                .Where(x => x.TreatmentBMPType.IsAnalyzedInModelingModule).ToList().Select(x => new
                {
                    x.TreatmentBMPID,
                    x.TreatmentBMPName,
                    x.TreatmentBMPType.TreatmentBMPTypeName,
                    FullyParameterized = x.IsFullyParameterized() ? "Yes" : "No"
                });

            return new JsonResult()
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = int.MaxValue
            };
        }


        [HttpGet]
        [AllowAnonymous]
        public JsonResult TreatmentBMPAttributeSummary(WebServiceToken webServiceToken)
        {
            var data = HttpRequestStorage.DatabaseEntities.vPowerBITreatmentBMPs.Select(x => new TreatmentBMPForPowerBI
            {
                PrimaryKey = x.PrimaryKey,
                TreatmentBMPName = x.TreatmentBMPName,
                Jurisdiction = x.Jurisdiction,
                LocationLon = x.LocationLon,
                LocationLat = x.LocationLat,
                Watershed = x.Watershed,
                DelineationType = x.DelineationType,
                WaterQualityManagementPlanID = x.WaterQualityManagementPlanID,
                TreatmentBMPModelingAttributeID = x.TreatmentBMPModelingAttributeID,
                TreatmentBMPID = x.PrimaryKey,
                TreatmentBMPTypeName = x.TreatmentBMPTypeName,
                UpstreamTreatmentBMPID = x.UpstreamTreatmentBMPID,
                AverageDivertedFlowrate = x.AverageDivertedFlowrate,
                AverageTreatmentFlowrate = x.AverageTreatmentFlowrate,
                DesignDryWeatherTreatmentCapacity = x.DesignDryWeatherTreatmentCapacity,
                DesignLowFlowDiversionCapacity = x.DesignLowFlowDiversionCapacity,
                DesignMediaFiltrationRate = x.DesignMediaFiltrationRate,
                DesignResidenceTimeforPermanentPool = x.DesignResidenceTimeforPermanentPool,
                DiversionRate = x.DiversionRate,
                DrawdownTimeforWQDetentionVolume = x.DrawdownTimeforWQDetentionVolume,
                EffectiveFootprint = x.EffectiveFootprint,
                EffectiveRetentionDepth = x.EffectiveRetentionDepth,
                InfiltrationDischargeRate = x.InfiltrationDischargeRate,
                InfiltrationSurfaceArea = x.InfiltrationSurfaceArea,
                MediaBedFootprint = x.MediaBedFootprint,
                PermanentPoolorWetlandVolume = x.PermanentPoolorWetlandVolume,
                RoutingConfigurationID = x.RoutingConfigurationID,
                StorageVolumeBelowLowestOutletElevation = x.StorageVolumeBelowLowestOutletElevation,
                SummerHarvestedWaterDemand = x.SummerHarvestedWaterDemand,
                TimeOfConcentrationID = x.TimeOfConcentrationID,
                DrawdownTimeForDetentionVolume = x.DrawdownTimeForDetentionVolume,
                TotalEffectiveBMPVolume = x.TotalEffectiveBMPVolume,
                TotalEffectiveDrywellBMPVolume = x.TotalEffectiveDrywellBMPVolume,
                TreatmentRate = x.TreatmentRate,
                UnderlyingHydrologicSoilGroupID = x.UnderlyingHydrologicSoilGroupID,
                UnderlyingInfiltrationRate = x.UnderlyingInfiltrationRate,
                WaterQualityDetentionVolume = x.WaterQualityDetentionVolume,
                WettedFootprint = x.WettedFootprint,
                WinterHarvestedWaterDemand = x.WinterHarvestedWaterDemand
            });

            return new JsonResult()
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = int.MaxValue
            };
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


        [HttpGet]
        [AllowAnonymous]
        public JsonResult LandUseStatistics(WebServiceToken webServiceToken)
        {
            var data = HttpRequestStorage.DatabaseEntities.vPowerBILandUseStatistics.ToList();


            return new JsonResult()
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = int.MaxValue
            };
        }
    }

    public class TreatmentBMPForPowerBI
    {
        public int PrimaryKey { get; set; }
        public double? LocationLon { get; set; }
        public double? LocationLat { get; set; }
        public string Watershed { get; set; }
        public int? WaterQualityManagementPlanID { get; set; }
        public int? TreatmentBMPModelingAttributeID { get; set; }
        public int TreatmentBMPID { get; set; }
        public int? UpstreamTreatmentBMPID { get; set; }
        public double? AverageDivertedFlowrate { get; set; }
        public double? AverageTreatmentFlowrate { get; set; }
        public double? DesignDryWeatherTreatmentCapacity { get; set; }
        public double? DesignLowFlowDiversionCapacity { get; set; }
        public double? DesignMediaFiltrationRate { get; set; }
        public double? DesignResidenceTimeforPermanentPool { get; set; }
        public double? DiversionRate { get; set; }
        public double? DrawdownTimeforWQDetentionVolume { get; set; }
        public double? EffectiveFootprint { get; set; }
        public double? EffectiveRetentionDepth { get; set; }
        public double? InfiltrationDischargeRate { get; set; }
        public double? InfiltrationSurfaceArea { get; set; }
        public double? MediaBedFootprint { get; set; }
        public double? PermanentPoolorWetlandVolume { get; set; }
        public int? RoutingConfigurationID { get; set; }
        public double? StorageVolumeBelowLowestOutletElevation { get; set; }
        public double? SummerHarvestedWaterDemand { get; set; }
        public int? TimeOfConcentrationID { get; set; }
        public double? DrawdownTimeForDetentionVolume { get; set; }
        public double? TotalEffectiveBMPVolume { get; set; }
        public double? TotalEffectiveDrywellBMPVolume { get; set; }
        public double? TreatmentRate { get; set; }
        public int? UnderlyingHydrologicSoilGroupID { get; set; }
        public double? UnderlyingInfiltrationRate { get; set; }
        public double? WaterQualityDetentionVolume { get; set; }
        public double? WettedFootprint { get; set; }
        public double? WinterHarvestedWaterDemand { get; set; }
        public string DelineationType { get; set; }
        public string TreatmentBMPTypeName { get; set; }
        public string TreatmentBMPName { get; set; }
        public string Jurisdiction { get; set; }
    }
}
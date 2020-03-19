//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vPowerBITreatmentBMP]
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public partial class vPowerBITreatmentBMP
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vPowerBITreatmentBMP()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vPowerBITreatmentBMP(int primaryKey, string treatmentBMPName, string treatmentBMPTypeName, double? locationLon, double? locationLat, string watershed, int? upstreamBMPID, string delineationType, int? waterQualityManagementPlanID, int? treatmentBMPModelingAttributeID, int? treatmentBMPID, int? upstreamTreatmentBMPID, double? averageDivertedFlowrate, double? averageTreatmentFlowrate, double? designDryWeatherTreatmentCapacity, double? designLowFlowDiversionCapacity, double? designMediaFiltrationRate, double? designResidenceTimeforPermanentPool, double? diversionRate, double? drawdownTimeforWQDetentionVolume, double? effectiveFootprint, double? effectiveRetentionDepth, double? infiltrationDischargeRate, double? infiltrationSurfaceArea, double? mediaBedFootprint, double? permanentPoolorWetlandVolume, int? routingConfigurationID, double? storageVolumeBelowLowestOutletElevation, double? summerHarvestedWaterDemand, int? timeOfConcentrationID, double? drawdownTimeForDetentionVolume, double? totalEffectiveBMPVolume, double? totalEffectiveDrywellBMPVolume, double? treatmentRate, int? underlyingHydrologicSoilGroupID, double? underlyingInfiltrationRate, double? waterQualityDetentionVolume, double? wettedFootprint, double? winterHarvestedWaterDemand) : this()
        {
            this.PrimaryKey = primaryKey;
            this.TreatmentBMPName = treatmentBMPName;
            this.TreatmentBMPTypeName = treatmentBMPTypeName;
            this.LocationLon = locationLon;
            this.LocationLat = locationLat;
            this.Watershed = watershed;
            this.UpstreamBMPID = upstreamBMPID;
            this.DelineationType = delineationType;
            this.WaterQualityManagementPlanID = waterQualityManagementPlanID;
            this.TreatmentBMPModelingAttributeID = treatmentBMPModelingAttributeID;
            this.TreatmentBMPID = treatmentBMPID;
            this.UpstreamTreatmentBMPID = upstreamTreatmentBMPID;
            this.AverageDivertedFlowrate = averageDivertedFlowrate;
            this.AverageTreatmentFlowrate = averageTreatmentFlowrate;
            this.DesignDryWeatherTreatmentCapacity = designDryWeatherTreatmentCapacity;
            this.DesignLowFlowDiversionCapacity = designLowFlowDiversionCapacity;
            this.DesignMediaFiltrationRate = designMediaFiltrationRate;
            this.DesignResidenceTimeforPermanentPool = designResidenceTimeforPermanentPool;
            this.DiversionRate = diversionRate;
            this.DrawdownTimeforWQDetentionVolume = drawdownTimeforWQDetentionVolume;
            this.EffectiveFootprint = effectiveFootprint;
            this.EffectiveRetentionDepth = effectiveRetentionDepth;
            this.InfiltrationDischargeRate = infiltrationDischargeRate;
            this.InfiltrationSurfaceArea = infiltrationSurfaceArea;
            this.MediaBedFootprint = mediaBedFootprint;
            this.PermanentPoolorWetlandVolume = permanentPoolorWetlandVolume;
            this.RoutingConfigurationID = routingConfigurationID;
            this.StorageVolumeBelowLowestOutletElevation = storageVolumeBelowLowestOutletElevation;
            this.SummerHarvestedWaterDemand = summerHarvestedWaterDemand;
            this.TimeOfConcentrationID = timeOfConcentrationID;
            this.DrawdownTimeForDetentionVolume = drawdownTimeForDetentionVolume;
            this.TotalEffectiveBMPVolume = totalEffectiveBMPVolume;
            this.TotalEffectiveDrywellBMPVolume = totalEffectiveDrywellBMPVolume;
            this.TreatmentRate = treatmentRate;
            this.UnderlyingHydrologicSoilGroupID = underlyingHydrologicSoilGroupID;
            this.UnderlyingInfiltrationRate = underlyingInfiltrationRate;
            this.WaterQualityDetentionVolume = waterQualityDetentionVolume;
            this.WettedFootprint = wettedFootprint;
            this.WinterHarvestedWaterDemand = winterHarvestedWaterDemand;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vPowerBITreatmentBMP(vPowerBITreatmentBMP vPowerBITreatmentBMP) : this()
        {
            this.PrimaryKey = vPowerBITreatmentBMP.PrimaryKey;
            this.TreatmentBMPName = vPowerBITreatmentBMP.TreatmentBMPName;
            this.TreatmentBMPTypeName = vPowerBITreatmentBMP.TreatmentBMPTypeName;
            this.LocationLon = vPowerBITreatmentBMP.LocationLon;
            this.LocationLat = vPowerBITreatmentBMP.LocationLat;
            this.Watershed = vPowerBITreatmentBMP.Watershed;
            this.UpstreamBMPID = vPowerBITreatmentBMP.UpstreamBMPID;
            this.DelineationType = vPowerBITreatmentBMP.DelineationType;
            this.WaterQualityManagementPlanID = vPowerBITreatmentBMP.WaterQualityManagementPlanID;
            this.TreatmentBMPModelingAttributeID = vPowerBITreatmentBMP.TreatmentBMPModelingAttributeID;
            this.TreatmentBMPID = vPowerBITreatmentBMP.TreatmentBMPID;
            this.UpstreamTreatmentBMPID = vPowerBITreatmentBMP.UpstreamTreatmentBMPID;
            this.AverageDivertedFlowrate = vPowerBITreatmentBMP.AverageDivertedFlowrate;
            this.AverageTreatmentFlowrate = vPowerBITreatmentBMP.AverageTreatmentFlowrate;
            this.DesignDryWeatherTreatmentCapacity = vPowerBITreatmentBMP.DesignDryWeatherTreatmentCapacity;
            this.DesignLowFlowDiversionCapacity = vPowerBITreatmentBMP.DesignLowFlowDiversionCapacity;
            this.DesignMediaFiltrationRate = vPowerBITreatmentBMP.DesignMediaFiltrationRate;
            this.DesignResidenceTimeforPermanentPool = vPowerBITreatmentBMP.DesignResidenceTimeforPermanentPool;
            this.DiversionRate = vPowerBITreatmentBMP.DiversionRate;
            this.DrawdownTimeforWQDetentionVolume = vPowerBITreatmentBMP.DrawdownTimeforWQDetentionVolume;
            this.EffectiveFootprint = vPowerBITreatmentBMP.EffectiveFootprint;
            this.EffectiveRetentionDepth = vPowerBITreatmentBMP.EffectiveRetentionDepth;
            this.InfiltrationDischargeRate = vPowerBITreatmentBMP.InfiltrationDischargeRate;
            this.InfiltrationSurfaceArea = vPowerBITreatmentBMP.InfiltrationSurfaceArea;
            this.MediaBedFootprint = vPowerBITreatmentBMP.MediaBedFootprint;
            this.PermanentPoolorWetlandVolume = vPowerBITreatmentBMP.PermanentPoolorWetlandVolume;
            this.RoutingConfigurationID = vPowerBITreatmentBMP.RoutingConfigurationID;
            this.StorageVolumeBelowLowestOutletElevation = vPowerBITreatmentBMP.StorageVolumeBelowLowestOutletElevation;
            this.SummerHarvestedWaterDemand = vPowerBITreatmentBMP.SummerHarvestedWaterDemand;
            this.TimeOfConcentrationID = vPowerBITreatmentBMP.TimeOfConcentrationID;
            this.DrawdownTimeForDetentionVolume = vPowerBITreatmentBMP.DrawdownTimeForDetentionVolume;
            this.TotalEffectiveBMPVolume = vPowerBITreatmentBMP.TotalEffectiveBMPVolume;
            this.TotalEffectiveDrywellBMPVolume = vPowerBITreatmentBMP.TotalEffectiveDrywellBMPVolume;
            this.TreatmentRate = vPowerBITreatmentBMP.TreatmentRate;
            this.UnderlyingHydrologicSoilGroupID = vPowerBITreatmentBMP.UnderlyingHydrologicSoilGroupID;
            this.UnderlyingInfiltrationRate = vPowerBITreatmentBMP.UnderlyingInfiltrationRate;
            this.WaterQualityDetentionVolume = vPowerBITreatmentBMP.WaterQualityDetentionVolume;
            this.WettedFootprint = vPowerBITreatmentBMP.WettedFootprint;
            this.WinterHarvestedWaterDemand = vPowerBITreatmentBMP.WinterHarvestedWaterDemand;
            CallAfterConstructor(vPowerBITreatmentBMP);
        }

        partial void CallAfterConstructor(vPowerBITreatmentBMP vPowerBITreatmentBMP);

        public int PrimaryKey { get; set; }
        public string TreatmentBMPName { get; set; }
        public string TreatmentBMPTypeName { get; set; }
        public double? LocationLon { get; set; }
        public double? LocationLat { get; set; }
        public string Watershed { get; set; }
        public int? UpstreamBMPID { get; set; }
        public string DelineationType { get; set; }
        public int? WaterQualityManagementPlanID { get; set; }
        public int? TreatmentBMPModelingAttributeID { get; set; }
        public int? TreatmentBMPID { get; set; }
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
    }
}
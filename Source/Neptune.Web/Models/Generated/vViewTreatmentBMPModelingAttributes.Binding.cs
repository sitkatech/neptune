//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vViewTreatmentBMPModelingAttributes]
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
    public partial class vViewTreatmentBMPModelingAttributes
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vViewTreatmentBMPModelingAttributes()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vViewTreatmentBMPModelingAttributes(int primaryKey, string treatmentBMPName, int treatmentBMPTypeID, string treatmentBMPTypeName, int stormwaterJurisdictionID, int? upstreamTreatmentBMPID, double? averageDivertedFlowrate, double? averageTreatmentFlowrate, double? designDryWeatherTreatmentCapacity, double? designLowFlowDiversionCapacity, double? designMediaFiltrationRate, double? designResidenceTimeforPermanentPool, double? diversionRate, double? drawdownTimeforWQDetentionVolume, double? effectiveFootprint, double? effectiveRetentionDepth, double? infiltrationDischargeRate, double? infiltrationSurfaceArea, double? mediaBedFootprint, double? permanentPoolorWetlandVolume, int? routingConfigurationID, double? storageVolumeBelowLowestOutletElevation, double? summerHarvestedWaterDemand, int? timeOfConcentrationID, double? totalDrawdownTime, double? totalEffectiveBMPVolume, double? totalEffectiveDrywellBMPVolume, double? treatmentRate, int? underlyingHydrologicSoilGroupID, double? underlyingInfiltrationRate, double? waterQualityDetentionVolume, double? wettedFootprint, double? winterHarvestedWaterDemand) : this()
        {
            this.PrimaryKey = primaryKey;
            this.TreatmentBMPName = treatmentBMPName;
            this.TreatmentBMPTypeID = treatmentBMPTypeID;
            this.TreatmentBMPTypeName = treatmentBMPTypeName;
            this.StormwaterJurisdictionID = stormwaterJurisdictionID;
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
            this.TotalDrawdownTime = totalDrawdownTime;
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
        public vViewTreatmentBMPModelingAttributes(vViewTreatmentBMPModelingAttributes vViewTreatmentBMPModelingAttributes) : this()
        {
            this.PrimaryKey = vViewTreatmentBMPModelingAttributes.PrimaryKey;
            this.TreatmentBMPName = vViewTreatmentBMPModelingAttributes.TreatmentBMPName;
            this.TreatmentBMPTypeID = vViewTreatmentBMPModelingAttributes.TreatmentBMPTypeID;
            this.TreatmentBMPTypeName = vViewTreatmentBMPModelingAttributes.TreatmentBMPTypeName;
            this.StormwaterJurisdictionID = vViewTreatmentBMPModelingAttributes.StormwaterJurisdictionID;
            this.UpstreamTreatmentBMPID = vViewTreatmentBMPModelingAttributes.UpstreamTreatmentBMPID;
            this.AverageDivertedFlowrate = vViewTreatmentBMPModelingAttributes.AverageDivertedFlowrate;
            this.AverageTreatmentFlowrate = vViewTreatmentBMPModelingAttributes.AverageTreatmentFlowrate;
            this.DesignDryWeatherTreatmentCapacity = vViewTreatmentBMPModelingAttributes.DesignDryWeatherTreatmentCapacity;
            this.DesignLowFlowDiversionCapacity = vViewTreatmentBMPModelingAttributes.DesignLowFlowDiversionCapacity;
            this.DesignMediaFiltrationRate = vViewTreatmentBMPModelingAttributes.DesignMediaFiltrationRate;
            this.DesignResidenceTimeforPermanentPool = vViewTreatmentBMPModelingAttributes.DesignResidenceTimeforPermanentPool;
            this.DiversionRate = vViewTreatmentBMPModelingAttributes.DiversionRate;
            this.DrawdownTimeforWQDetentionVolume = vViewTreatmentBMPModelingAttributes.DrawdownTimeforWQDetentionVolume;
            this.EffectiveFootprint = vViewTreatmentBMPModelingAttributes.EffectiveFootprint;
            this.EffectiveRetentionDepth = vViewTreatmentBMPModelingAttributes.EffectiveRetentionDepth;
            this.InfiltrationDischargeRate = vViewTreatmentBMPModelingAttributes.InfiltrationDischargeRate;
            this.InfiltrationSurfaceArea = vViewTreatmentBMPModelingAttributes.InfiltrationSurfaceArea;
            this.MediaBedFootprint = vViewTreatmentBMPModelingAttributes.MediaBedFootprint;
            this.PermanentPoolorWetlandVolume = vViewTreatmentBMPModelingAttributes.PermanentPoolorWetlandVolume;
            this.RoutingConfigurationID = vViewTreatmentBMPModelingAttributes.RoutingConfigurationID;
            this.StorageVolumeBelowLowestOutletElevation = vViewTreatmentBMPModelingAttributes.StorageVolumeBelowLowestOutletElevation;
            this.SummerHarvestedWaterDemand = vViewTreatmentBMPModelingAttributes.SummerHarvestedWaterDemand;
            this.TimeOfConcentrationID = vViewTreatmentBMPModelingAttributes.TimeOfConcentrationID;
            this.TotalDrawdownTime = vViewTreatmentBMPModelingAttributes.TotalDrawdownTime;
            this.TotalEffectiveBMPVolume = vViewTreatmentBMPModelingAttributes.TotalEffectiveBMPVolume;
            this.TotalEffectiveDrywellBMPVolume = vViewTreatmentBMPModelingAttributes.TotalEffectiveDrywellBMPVolume;
            this.TreatmentRate = vViewTreatmentBMPModelingAttributes.TreatmentRate;
            this.UnderlyingHydrologicSoilGroupID = vViewTreatmentBMPModelingAttributes.UnderlyingHydrologicSoilGroupID;
            this.UnderlyingInfiltrationRate = vViewTreatmentBMPModelingAttributes.UnderlyingInfiltrationRate;
            this.WaterQualityDetentionVolume = vViewTreatmentBMPModelingAttributes.WaterQualityDetentionVolume;
            this.WettedFootprint = vViewTreatmentBMPModelingAttributes.WettedFootprint;
            this.WinterHarvestedWaterDemand = vViewTreatmentBMPModelingAttributes.WinterHarvestedWaterDemand;
            CallAfterConstructor(vViewTreatmentBMPModelingAttributes);
        }

        partial void CallAfterConstructor(vViewTreatmentBMPModelingAttributes vViewTreatmentBMPModelingAttributes);

        public int PrimaryKey { get; set; }
        public string TreatmentBMPName { get; set; }
        public int TreatmentBMPTypeID { get; set; }
        public string TreatmentBMPTypeName { get; set; }
        public int StormwaterJurisdictionID { get; set; }
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
        public double? TotalDrawdownTime { get; set; }
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
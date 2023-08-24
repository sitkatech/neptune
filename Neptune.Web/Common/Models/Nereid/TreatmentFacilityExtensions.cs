﻿using Neptune.EFModels.Entities;

namespace Neptune.Web.Common.Models.Nereid;

public static class TreatmentFacilityExtensions
{
    public static TreatmentFacility ToTreatmentFacility(this TreatmentBMP treatmentBMP, bool isBaselineCondition)
    {
        var treatmentBMPNodeID = NereidUtilities.TreatmentBMPNodeID(treatmentBMP.TreatmentBMPID);
        var modelBasinKey = treatmentBMP.ModelBasin?.ModelBasinKey.ToString();
        var isFullyParameterized = true;// todo: treatmentBMP.IsFullyParameterized();
        double? treatmentRate = null;
        var modelingAttribute = treatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP;

        // in the baseline condition, anything built after 2003 is treated as if it doesn't exist.
        if (!isFullyParameterized || 
            (isBaselineCondition && treatmentBMP.YearBuilt.HasValue && treatmentBMP.YearBuilt.Value > NereidUtilities.BASELINE_CUTOFF_YEAR))
        {
            return new TreatmentFacility
            {
                NodeID = treatmentBMPNodeID,
                FacilityType = "NoTreatment",
                ReferenceDataKey = modelBasinKey,
                DesignStormwaterDepth = treatmentBMP.PrecipitationZone?.DesignStormwaterDepthInInches ?? .8,
                EliminateAllDryWeatherFlowOverride = modelingAttribute?.DryWeatherFlowOverrideID == DryWeatherFlowOverride.Yes.DryWeatherFlowOverrideID
            };
        }
            
        // treatment rate is an alias for four different fields, so we need to pick the one that's not null
        if (modelingAttribute.InfiltrationDischargeRate != null)
        {
            treatmentRate = modelingAttribute.InfiltrationDischargeRate;
        }
        else if (modelingAttribute.TreatmentRate != null)
        {
            treatmentRate = modelingAttribute.TreatmentRate;
        }
        else if (modelingAttribute.AverageDivertedFlowrate != null)
        {
            // AverageDivertedFlowrate is collected in gallons per day instead of CFS, but we need to send CFS to Nereid.
            treatmentRate = modelingAttribute.AverageDivertedFlowrate * NereidUtilities.GPD_TO_CFS;
        }
        else if (modelingAttribute.AverageTreatmentFlowrate != null)
        {
            treatmentRate = modelingAttribute.AverageTreatmentFlowrate;
        }

        double? area = null;

        // area_sqft is an alias for four different fields, so we need to take the one that's not null
        if (modelingAttribute.EffectiveFootprint != null)
        {
            area = modelingAttribute.EffectiveFootprint;
        }
        else if (modelingAttribute.InfiltrationSurfaceArea != null)
        {
            area = modelingAttribute.InfiltrationSurfaceArea;
        }
        else if (modelingAttribute.MediaBedFootprint != null)
        {
            area = modelingAttribute.MediaBedFootprint;
        }
        else if (modelingAttribute.WettedFootprint != null)
        {
            area = modelingAttribute.WettedFootprint;
        }

        double? designCapacity = null;

        if (modelingAttribute.DesignDryWeatherTreatmentCapacity != null)
        {
            designCapacity = modelingAttribute.DesignDryWeatherTreatmentCapacity;
        }
        else if (modelingAttribute.DesignLowFlowDiversionCapacity != null)
        {
            // DesignLowFlowDiversionCapacity is collected in GPD, so convert to CFS
            designCapacity = modelingAttribute.DesignLowFlowDiversionCapacity * NereidUtilities.GPD_TO_CFS;
        }

        if (designCapacity == null)
        {
            designCapacity = treatmentRate;
        }
        else if (treatmentRate == null)
        {
            treatmentRate = designCapacity;
        }

        // null-coalescence here represents default values.
        // It's generally an anti-pattern for an API to require its clients to insert default values,
        // since the API should be written to replace nulls/absent values with defaults,
        // but it's so few that it's not worth a revision to the modeling engine at this time (4/16/2020)
        var treatmentFacility = new TreatmentFacility
        {
            NodeID = treatmentBMPNodeID,
            FacilityType = treatmentBMP.TreatmentBMPType.TreatmentBMPModelingType.TreatmentBMPModelingTypeName,
            ReferenceDataKey = modelBasinKey,
            DesignStormwaterDepth = treatmentBMP.PrecipitationZone?.DesignStormwaterDepthInInches,
            DesignCapacity = designCapacity,
            DesignMediaFiltrationRate = modelingAttribute.DesignMediaFiltrationRate,
            //convert Days to Hours for this field.
            DiversionRate = modelingAttribute.DiversionRate,
            DrawdownTimeforWQDetentionVolume = modelingAttribute.DrawdownTimeforWQDetentionVolume,
            Area = area,
            EffectiveRetentionDepth = modelingAttribute.EffectiveRetentionDepth,
            MonthsOfOperation = modelingAttribute.MonthsOfOperation?.MonthsOfOperationNereidAlias ?? MonthsOfOperation.Both.MonthsOfOperationNereidAlias,
            PermanentPoolorWetlandVolume = modelingAttribute.PermanentPoolorWetlandVolume,
            RoutingConfiguration = modelingAttribute.RoutingConfigurationID == RoutingConfiguration.Online.RoutingConfigurationID,
            StorageVolumeBelowLowestOutletElevation = modelingAttribute.StorageVolumeBelowLowestOutletElevation,
            // SummerHarvestedWaterDemand is collected in GPD, so convert to CFS
            SummerHarvestedWaterDemand = modelingAttribute.SummerHarvestedWaterDemand  * NereidUtilities.GPD_TO_CFS,
            TimeOfConcentration = modelingAttribute.TimeOfConcentration?.TimeOfConcentrationDisplayName ?? TimeOfConcentration.FiveMinutes.TimeOfConcentrationDisplayName,
            TotalDrawdownTime = modelingAttribute.DrawdownTimeforWQDetentionVolume,
            TotalEffectiveBMPVolume = modelingAttribute.TotalEffectiveBMPVolume,
            TreatmentRate = treatmentRate,
            UnderlyingHydrologicSoilGroup = modelingAttribute.UnderlyingHydrologicSoilGroup?.UnderlyingHydrologicSoilGroupDisplayName.ToLower() ?? UnderlyingHydrologicSoilGroup.D.UnderlyingHydrologicSoilGroupDisplayName.ToLower(),
            UnderlyingInfiltrationRate = modelingAttribute.UnderlyingInfiltrationRate,
            UpstreamBMP = modelingAttribute.UpstreamTreatmentBMPID.HasValue ? NereidUtilities.TreatmentBMPNodeID(modelingAttribute.UpstreamTreatmentBMPID.Value) : null,
            WaterQualityDetentionVolume = modelingAttribute.WaterQualityDetentionVolume,
            // WinterHarvestedWaterDemand is collected in GPD, so convert to CFS
            WinterHarvestedWaterDemand = modelingAttribute.WinterHarvestedWaterDemand * NereidUtilities.GPD_TO_CFS,
            EliminateAllDryWeatherFlowOverride = modelingAttribute.DryWeatherFlowOverrideID == DryWeatherFlowOverride.Yes.DryWeatherFlowOverrideID
        };
        return treatmentFacility;
    }
}
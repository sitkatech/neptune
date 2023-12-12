using Neptune.Common;
using Neptune.EFModels.Entities;

namespace Neptune.EFModels.Nereid;

public static class TreatmentFacilityExtensions
{
    public static TreatmentFacility ToTreatmentFacility(this TreatmentBMP treatmentBMP, bool isBaselineCondition,
        Dictionary<int, int> modelBasins, Dictionary<int, double> precipitationZones)
    {
        var treatmentBMPNodeID = NereidUtilities.TreatmentBMPNodeID(treatmentBMP.TreatmentBMPID);
        var modelBasinKey = treatmentBMP.ModelBasinID.HasValue && modelBasins.TryGetValue(treatmentBMP.ModelBasinID.Value, out var modelBasinID) ? modelBasinID.ToString() : null;
        var isFullyParameterized = treatmentBMP.IsFullyParameterized(treatmentBMP.Delineation);
        double? treatmentRate = null;
        double? area = null;
        double? designCapacity = null;

        var treatmentBMPModelingAttribute = treatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP;

        // in the baseline condition, anything built after 2003 is treated as if it doesn't exist.
        var designStormwaterDepth =  treatmentBMP.PrecipitationZoneID.HasValue && precipitationZones.TryGetValue(treatmentBMP.PrecipitationZoneID.Value, out var designStormwaterDepthInInches) ? designStormwaterDepthInInches : (double?)null;
        if (!isFullyParameterized || 
            (isBaselineCondition && treatmentBMP.YearBuilt.HasValue && treatmentBMP.YearBuilt.Value > Constants.NEREID_BASELINE_CUTOFF_YEAR))
        {
            return new TreatmentFacility
            {
                NodeID = treatmentBMPNodeID,
                FacilityType = "NoTreatment",
                ReferenceDataKey = modelBasinKey,
                DesignStormwaterDepth = designStormwaterDepth ?? .8,
                EliminateAllDryWeatherFlowOverride = treatmentBMPModelingAttribute?.DryWeatherFlowOverrideID == DryWeatherFlowOverride.Yes.DryWeatherFlowOverrideID
            };
        }

        if (treatmentBMP.TreatmentBMPType.TreatmentBMPModelingTypeID.HasValue && treatmentBMPModelingAttribute != null)
        {
            // treatment rate is an alias for four different fields: InfiltrationDischargeRate, TreatmentRate, AverageTreatmentFlowrate, AverageDivertedFlowrate 
            // area is an alias for four different fields: EffectiveFootprint, MediaBedFootprint, InfiltrationSurfaceArea, WettedFootprint
            // designCapacity is an alias for two different fields: DesignDryWeatherTreatmentCapacity, DesignLowFlowDiversionCapacity

            switch (treatmentBMP.TreatmentBMPType.TreatmentBMPModelingTypeID.Value)
            {
                case (int)TreatmentBMPModelingTypeEnum.DryExtendedDetentionBasin:
                case (int)TreatmentBMPModelingTypeEnum.FlowDurationControlBasin:
                case (int)TreatmentBMPModelingTypeEnum.FlowDurationControlTank:
                    area = treatmentBMPModelingAttribute.EffectiveFootprint;
                    break;
                case (int)TreatmentBMPModelingTypeEnum.BioinfiltrationBioretentionWithRaisedUnderdrain:
                    area = treatmentBMPModelingAttribute.MediaBedFootprint;
                    break;
                case (int) TreatmentBMPModelingTypeEnum.BioretentionWithNoUnderdrain:
                case (int) TreatmentBMPModelingTypeEnum.InfiltrationBasin:
                case (int) TreatmentBMPModelingTypeEnum.InfiltrationTrench:
                case (int) TreatmentBMPModelingTypeEnum.PermeablePavement:
                case (int) TreatmentBMPModelingTypeEnum.UndergroundInfiltration:
                    area = treatmentBMPModelingAttribute.InfiltrationSurfaceArea;
                    break;
                case (int) TreatmentBMPModelingTypeEnum.BioretentionWithUnderdrainAndImperviousLiner:
                case (int) TreatmentBMPModelingTypeEnum.SandFilters:
                    area = treatmentBMPModelingAttribute.MediaBedFootprint;
                    break;
                case (int) TreatmentBMPModelingTypeEnum.Drywell:
                    treatmentRate = treatmentBMPModelingAttribute.InfiltrationDischargeRate;
                    break;
                case (int) TreatmentBMPModelingTypeEnum.DryWeatherTreatmentSystems:
                    treatmentRate = treatmentBMPModelingAttribute.AverageTreatmentFlowrate;
                    designCapacity = treatmentBMPModelingAttribute.DesignDryWeatherTreatmentCapacity;
                    break;
                case (int) TreatmentBMPModelingTypeEnum.LowFlowDiversions:
                    // AverageDivertedFlowrate is collected in gallons per day instead of CFS, but we need to send CFS to Nereid.
                    treatmentRate = treatmentBMPModelingAttribute.AverageDivertedFlowrate * Constants.GPD_TO_CFS;
                    // DesignLowFlowDiversionCapacity is collected in GPD, so convert to CFS
                    designCapacity = treatmentBMPModelingAttribute.DesignLowFlowDiversionCapacity * Constants.GPD_TO_CFS;
                    break;
                case (int) TreatmentBMPModelingTypeEnum.HydrodynamicSeparator:
                case (int) TreatmentBMPModelingTypeEnum.ProprietaryBiotreatment:
                case (int) TreatmentBMPModelingTypeEnum.ProprietaryTreatmentControl:
                    treatmentRate = treatmentBMPModelingAttribute.TreatmentRate;
                    break;
                case (int) TreatmentBMPModelingTypeEnum.VegetatedFilterStrip:
                case (int) TreatmentBMPModelingTypeEnum.VegetatedSwale:
                    treatmentRate = treatmentBMPModelingAttribute.TreatmentRate;
                    area = treatmentBMPModelingAttribute.WettedFootprint;
                    break;
            }
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
            DesignStormwaterDepth = designStormwaterDepth,
            DesignCapacity = designCapacity,
            DesignMediaFiltrationRate = treatmentBMPModelingAttribute.DesignMediaFiltrationRate,
            //convert Days to Hours for this field.
            DiversionRate = treatmentBMPModelingAttribute.DiversionRate,
            DrawdownTimeforWQDetentionVolume = treatmentBMPModelingAttribute.DrawdownTimeForWQDetentionVolume,
            Area = area,
            EffectiveRetentionDepth = treatmentBMPModelingAttribute.EffectiveRetentionDepth,
            MonthsOfOperation = treatmentBMPModelingAttribute.MonthsOfOperation?.MonthsOfOperationNereidAlias ?? MonthsOfOperation.Both.MonthsOfOperationNereidAlias,
            PermanentPoolorWetlandVolume = treatmentBMPModelingAttribute.PermanentPoolOrWetlandVolume,
            RoutingConfiguration = treatmentBMPModelingAttribute.RoutingConfigurationID == RoutingConfiguration.Online.RoutingConfigurationID,
            StorageVolumeBelowLowestOutletElevation = treatmentBMPModelingAttribute.StorageVolumeBelowLowestOutletElevation,
            // SummerHarvestedWaterDemand is collected in GPD, so convert to CFS
            SummerHarvestedWaterDemand = treatmentBMPModelingAttribute.SummerHarvestedWaterDemand  * Constants.GPD_TO_CFS,
            TimeOfConcentration = treatmentBMPModelingAttribute.TimeOfConcentration?.TimeOfConcentrationDisplayName ?? TimeOfConcentration.FiveMinutes.TimeOfConcentrationDisplayName,
            TotalDrawdownTime = treatmentBMPModelingAttribute.DrawdownTimeForWQDetentionVolume,
            TotalEffectiveBMPVolume = treatmentBMPModelingAttribute.TotalEffectiveBMPVolume,
            TreatmentRate = treatmentRate,
            UnderlyingHydrologicSoilGroup = treatmentBMPModelingAttribute.UnderlyingHydrologicSoilGroup?.UnderlyingHydrologicSoilGroupDisplayName.ToLower() ?? UnderlyingHydrologicSoilGroup.D.UnderlyingHydrologicSoilGroupDisplayName.ToLower(),
            UnderlyingInfiltrationRate = treatmentBMPModelingAttribute.UnderlyingInfiltrationRate,
            UpstreamBMP = treatmentBMPModelingAttribute.UpstreamTreatmentBMPID.HasValue ? NereidUtilities.TreatmentBMPNodeID(treatmentBMPModelingAttribute.UpstreamTreatmentBMPID.Value) : null,
            WaterQualityDetentionVolume = treatmentBMPModelingAttribute.WaterQualityDetentionVolume,
            // WinterHarvestedWaterDemand is collected in GPD, so convert to CFS
            WinterHarvestedWaterDemand = treatmentBMPModelingAttribute.WinterHarvestedWaterDemand * Constants.GPD_TO_CFS,
            EliminateAllDryWeatherFlowOverride = treatmentBMPModelingAttribute.DryWeatherFlowOverrideID == DryWeatherFlowOverride.Yes.DryWeatherFlowOverrideID
        };
        return treatmentFacility;
    }
}
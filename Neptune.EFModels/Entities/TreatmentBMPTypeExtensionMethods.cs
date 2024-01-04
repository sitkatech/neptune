using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static partial class TreatmentBMPTypeExtensionMethods
{
    public static TreatmentBMPTypeDisplayDto AsDisplayDto(this TreatmentBMPType treatmentBMPType)
    {
        var treatmentBMPTypeDisplayDto = new TreatmentBMPTypeDisplayDto()
        {
            TreatmentBMPTypeID = treatmentBMPType.TreatmentBMPTypeID,
            TreatmentBMPTypeName = treatmentBMPType.TreatmentBMPTypeName,
        };
        return treatmentBMPTypeDisplayDto;
    }

    public static bool HasMissingModelingAttributes(this TreatmentBMPType treatmentBMPType, ITreatmentBMPModelingAttribute? treatmentBMPModelingAttribute)
    {
        if (!treatmentBMPType.IsAnalyzedInModelingModule)
        {
            return false;
        }

        var treatmentBMPModelingTypeEnum = treatmentBMPType.TreatmentBMPModelingType.ToEnum;
        if (treatmentBMPModelingAttribute != null)
        {
            var hasNoRoutingConfigurationAndOffline = !treatmentBMPModelingAttribute.RoutingConfigurationID.HasValue ||
                                                      treatmentBMPModelingAttribute is { RoutingConfigurationID: (int)RoutingConfigurationEnum.Offline, DiversionRate: null };
            switch (treatmentBMPModelingTypeEnum)
            {
                case TreatmentBMPModelingTypeEnum.BioinfiltrationBioretentionWithRaisedUnderdrain:
                    return hasNoRoutingConfigurationAndOffline ||
                           !treatmentBMPModelingAttribute.TotalEffectiveBMPVolume.HasValue ||
                           !treatmentBMPModelingAttribute.StorageVolumeBelowLowestOutletElevation.HasValue ||
                           !treatmentBMPModelingAttribute.MediaBedFootprint.HasValue ||
                           !treatmentBMPModelingAttribute.DesignMediaFiltrationRate.HasValue;
                case TreatmentBMPModelingTypeEnum.BioretentionWithNoUnderdrain:
                case TreatmentBMPModelingTypeEnum.InfiltrationBasin:
                case TreatmentBMPModelingTypeEnum.InfiltrationTrench:
                case TreatmentBMPModelingTypeEnum.PermeablePavement:
                case TreatmentBMPModelingTypeEnum.UndergroundInfiltration:
                    return hasNoRoutingConfigurationAndOffline ||
                           !treatmentBMPModelingAttribute.TotalEffectiveBMPVolume.HasValue ||
                           !treatmentBMPModelingAttribute.InfiltrationSurfaceArea.HasValue ||
                           !treatmentBMPModelingAttribute.UnderlyingInfiltrationRate.HasValue;
                case TreatmentBMPModelingTypeEnum.BioretentionWithUnderdrainAndImperviousLiner:
                case TreatmentBMPModelingTypeEnum.SandFilters:
                    return hasNoRoutingConfigurationAndOffline ||
                           !treatmentBMPModelingAttribute.TotalEffectiveBMPVolume.HasValue ||
                           !treatmentBMPModelingAttribute.MediaBedFootprint.HasValue ||
                           !treatmentBMPModelingAttribute.DesignMediaFiltrationRate.HasValue;
                case TreatmentBMPModelingTypeEnum.CisternsForHarvestAndUse:
                    return hasNoRoutingConfigurationAndOffline ||
                           !treatmentBMPModelingAttribute.TotalEffectiveBMPVolume.HasValue ||
                           !treatmentBMPModelingAttribute.WinterHarvestedWaterDemand.HasValue ||
                           !treatmentBMPModelingAttribute.SummerHarvestedWaterDemand.HasValue;
                case TreatmentBMPModelingTypeEnum.ConstructedWetland:
                case TreatmentBMPModelingTypeEnum.WetDetentionBasin:
                    return hasNoRoutingConfigurationAndOffline ||
                           !treatmentBMPModelingAttribute.PermanentPoolOrWetlandVolume.HasValue ||
                           !treatmentBMPModelingAttribute.WaterQualityDetentionVolume.HasValue;
                case TreatmentBMPModelingTypeEnum.DryExtendedDetentionBasin:
                case TreatmentBMPModelingTypeEnum.FlowDurationControlBasin:
                case TreatmentBMPModelingTypeEnum.FlowDurationControlTank:
                    return hasNoRoutingConfigurationAndOffline ||
                           !treatmentBMPModelingAttribute.TotalEffectiveBMPVolume.HasValue ||
                           !treatmentBMPModelingAttribute.StorageVolumeBelowLowestOutletElevation.HasValue ||
                           !treatmentBMPModelingAttribute.EffectiveFootprint.HasValue ||
                           !treatmentBMPModelingAttribute.DrawdownTimeForWQDetentionVolume.HasValue;
                case TreatmentBMPModelingTypeEnum.DryWeatherTreatmentSystems:
                    return !treatmentBMPModelingAttribute.DesignDryWeatherTreatmentCapacity.HasValue &&
                           !treatmentBMPModelingAttribute.AverageTreatmentFlowrate.HasValue;
                case TreatmentBMPModelingTypeEnum.Drywell:
                    return hasNoRoutingConfigurationAndOffline ||
                           !treatmentBMPModelingAttribute.TotalEffectiveDrywellBMPVolume.HasValue ||
                           !treatmentBMPModelingAttribute.InfiltrationDischargeRate.HasValue;
                case TreatmentBMPModelingTypeEnum.HydrodynamicSeparator:
                case TreatmentBMPModelingTypeEnum.ProprietaryBiotreatment:
                case TreatmentBMPModelingTypeEnum.ProprietaryTreatmentControl:
                    return !treatmentBMPModelingAttribute.TreatmentRate.HasValue;
                case TreatmentBMPModelingTypeEnum.LowFlowDiversions:
                    return !treatmentBMPModelingAttribute.DesignLowFlowDiversionCapacity.HasValue &&
                           !treatmentBMPModelingAttribute.AverageDivertedFlowrate.HasValue;
                case TreatmentBMPModelingTypeEnum.VegetatedFilterStrip:
                case TreatmentBMPModelingTypeEnum.VegetatedSwale:
                    return hasNoRoutingConfigurationAndOffline ||
                           !treatmentBMPModelingAttribute.TreatmentRate.HasValue ||
                           !treatmentBMPModelingAttribute.WettedFootprint.HasValue ||
                           !treatmentBMPModelingAttribute.EffectiveRetentionDepth.HasValue;
                default:
                    throw new ArgumentOutOfRangeException(
                        $"Unknown TreatmentBMPModelingTypeEnum + {treatmentBMPModelingTypeEnum}");
            }
        }

        return true;
    }

    public static List<TreatmentBMPModelingAttributeDefinitionDto> GetModelingAttributes(this TreatmentBMPType treatmentBMPType)
    {
        var modelingAttributes = new List<TreatmentBMPModelingAttributeDefinitionDto>();
        if (!treatmentBMPType.IsAnalyzedInModelingModule)
        {
            return modelingAttributes;
        }

        var treatmentBMPModelingTypeEnum = treatmentBMPType.TreatmentBMPModelingType.ToEnum;
        switch (treatmentBMPModelingTypeEnum)
        {
            case TreatmentBMPModelingTypeEnum.BioinfiltrationBioretentionWithRaisedUnderdrain:
                modelingAttributes.Add(new TreatmentBMPModelingAttributeDefinitionDto("TotalEffectiveBMPVolume", "cu ft"));
                modelingAttributes.Add(new TreatmentBMPModelingAttributeDefinitionDto("StorageVolumeBelowLowestOutletElevation", "cu ft"));
                modelingAttributes.Add(new TreatmentBMPModelingAttributeDefinitionDto("MediaBedFootprint", "sq ft"));
                modelingAttributes.Add(new TreatmentBMPModelingAttributeDefinitionDto("DesignMediaFiltrationRate", "in/hr"));
                modelingAttributes.Add(new TreatmentBMPModelingAttributeDefinitionDto("UnderlyingHydrologicSoilGroupID", null));
//                modelingAttributes.Add(new TreatmentBMPModelingAttributeDefinitionDto("DryWeatherFlowOverrideID", null));
                break;
            case TreatmentBMPModelingTypeEnum.BioretentionWithNoUnderdrain:
            case TreatmentBMPModelingTypeEnum.InfiltrationBasin:
            case TreatmentBMPModelingTypeEnum.InfiltrationTrench:
            case TreatmentBMPModelingTypeEnum.PermeablePavement:
            case TreatmentBMPModelingTypeEnum.UndergroundInfiltration:
                modelingAttributes.Add(new TreatmentBMPModelingAttributeDefinitionDto("TotalEffectiveBMPVolume", "cu ft"));
                modelingAttributes.Add(new TreatmentBMPModelingAttributeDefinitionDto("InfiltrationSurfaceArea", "sq ft"));
                modelingAttributes.Add(new TreatmentBMPModelingAttributeDefinitionDto("UnderlyingInfiltrationRate", "in/hr"));
//                modelingAttributes.Add(new TreatmentBMPModelingAttributeDefinitionDto("DryWeatherFlowOverrideID", null));
                break;
            case TreatmentBMPModelingTypeEnum.BioretentionWithUnderdrainAndImperviousLiner:
            case TreatmentBMPModelingTypeEnum.SandFilters:
                modelingAttributes.Add(new TreatmentBMPModelingAttributeDefinitionDto("TotalEffectiveBMPVolume", "cu ft"));
                modelingAttributes.Add(new TreatmentBMPModelingAttributeDefinitionDto("MediaBedFootprint", "sq ft"));
                modelingAttributes.Add(new TreatmentBMPModelingAttributeDefinitionDto("DesignMediaFiltrationRate", "in/hr"));
//                modelingAttributes.Add(new TreatmentBMPModelingAttributeDefinitionDto("DryWeatherFlowOverrideID", null));
                break;
            case TreatmentBMPModelingTypeEnum.CisternsForHarvestAndUse:
                modelingAttributes.Add(new TreatmentBMPModelingAttributeDefinitionDto("TotalEffectiveBMPVolume", "cu ft"));
                modelingAttributes.Add(new TreatmentBMPModelingAttributeDefinitionDto("WinterHarvestedWaterDemand", "gpd"));
                modelingAttributes.Add(new TreatmentBMPModelingAttributeDefinitionDto("SummerHarvestedWaterDemand", "gpd"));
//                modelingAttributes.Add(new TreatmentBMPModelingAttributeDefinitionDto("DryWeatherFlowOverrideID", null));
                break;
            case TreatmentBMPModelingTypeEnum.ConstructedWetland:
            case TreatmentBMPModelingTypeEnum.WetDetentionBasin:
                modelingAttributes.Add(new TreatmentBMPModelingAttributeDefinitionDto("PermanentPoolOrWetlandVolume", "cu ft"));
                modelingAttributes.Add(new TreatmentBMPModelingAttributeDefinitionDto("WaterQualityDetentionVolume", "cu ft"));
//                modelingAttributes.Add(new TreatmentBMPModelingAttributeDefinitionDto("DryWeatherFlowOverrideID", null));
                break;
            case TreatmentBMPModelingTypeEnum.DryExtendedDetentionBasin:
            case TreatmentBMPModelingTypeEnum.FlowDurationControlBasin:
            case TreatmentBMPModelingTypeEnum.FlowDurationControlTank:
                modelingAttributes.Add(new TreatmentBMPModelingAttributeDefinitionDto("TotalEffectiveBMPVolume", "cu ft"));
                modelingAttributes.Add(new TreatmentBMPModelingAttributeDefinitionDto("StorageVolumeBelowLowestOutletElevation", "cu ft"));
                modelingAttributes.Add(new TreatmentBMPModelingAttributeDefinitionDto("EffectiveFootprint", "sq ft"));
                modelingAttributes.Add(new TreatmentBMPModelingAttributeDefinitionDto("DrawdownTimeForWQDetentionVolume", "hours"));
                modelingAttributes.Add(new TreatmentBMPModelingAttributeDefinitionDto("UnderlyingHydrologicSoilGroupID", null));
//                modelingAttributes.Add(new TreatmentBMPModelingAttributeDefinitionDto("DryWeatherFlowOverrideID", null));
                break;
            case TreatmentBMPModelingTypeEnum.DryWeatherTreatmentSystems:
                modelingAttributes.Add(new TreatmentBMPModelingAttributeDefinitionDto("DesignDryWeatherTreatmentCapacity", "cfs"));
                modelingAttributes.Add(new TreatmentBMPModelingAttributeDefinitionDto("AverageTreatmentFlowrate", "cfs"));
                modelingAttributes.Add(new TreatmentBMPModelingAttributeDefinitionDto("MonthsOfOperationID", null));
//                modelingAttributes.Add(new TreatmentBMPModelingAttributeDefinitionDto("DryWeatherFlowOverrideID", null));
                break;
            case TreatmentBMPModelingTypeEnum.Drywell:
                modelingAttributes.Add(new TreatmentBMPModelingAttributeDefinitionDto("TotalEffectiveDrywellBMPVolume", "cu ft"));
                modelingAttributes.Add(new TreatmentBMPModelingAttributeDefinitionDto("InfiltrationDischargeRate", "cfs"));
                modelingAttributes.Add(new TreatmentBMPModelingAttributeDefinitionDto("TimeOfConcentrationID", null));
//                modelingAttributes.Add(new TreatmentBMPModelingAttributeDefinitionDto("DryWeatherFlowOverrideID", null));
                break;
            case TreatmentBMPModelingTypeEnum.HydrodynamicSeparator:
            case TreatmentBMPModelingTypeEnum.ProprietaryBiotreatment:
            case TreatmentBMPModelingTypeEnum.ProprietaryTreatmentControl:
                modelingAttributes.Add(new TreatmentBMPModelingAttributeDefinitionDto("TreatmentRate", "cfs"));
                modelingAttributes.Add(new TreatmentBMPModelingAttributeDefinitionDto("TimeOfConcentrationID", null));
//                modelingAttributes.Add(new TreatmentBMPModelingAttributeDefinitionDto("DryWeatherFlowOverrideID", null));
                break;
            case TreatmentBMPModelingTypeEnum.LowFlowDiversions:
                modelingAttributes.Add(new TreatmentBMPModelingAttributeDefinitionDto("DesignLowFlowDiversionCapacity", "gpd"));
                modelingAttributes.Add(new TreatmentBMPModelingAttributeDefinitionDto("AverageDivertedFlowrate", "gpd"));
                modelingAttributes.Add(new TreatmentBMPModelingAttributeDefinitionDto("MonthsOfOperationID", null));
//                modelingAttributes.Add(new TreatmentBMPModelingAttributeDefinitionDto("DryWeatherFlowOverrideID", null));
                break;
            case TreatmentBMPModelingTypeEnum.VegetatedFilterStrip:
            case TreatmentBMPModelingTypeEnum.VegetatedSwale:
                modelingAttributes.Add(new TreatmentBMPModelingAttributeDefinitionDto("TimeOfConcentrationID", null));
                modelingAttributes.Add(new TreatmentBMPModelingAttributeDefinitionDto("TreatmentRate", "cfs"));
                modelingAttributes.Add(new TreatmentBMPModelingAttributeDefinitionDto("WettedFootprint", "sq ft"));
                modelingAttributes.Add(new TreatmentBMPModelingAttributeDefinitionDto("EffectiveRetentionDepth", "ft"));
                modelingAttributes.Add(new TreatmentBMPModelingAttributeDefinitionDto("UnderlyingHydrologicSoilGroupID", null));
//                modelingAttributes.Add(new TreatmentBMPModelingAttributeDefinitionDto("DryWeatherFlowOverrideID", null));
                break;
            default:
                throw new ArgumentOutOfRangeException($"Unknown TreatmentBMPModelingTypeEnum + {treatmentBMPModelingTypeEnum}");
        }

        return modelingAttributes;
    }
}
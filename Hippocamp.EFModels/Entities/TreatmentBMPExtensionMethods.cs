using Hippocamp.Models.DataTransferObjects;


namespace Hippocamp.EFModels.Entities
{
    public static partial class TreatmentBMPExtensionMethods
    {
        public static TreatmentBMPUpsertDto AsUpsertDto(this TreatmentBMP treatmentBMP)
        {
            var treatmentBMPUpsertDto = new TreatmentBMPUpsertDto()
            {
                TreatmentBMPID = treatmentBMP.TreatmentBMPID,
                TreatmentBMPName = treatmentBMP.TreatmentBMPName,
                TreatmentBMPTypeID = treatmentBMP.TreatmentBMPTypeID,
                TreatmentBMPTypeName = treatmentBMP.TreatmentBMPType.TreatmentBMPTypeName,
                WatershedName = treatmentBMP.Watershed.WatershedName,
                StormwaterJurisdictionName = treatmentBMP.StormwaterJurisdiction.Organization.OrganizationName,
                Longitude = treatmentBMP.Longitude,
                Latitude = treatmentBMP.Latitude,
                Notes = treatmentBMP.Notes,

                //AverageDivertedFlowrate
                //AverageTreatmentFlowrate
                //DesignDryWeatherTreatmentCapacity
                //DesignLowFlowDiversionCapacity
                //DesignMediaFiltrationRate
                //DesignResidenceTimeforPermanentPool
                //DiversionRate
                //DrawdownTimeforWQDetentionVolume
                //EffectiveFootprint
                //EffectiveRetentionDepth
                //InfiltrationDischargeRate
                //InfiltrationSurfaceArea
                //MediaBedFootprint
                //PermanentPoolorWetlandVolume
                //RoutingConfigurationID
                //StorageVolumeBelowLowestOutletElevation
                //SummerHarvestedWaterDemand
                //TimeOfConcentrationID
                //DrawdownTimeForDetentionVolume
                //TotalEffectiveBMPVolume
                //TotalEffectiveDrywellBMPVolume
                //TreatmentRate
                //UnderlyingHydrologicSoilGroupID
                //UnderlyingInfiltrationRate
                //WaterQualityDetentionVolume
                //WettedFootprint
                //WinterHarvestedWaterDemand
                //MonthsOfOperationID
                //DryWeatherFlowOverrideID
            };

            return treatmentBMPUpsertDto;
        }

        public static TreatmentBMPUpsertDto AsUpsertDtoWithModelingAttributes(this TreatmentBMP treatmentBMP, TreatmentBMPModelingAttribute? treatmentBMPModelingAttribute)
        {
            var treatmentBMPUpsertDto = new TreatmentBMPUpsertDto()
            {
                TreatmentBMPID = treatmentBMP.TreatmentBMPID,
                TreatmentBMPName = treatmentBMP.TreatmentBMPName,
                TreatmentBMPTypeID = treatmentBMP.TreatmentBMPTypeID,
                TreatmentBMPTypeName = treatmentBMP.TreatmentBMPType.TreatmentBMPTypeName,
                WatershedName = treatmentBMP.Watershed.WatershedName,
                StormwaterJurisdictionName = treatmentBMP.StormwaterJurisdiction.Organization.OrganizationName,
                Longitude = treatmentBMP.Longitude,
                Latitude = treatmentBMP.Latitude,
                Notes = treatmentBMP.Notes,
                AverageDivertedFlowrate = treatmentBMPModelingAttribute?.AverageDivertedFlowrate,
                AverageTreatmentFlowrate = treatmentBMPModelingAttribute?.AverageTreatmentFlowrate,
                DesignDryWeatherTreatmentCapacity = treatmentBMPModelingAttribute?.DesignDryWeatherTreatmentCapacity,
                DesignLowFlowDiversionCapacity = treatmentBMPModelingAttribute?.DesignLowFlowDiversionCapacity,
                DesignMediaFiltrationRate = treatmentBMPModelingAttribute?.DesignMediaFiltrationRate,
                DesignResidenceTimeforPermanentPool = treatmentBMPModelingAttribute?.DesignResidenceTimeforPermanentPool,
                DiversionRate = treatmentBMPModelingAttribute?.DiversionRate,
                DrawdownTimeforWQDetentionVolume = treatmentBMPModelingAttribute?.DrawdownTimeforWQDetentionVolume,
                EffectiveFootprint = treatmentBMPModelingAttribute?.EffectiveFootprint,
                EffectiveRetentionDepth = treatmentBMPModelingAttribute?.EffectiveRetentionDepth,
                InfiltrationDischargeRate = treatmentBMPModelingAttribute?.InfiltrationDischargeRate,
                InfiltrationSurfaceArea = treatmentBMPModelingAttribute?.InfiltrationSurfaceArea,
                MediaBedFootprint = treatmentBMPModelingAttribute?.MediaBedFootprint,
                PermanentPoolorWetlandVolume = treatmentBMPModelingAttribute?.PermanentPoolorWetlandVolume,
                RoutingConfiguration = treatmentBMPModelingAttribute?.RoutingConfiguration?.AsDto(),
                StorageVolumeBelowLowestOutletElevation = treatmentBMPModelingAttribute?.StorageVolumeBelowLowestOutletElevation,
                SummerHarvestedWaterDemand = treatmentBMPModelingAttribute?.SummerHarvestedWaterDemand,
                TimeOfConcentration = treatmentBMPModelingAttribute?.TimeOfConcentration?.AsDto(),
                DrawdownTimeForDetentionVolume = treatmentBMPModelingAttribute?.DrawdownTimeForDetentionVolume,
                TotalEffectiveBMPVolume = treatmentBMPModelingAttribute?.TotalEffectiveBMPVolume,
                TotalEffectiveDrywellBMPVolume = treatmentBMPModelingAttribute?.TotalEffectiveDrywellBMPVolume,
                TreatmentRate = treatmentBMPModelingAttribute?.TreatmentRate,
                UnderlyingHydrologicSoilGroup = treatmentBMPModelingAttribute?.UnderlyingHydrologicSoilGroup?.AsDto(),
                UnderlyingInfiltrationRate = treatmentBMPModelingAttribute?.UnderlyingInfiltrationRate,
                WaterQualityDetentionVolume = treatmentBMPModelingAttribute?.WaterQualityDetentionVolume,
                WettedFootprint = treatmentBMPModelingAttribute?.WettedFootprint,
                WinterHarvestedWaterDemand = treatmentBMPModelingAttribute?.WinterHarvestedWaterDemand,
                MonthsOfOperation = treatmentBMPModelingAttribute?.MonthsOfOperation?.AsDto(),
                DryWeatherFlowOverride = treatmentBMPModelingAttribute?.DryWeatherFlowOverride?.AsDto()
            };

            return treatmentBMPUpsertDto;
        }

        public static TreatmentBMPDisplayDto AsDisplayDto(this TreatmentBMP treatmentBMP)
        {
            var treatmentBMPDisplayDto = new TreatmentBMPDisplayDto()
            {
                TreatmentBMPName = treatmentBMP.TreatmentBMPName,
                TreatmentBMPTypeName = treatmentBMP.TreatmentBMPType.TreatmentBMPTypeName,
                Longitude = treatmentBMP.Longitude,
                Latitude = treatmentBMP.Latitude
            };

            return treatmentBMPDisplayDto;
        }
    }
}
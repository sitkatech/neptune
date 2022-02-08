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
                Notes = treatmentBMP.Notes
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
                TreatmentBMPModelingTypeID = treatmentBMP.TreatmentBMPType.TreatmentBMPModelingTypeID,
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
                RoutingConfigurationID = treatmentBMPModelingAttribute?.RoutingConfigurationID,
                StorageVolumeBelowLowestOutletElevation = treatmentBMPModelingAttribute?.StorageVolumeBelowLowestOutletElevation,
                SummerHarvestedWaterDemand = treatmentBMPModelingAttribute?.SummerHarvestedWaterDemand,
                TimeOfConcentrationID = treatmentBMPModelingAttribute?.TimeOfConcentrationID,
                DrawdownTimeForDetentionVolume = treatmentBMPModelingAttribute?.DrawdownTimeForDetentionVolume,
                TotalEffectiveBMPVolume = treatmentBMPModelingAttribute?.TotalEffectiveBMPVolume,
                TotalEffectiveDrywellBMPVolume = treatmentBMPModelingAttribute?.TotalEffectiveDrywellBMPVolume,
                TreatmentRate = treatmentBMPModelingAttribute?.TreatmentRate,
                UnderlyingHydrologicSoilGroupID = treatmentBMPModelingAttribute?.UnderlyingHydrologicSoilGroupID,
                UnderlyingInfiltrationRate = treatmentBMPModelingAttribute?.UnderlyingInfiltrationRate,
                WaterQualityDetentionVolume = treatmentBMPModelingAttribute?.WaterQualityDetentionVolume,
                WettedFootprint = treatmentBMPModelingAttribute?.WettedFootprint,
                WinterHarvestedWaterDemand = treatmentBMPModelingAttribute?.WinterHarvestedWaterDemand,
                MonthsOfOperationID = treatmentBMPModelingAttribute?.MonthsOfOperationID,
                DryWeatherFlowOverrideID = treatmentBMPModelingAttribute?.DryWeatherFlowOverrideID
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
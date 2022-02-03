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
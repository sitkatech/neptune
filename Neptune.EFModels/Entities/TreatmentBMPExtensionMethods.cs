using Microsoft.EntityFrameworkCore;
using Neptune.Models.DataTransferObjects;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities;

public static partial class TreatmentBMPExtensionMethods
{
    public static TreatmentBMPDisplayDto AsDisplayDto(this TreatmentBMP treatmentBMP)
    {
        var treatmentBMPSimpleDto = new TreatmentBMPDisplayDto()
        {
            TreatmentBMPID = treatmentBMP.TreatmentBMPID,
            DisplayName = treatmentBMP.TreatmentBMPName,
            TreatmentBMPTypeName = treatmentBMP.TreatmentBMPType.TreatmentBMPTypeName,
            ProjectID = treatmentBMP.ProjectID,
            InventoryIsVerified = treatmentBMP.InventoryIsVerified,
            Longitude = treatmentBMP.LocationPoint4326.Coordinate.X,
            Latitude = treatmentBMP.LocationPoint4326.Coordinate.Y,
            TreatmentBMPName = treatmentBMP.TreatmentBMPName,
            IsFullyParameterized = treatmentBMP.IsFullyParameterized(treatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP)
        };
        return treatmentBMPSimpleDto;
    }

    public static TreatmentBMPUpsertDto AsUpsertDtoWithModelingAttributes(this TreatmentBMP treatmentBMP, TreatmentBMPModelingAttribute treatmentBMPModelingAttribute)
    {
        var treatmentBMPUpsertDto = new TreatmentBMPUpsertDto()
        {
            TreatmentBMPID = treatmentBMP.TreatmentBMPID,
            TreatmentBMPName = treatmentBMP.TreatmentBMPName,
            TreatmentBMPTypeID = treatmentBMP.TreatmentBMPTypeID,
            TreatmentBMPTypeName = treatmentBMP.TreatmentBMPType.TreatmentBMPTypeName,
            TreatmentBMPModelingTypeID = treatmentBMP.TreatmentBMPType.TreatmentBMPModelingTypeID,
            WatershedName = treatmentBMP.Watershed?.WatershedName,
            StormwaterJurisdictionName = treatmentBMP.StormwaterJurisdiction.Organization.OrganizationName,
            Longitude = treatmentBMP.LocationPoint4326?.Coordinate.X,
            Latitude = treatmentBMP.LocationPoint4326?.Coordinate.Y,
            Notes = treatmentBMP.Notes,
            AverageDivertedFlowrate = treatmentBMPModelingAttribute?.AverageDivertedFlowrate,
            AverageTreatmentFlowrate = treatmentBMPModelingAttribute?.AverageTreatmentFlowrate,
            DesignDryWeatherTreatmentCapacity = treatmentBMPModelingAttribute?.DesignDryWeatherTreatmentCapacity,
            DesignLowFlowDiversionCapacity = treatmentBMPModelingAttribute?.DesignLowFlowDiversionCapacity,
            DesignMediaFiltrationRate = treatmentBMPModelingAttribute?.DesignMediaFiltrationRate,
            DiversionRate = treatmentBMPModelingAttribute?.DiversionRate,
            DrawdownTimeForWQDetentionVolume = treatmentBMPModelingAttribute?.DrawdownTimeForWQDetentionVolume,
            EffectiveFootprint = treatmentBMPModelingAttribute?.EffectiveFootprint,
            EffectiveRetentionDepth = treatmentBMPModelingAttribute?.EffectiveRetentionDepth,
            InfiltrationDischargeRate = treatmentBMPModelingAttribute?.InfiltrationDischargeRate,
            InfiltrationSurfaceArea = treatmentBMPModelingAttribute?.InfiltrationSurfaceArea,
            MediaBedFootprint = treatmentBMPModelingAttribute?.MediaBedFootprint,
            PermanentPoolOrWetlandVolume = treatmentBMPModelingAttribute?.PermanentPoolOrWetlandVolume,
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
            DryWeatherFlowOverrideID = treatmentBMPModelingAttribute?.DryWeatherFlowOverrideID,
            AreAllModelingAttributesComplete = !treatmentBMP.TreatmentBMPType.HasMissingModelingAttributes(treatmentBMPModelingAttribute),
            IsFullyParameterized = treatmentBMP.IsFullyParameterized(treatmentBMPModelingAttribute)
        };

        return treatmentBMPUpsertDto;
    }


    public static WaterQualityManagementPlanVerifyTreatmentBMPSimpleDto AsWaterQualityManagementPlanVerifyTreatmentBMPSimpleDto(this TreatmentBMP treatmentBMP)
    {
        var waterQualityManagementPlanVerifyTreatmentBMPSimpleDto = new WaterQualityManagementPlanVerifyTreatmentBMPSimpleDto()
        {
            TreatmentBMPName = treatmentBMP.TreatmentBMPName,
            TreatmentBMPID = treatmentBMP.TreatmentBMPID,
            TreatmentBMPType = treatmentBMP.TreatmentBMPType.TreatmentBMPTypeName,
        };
        //var mostRecentFieldVisit = treatmentBMP.FieldVisit.Where(x => x.FieldVisitStatus == FieldVisitStatus.Complete).OrderByDescending(x => x.VisitDate).FirstOrDefault();
        //waterQualityManagementPlanVerifyTreatmentBMPSimpleDto.FieldVisiLastVisitedtDate = mostRecentFieldVisit?.VisitDate.ToShortDateString();
        //waterQualityManagementPlanVerifyTreatmentBMPSimpleDto.FieldVisitMostRecentScore = mostRecentFieldVisit?.GetPostMaintenanceAssessment() != null ? mostRecentFieldVisit.GetPostMaintenanceAssessment().FormattedScore() : mostRecentFieldVisit?.GetInitialAssessment()?.FormattedScore();

        return waterQualityManagementPlanVerifyTreatmentBMPSimpleDto;
    }

    public static bool IsFullyParameterized(this TreatmentBMP treatmentBMP, Delineation? delineation)
    {
        // Planning BMPs don't need verified delineations
        // assumes the delineation passed in is the from the "upstreamest" BMP
        if (treatmentBMP.ProjectID == null && !(delineation?.IsVerified ?? false))
        {
            return false;
        }

        var treatmentBMPType = treatmentBMP.TreatmentBMPType;
        var treatmentBMPModelingAttribute = treatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP;
        return !treatmentBMPType.HasMissingModelingAttributes(treatmentBMPModelingAttribute);
    }

    public static bool IsFullyParameterized(this TreatmentBMP treatmentBMP, TreatmentBMPModelingAttribute bmpModelingAttributes)
    {
        if (treatmentBMP.TreatmentBMPType.TreatmentBMPModelingTypeID == null)
        {
            return false;
        }

        if (treatmentBMP.Delineation == null)
        {
            return false;
        }

        return !treatmentBMP.TreatmentBMPType.HasMissingModelingAttributes(bmpModelingAttributes);
    }

    public static void SetTreatmentBMPPointInPolygonDataByLocationPoint(this TreatmentBMP treatmentBMP,
        Geometry locationPoint, NeptuneDbContext dbContext)
    {
        treatmentBMP.WatershedID = dbContext.Watersheds.AsNoTracking()
            .FirstOrDefault(x => locationPoint.Intersects(x.WatershedGeometry))?.WatershedID;
        treatmentBMP.ModelBasinID = dbContext.ModelBasins.AsNoTracking()
            .FirstOrDefault(x => locationPoint.Intersects(x.ModelBasinGeometry))?.ModelBasinID;
        treatmentBMP.PrecipitationZoneID = dbContext.PrecipitationZones.AsNoTracking()
            .FirstOrDefault(x => locationPoint.Intersects(x.PrecipitationZoneGeometry))?.PrecipitationZoneID;
        treatmentBMP.RegionalSubbasinID = dbContext.RegionalSubbasins.AsNoTracking()
            .FirstOrDefault(x => locationPoint.Intersects(x.CatchmentGeometry))?.RegionalSubbasinID;
    }
}
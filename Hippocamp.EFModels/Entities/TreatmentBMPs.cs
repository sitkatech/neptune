﻿using System.Collections.Generic;
using System.Linq;
using Hippocamp.API.Util;
using Hippocamp.Models.DataTransferObjects;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    public partial class TreatmentBMPs
    {
        private static IQueryable<TreatmentBMP> GetTreatmentBMPsImpl(HippocampDbContext dbContext)
        {
            return dbContext.TreatmentBMPs
                .Include(x => x.StormwaterJurisdiction).ThenInclude(x => x.Organization)
                .Include(x => x.TreatmentBMPType)
                .Include(x => x.Watershed)
                .AsNoTracking();
        }

        public static List<TreatmentBMPUpsertDto> ListByProjectIDAsUpsertDto(HippocampDbContext dbContext, int projectID)
        {
            var treatmentBMPs = GetTreatmentBMPsImpl(dbContext)
                .Where(x => x.ProjectID == projectID).ToList();

            var treatmentBMPIDs = treatmentBMPs.Select(x => x.TreatmentBMPID).ToList();

            var treatmentBMPModelingAttributes = dbContext.TreatmentBMPModelingAttributes
                .Where(x => treatmentBMPIDs.Contains(x.TreatmentBMPID)).ToList();

            var treatmentBMPUpsertDtos = treatmentBMPs
                .GroupJoin(treatmentBMPModelingAttributes,
                    x => x.TreatmentBMPID,
                    y => y.TreatmentBMPID,
                    (x, y) => new {TreatmentBMP = x, TreatmentBmpModelingAttribute = y.SingleOrDefault()})
                .Select(x => x.TreatmentBMP.AsUpsertDtoWithModelingAttributes(x.TreatmentBmpModelingAttribute))
                .ToList();

            return treatmentBMPUpsertDtos;
        }

        public static List<TreatmentBMPDisplayDto> ListAsDisplayDto(HippocampDbContext dbContext)
        {
            var treatmentBMPDisplayDtos = GetTreatmentBMPsImpl(dbContext)
                .Select(x => x.AsDisplayDto())
                .ToList();

            return treatmentBMPDisplayDtos;
        }

        public static List<TreatmentBMPTypeSimpleDto> ListTypesAsSimpleDto(HippocampDbContext dbContext)
        {
            var treatmentBMPTypeSimpleDtos = dbContext.TreatmentBMPTypes.Select(x => x.AsSimpleDto()).ToList();
            return treatmentBMPTypeSimpleDtos;
        }

        public static List<TreatmentBMPModelingAttributeDropdownItemDto> GetModelingAttributeDropdownItemsAsDto(HippocampDbContext dbContext)
        {
            var treatmentBMPModelingAttributeDropdownItemDtos = new List<TreatmentBMPModelingAttributeDropdownItemDto>();

            var timeOfConcentrationDropdownItemDtos = dbContext.TimeOfConcentrations.Select(x =>
                new TreatmentBMPModelingAttributeDropdownItemDto(x.TimeOfConcentrationID, x.TimeOfConcentrationDisplayName, "TimeOfConcentrationID"));
            treatmentBMPModelingAttributeDropdownItemDtos.AddRange(timeOfConcentrationDropdownItemDtos);

            var routingConfigurationDropdownItemDtos = dbContext.RoutingConfigurations.Select(x =>
                new TreatmentBMPModelingAttributeDropdownItemDto(x.RoutingConfigurationID, x.RoutingConfigurationDisplayName, "RoutingConfigurationID"));
            treatmentBMPModelingAttributeDropdownItemDtos.AddRange(routingConfigurationDropdownItemDtos);

            var monthsOfOperationDropdownItemDtos = dbContext.MonthsOfOperations.Select(x =>
                new TreatmentBMPModelingAttributeDropdownItemDto(x.MonthsOfOperationID, x.MonthsOfOperationDisplayName, "MonthsOfOperationID"));
            treatmentBMPModelingAttributeDropdownItemDtos.AddRange(monthsOfOperationDropdownItemDtos);

            var underlyingHydrologicSoilGroupsDropdownItemDtos = dbContext.UnderlyingHydrologicSoilGroups.Select(x =>
                new TreatmentBMPModelingAttributeDropdownItemDto(x.UnderlyingHydrologicSoilGroupID, x.UnderlyingHydrologicSoilGroupDisplayName, "UnderlyingHydrologicSoilGroupID"));
            treatmentBMPModelingAttributeDropdownItemDtos.AddRange(underlyingHydrologicSoilGroupsDropdownItemDtos);

            var dryWeatherFlowOverrideDropdownItemDtos = dbContext.DryWeatherFlowOverrides.Select(x =>
                new TreatmentBMPModelingAttributeDropdownItemDto(x.DryWeatherFlowOverrideID, x.DryWeatherFlowOverrideDisplayName, "DryWeatherFlowOverrideID"));
            treatmentBMPModelingAttributeDropdownItemDtos.AddRange(dryWeatherFlowOverrideDropdownItemDtos);

            return treatmentBMPModelingAttributeDropdownItemDtos;
        }

        public static void MergeTreatmentBMPs(HippocampDbContext dbContext, List<TreatmentBMPUpsertDto> treatmentBMPUpsertDtos)
        {
            var updatedTreatmentBMPs = treatmentBMPUpsertDtos
                .Select(x => new TreatmentBMP()
                {
                    TreatmentBMPID = x.TreatmentBMPID,
                    TreatmentBMPName = x.TreatmentBMPName,
                    TreatmentBMPTypeID = x.TreatmentBMPTypeID,
                    //ProjectID
                    //StormwaterJurisdictionID
                    //OwnerOrganizationID
                    //LocationPoint4326
                    Notes = x.Notes,
                    InventoryIsVerified = false,
                    TrashCaptureStatusTypeID = 4,
                    SizingBasisTypeID = 4
                });

            var existingTreatmentBMPs = dbContext.TreatmentBMPs.ToList();
            var allInDatabase = dbContext.TreatmentBMPs;

            existingTreatmentBMPs.MergeNew(updatedTreatmentBMPs, allInDatabase, 
                (x, y) => x.TreatmentBMPID == y.TreatmentBMPID);
            existingTreatmentBMPs.MergeUpdate(updatedTreatmentBMPs, 
                (x, y) => x.TreatmentBMPID == y.TreatmentBMPID, 
                (x, y) =>
                {
                    x.TreatmentBMPName = y.TreatmentBMPName;
                    x.TreatmentBMPTypeID = y.TreatmentBMPTypeID;
                    x.LocationPoint4326 = y.LocationPoint4326;
                    x.StormwaterJurisdictionID = y.StormwaterJurisdictionID;
                    x.OwnerOrganizationID = y.OwnerOrganizationID;
                    x.Notes = y.Notes;
                });
            dbContext.SaveChanges();

            existingTreatmentBMPs = dbContext.TreatmentBMPs.ToList();
        }

        public static void MergeNewTreatmentBMPs(HippocampDbContext dbContext, List<TreatmentBMPUpsertDto> treatmentBMPUpsertDtos)
        {
            // merge new Treatment BMPs
            var newTreatmentBMPs = treatmentBMPUpsertDtos
                .Select(TreatmentBMPFromUpsertDto);

            var existingTreatmentBMPs = dbContext.TreatmentBMPs.ToList();
            var allTreatmentBMPsInDatabase = dbContext.TreatmentBMPs;

            existingTreatmentBMPs.MergeNew(newTreatmentBMPs, allTreatmentBMPsInDatabase,
                (x, y) => x.TreatmentBMPID == y.TreatmentBMPID);

            // update upsert dtos with new TreatmentBMPIDs
            foreach (var treatmentBMPUpsertDto in treatmentBMPUpsertDtos)
            {
                treatmentBMPUpsertDto.TreatmentBMPID = existingTreatmentBMPs
                    .Single(x => x.TreatmentBMPName == treatmentBMPUpsertDto.TreatmentBMPName).TreatmentBMPID;
            }

            // merge new TreatmentBMPModelingAttributeIDs
            var newTreatmentBMPModelingAttributes = treatmentBMPUpsertDtos.Select(TreatmentBMPModelingAttributeFromUpsertDto);
            var existingTreatmentBMPModelingAttributes = dbContext.TreatmentBMPModelingAttributes.ToList();
            var allTreatmentBMPModelingAttributesInDatabase = dbContext.TreatmentBMPModelingAttributes;

            existingTreatmentBMPModelingAttributes.MergeNew(newTreatmentBMPModelingAttributes, allTreatmentBMPModelingAttributesInDatabase,
                (x, y) => x.TreatmentBMPModelingAttributeID == y.TreatmentBMPModelingAttributeID);
        }

        public static void MergeUpdatedAndDeletedTreatmentBMPs(HippocampDbContext dbContext, List<TreatmentBMPUpsertDto> treatmentBMPUpsertDtos)
        {
            // update TreatmentBMP and TreatmentBMPModelingAttribute records
            var updatedTreatmentBMPs = treatmentBMPUpsertDtos
                .Select(TreatmentBMPFromUpsertDto);

            var existingTreatmentBMPs = dbContext.TreatmentBMPs.ToList();
            existingTreatmentBMPs.MergeUpdate(updatedTreatmentBMPs, 
                (x, y) => x.TreatmentBMPID == y.TreatmentBMPID,
                (x, y) =>
                {
                    x.TreatmentBMPID = y.TreatmentBMPID;
                    x.TreatmentBMPName = y.TreatmentBMPName;
                    x.TreatmentBMPTypeID = y.TreatmentBMPTypeID;
                    x.LocationPoint4326 = y.LocationPoint;
                    x.Notes = y.Notes;
                });

            var updatedTreatmentBMPModelingAttributes = treatmentBMPUpsertDtos
                .Select(TreatmentBMPModelingAttributeFromUpsertDto);

            var existingTreatmentBMPModelingAttributes = dbContext.TreatmentBMPModelingAttributes.ToList();
            existingTreatmentBMPModelingAttributes.MergeUpdate(updatedTreatmentBMPModelingAttributes,
                (x, y) => x.TreatmentBMPModelingAttributeID == y.TreatmentBMPModelingAttributeID,
                (x, y) =>
                {
                    x.TreatmentBMPID = y.TreatmentBMPID;
                    x.AverageDivertedFlowrate = y.AverageDivertedFlowrate;
                    x.AverageTreatmentFlowrate = y.AverageTreatmentFlowrate;
                    x.DesignDryWeatherTreatmentCapacity = y.DesignDryWeatherTreatmentCapacity;
                    x.DesignLowFlowDiversionCapacity = y.DesignLowFlowDiversionCapacity;
                    x.DesignMediaFiltrationRate = y.DesignMediaFiltrationRate;
                    x.DesignResidenceTimeforPermanentPool = y.DesignResidenceTimeforPermanentPool;
                    x.DiversionRate = y.DiversionRate;
                    x.DrawdownTimeforWQDetentionVolume = y.DrawdownTimeforWQDetentionVolume;
                    x.EffectiveFootprint = y.EffectiveFootprint;
                    x.EffectiveRetentionDepth = y.EffectiveRetentionDepth;
                    x.InfiltrationDischargeRate = y.InfiltrationDischargeRate;
                    x.InfiltrationSurfaceArea = y.InfiltrationSurfaceArea;
                    x.MediaBedFootprint = y.MediaBedFootprint;
                    x.PermanentPoolorWetlandVolume = y.PermanentPoolorWetlandVolume;
                    x.RoutingConfigurationID = y.RoutingConfigurationID;
                    x.StorageVolumeBelowLowestOutletElevation = y.StorageVolumeBelowLowestOutletElevation;
                    x.SummerHarvestedWaterDemand = y.SummerHarvestedWaterDemand;
                    x.TimeOfConcentrationID = y.TimeOfConcentrationID;
                    x.DrawdownTimeForDetentionVolume = y.DrawdownTimeForDetentionVolume;
                    x.TotalEffectiveBMPVolume = y.TotalEffectiveBMPVolume;
                    x.TotalEffectiveDrywellBMPVolume = y.TotalEffectiveDrywellBMPVolume;
                    x.TreatmentRate = y.TreatmentRate;
                    x.UnderlyingHydrologicSoilGroupID = y.UnderlyingHydrologicSoilGroupID;
                    x.UnderlyingInfiltrationRate = y.UnderlyingInfiltrationRate;
                    x.WaterQualityDetentionVolume = y.WaterQualityDetentionVolume;
                    x.WettedFootprint = y.WettedFootprint;
                    x.WinterHarvestedWaterDemand = y.WinterHarvestedWaterDemand;
                    x.MonthsOfOperationID = y.MonthsOfOperationID;
                    x.DryWeatherFlowOverrideID = y.DryWeatherFlowOverrideID;
                });

            // delete TreatmentBMPModelingAttribute records, and then TreatmentBMP records
            var allTreatmentBMPModelingAttributesInDatabase = dbContext.TreatmentBMPModelingAttributes;
            existingTreatmentBMPModelingAttributes.MergeDelete(updatedTreatmentBMPModelingAttributes, 
                (x, y) => x.TreatmentBMPID == y.TreatmentBMPID, 
                allTreatmentBMPModelingAttributesInDatabase);

            var allTreatmentBMPsInDatabase = dbContext.TreatmentBMPs;
            existingTreatmentBMPs.MergeDelete(updatedTreatmentBMPs,
                (x, y) => x.TreatmentBMPID == y.TreatmentBMPID,
                allTreatmentBMPsInDatabase);
        }

        public static TreatmentBMP TreatmentBMPFromUpsertDto(TreatmentBMPUpsertDto treatmentBMPUpsertDto)
        {
            return new()
            {
                TreatmentBMPID = treatmentBMPUpsertDto.TreatmentBMPID,
                TreatmentBMPName = treatmentBMPUpsertDto.TreatmentBMPName,
                TreatmentBMPTypeID = treatmentBMPUpsertDto.TreatmentBMPTypeID,
                //ProjectID
                //StormwaterJurisdictionID
                //OwnerOrganizationID
                //LocationPoint4326
                Notes = treatmentBMPUpsertDto.Notes,
                InventoryIsVerified = false,
                TrashCaptureStatusTypeID = 4,
                SizingBasisTypeID = 4
            };
        }

        public static TreatmentBMPModelingAttribute TreatmentBMPModelingAttributeFromUpsertDto(TreatmentBMPUpsertDto treatmentBMPUpsertDto)
        {
            return new()
            {
                TreatmentBMPID = treatmentBMPUpsertDto.TreatmentBMPID,
                AverageDivertedFlowrate = treatmentBMPUpsertDto.AverageDivertedFlowrate,
                AverageTreatmentFlowrate = treatmentBMPUpsertDto.AverageTreatmentFlowrate,
                DesignDryWeatherTreatmentCapacity = treatmentBMPUpsertDto.DesignDryWeatherTreatmentCapacity,
                DesignLowFlowDiversionCapacity = treatmentBMPUpsertDto.DesignLowFlowDiversionCapacity,
                DesignMediaFiltrationRate = treatmentBMPUpsertDto.DesignMediaFiltrationRate,
                DesignResidenceTimeforPermanentPool = treatmentBMPUpsertDto.DesignResidenceTimeforPermanentPool,
                DiversionRate = treatmentBMPUpsertDto.DiversionRate,
                DrawdownTimeforWQDetentionVolume = treatmentBMPUpsertDto.DrawdownTimeforWQDetentionVolume,
                EffectiveFootprint = treatmentBMPUpsertDto.EffectiveFootprint,
                EffectiveRetentionDepth = treatmentBMPUpsertDto.EffectiveRetentionDepth,
                InfiltrationDischargeRate = treatmentBMPUpsertDto.InfiltrationDischargeRate,
                InfiltrationSurfaceArea = treatmentBMPUpsertDto.InfiltrationSurfaceArea,
                MediaBedFootprint = treatmentBMPUpsertDto.MediaBedFootprint,
                PermanentPoolorWetlandVolume = treatmentBMPUpsertDto.PermanentPoolorWetlandVolume,
                RoutingConfigurationID = treatmentBMPUpsertDto.RoutingConfigurationID,
                StorageVolumeBelowLowestOutletElevation = treatmentBMPUpsertDto.StorageVolumeBelowLowestOutletElevation,
                SummerHarvestedWaterDemand = treatmentBMPUpsertDto.SummerHarvestedWaterDemand,
                TimeOfConcentrationID = treatmentBMPUpsertDto.TimeOfConcentrationID,
                DrawdownTimeForDetentionVolume = treatmentBMPUpsertDto.DrawdownTimeForDetentionVolume,
                TotalEffectiveBMPVolume = treatmentBMPUpsertDto.TotalEffectiveBMPVolume,
                TotalEffectiveDrywellBMPVolume = treatmentBMPUpsertDto.TotalEffectiveDrywellBMPVolume,
                TreatmentRate = treatmentBMPUpsertDto.TreatmentRate,
                UnderlyingHydrologicSoilGroupID = treatmentBMPUpsertDto.UnderlyingHydrologicSoilGroupID,
                UnderlyingInfiltrationRate = treatmentBMPUpsertDto.UnderlyingInfiltrationRate,
                WaterQualityDetentionVolume = treatmentBMPUpsertDto.WaterQualityDetentionVolume,
                WettedFootprint = treatmentBMPUpsertDto.WettedFootprint,
                WinterHarvestedWaterDemand = treatmentBMPUpsertDto.WinterHarvestedWaterDemand,
                MonthsOfOperationID = treatmentBMPUpsertDto.MonthsOfOperationID,
                DryWeatherFlowOverrideID = treatmentBMPUpsertDto.DryWeatherFlowOverrideID
            };
        }

    }
}
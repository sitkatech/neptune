using System;
using System.Collections.Generic;
using System.Linq;
using Hippocamp.API.Util;
using Hippocamp.Models.DataTransferObjects;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using static Hippocamp.EFModels.Entities.DelineationType;

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
                .Include(x => x.OwnerOrganization)
                .AsNoTracking();
        }

        public static TreatmentBMP GetByTreatmentBMPID (HippocampDbContext dbContext, int treatmentBMPID)
        {
            return dbContext.TreatmentBMPs.SingleOrDefault(x => x.TreatmentBMPID == treatmentBMPID);
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
            var treatmentBMPTypeSimpleDtos = dbContext.TreatmentBMPTypes
                .OrderBy(x => x.TreatmentBMPTypeName)
                .Select(x => x.AsSimpleDto())
                .ToList();
            return treatmentBMPTypeSimpleDtos;
        }

        public static List<TreatmentBMPModelingAttributeDropdownItemDto> GetModelingAttributeDropdownItemsAsDto(HippocampDbContext dbContext)
        {
            var treatmentBMPModelingAttributeDropdownItemDtos = new List<TreatmentBMPModelingAttributeDropdownItemDto>();

            var timeOfConcentrationDropdownItemDtos = dbContext.TimeOfConcentrations.Select(x =>
                new TreatmentBMPModelingAttributeDropdownItemDto(x.TimeOfConcentrationID, x.TimeOfConcentrationDisplayName, "TimeOfConcentrationID"));
            treatmentBMPModelingAttributeDropdownItemDtos.AddRange(timeOfConcentrationDropdownItemDtos);

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

        public static void MergeProjectTreatmentBMPs(HippocampDbContext dbContext, List<TreatmentBMPUpsertDto> treatmentBMPUpsertDtos, List<TreatmentBMP> existingTreatmentBMPs, Project project)
        {
            var existingProjectTreatmentBMPs = existingTreatmentBMPs.Where(x => x.ProjectID == project.ProjectID).ToList();
            var existingProjectTreatmentBMPModelingAttributes = dbContext.TreatmentBMPModelingAttributes.Where(x => existingProjectTreatmentBMPs.Select(y => y.TreatmentBMPID).Contains(x.TreatmentBMPID)).ToList();
            
            var allTreatmentBMPsInDatabase = dbContext.TreatmentBMPs;
            var allTreatmentBMPModelingAttributesInDatabase = dbContext.TreatmentBMPModelingAttributes;

            // merge new Treatment BMPs
            var updatedTreatmentBMPs = treatmentBMPUpsertDtos
                .Select(x => TreatmentBMPFromUpsertDtoAndProject(dbContext, x, project));

            var treatmentBMPsWhoseLocationChanged = existingProjectTreatmentBMPs.Where(x => updatedTreatmentBMPs.Any(y => x.TreatmentBMPID == y.TreatmentBMPID && (!x.LocationPoint4326.Equals(y.LocationPoint4326)))).Select(x => x.TreatmentBMPID).ToList();

            existingProjectTreatmentBMPs.MergeNew(updatedTreatmentBMPs, allTreatmentBMPsInDatabase,
                (x, y) => x.TreatmentBMPName == y.TreatmentBMPName);

            dbContext.SaveChanges();

            // update upsert dtos with new TreatmentBMPIDs
            foreach (var treatmentBMPUpsertDto in treatmentBMPUpsertDtos)
            {
                treatmentBMPUpsertDto.TreatmentBMPID = existingProjectTreatmentBMPs
                    .Single(x => x.TreatmentBMPName == treatmentBMPUpsertDto.TreatmentBMPName).TreatmentBMPID;
            }

            // merge new TreatmentBMPModelingAttributeIDs
            var updatedTreatmentBMPModelingAttributes = treatmentBMPUpsertDtos.Select(TreatmentBMPModelingAttributeFromUpsertDto);
            existingProjectTreatmentBMPModelingAttributes.MergeNew(updatedTreatmentBMPModelingAttributes, allTreatmentBMPModelingAttributesInDatabase,
                (x, y) => x.TreatmentBMPID == y.TreatmentBMPID);

            // update TreatmentBMP and TreatmentBMPModelingAttribute records
            existingProjectTreatmentBMPs.MergeUpdate(updatedTreatmentBMPs, 
                (x, y) => x.TreatmentBMPID == y.TreatmentBMPID,
                (x, y) =>
                {
                    x.TreatmentBMPName = y.TreatmentBMPName;
                    x.LocationPoint4326 = y.LocationPoint4326;
                    x.LocationPoint = y.LocationPoint;
                    x.WatershedID = y.WatershedID;
                    x.Notes = y.Notes;
                });

            existingProjectTreatmentBMPModelingAttributes.MergeUpdate(updatedTreatmentBMPModelingAttributes,
                (x, y) => x.TreatmentBMPID == y.TreatmentBMPID,
                (x, y) =>
                {
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

            // delete TreatmentBMPModelingAttribute records
            existingProjectTreatmentBMPModelingAttributes.MergeDelete(updatedTreatmentBMPModelingAttributes, 
                (x, y) => x.TreatmentBMPID == y.TreatmentBMPID, 
                allTreatmentBMPModelingAttributesInDatabase);

            // delete TreatmentBMP records
            existingProjectTreatmentBMPs.MergeDelete(updatedTreatmentBMPs,
                (x, y) => x.TreatmentBMPID == y.TreatmentBMPID,
                allTreatmentBMPsInDatabase);

            if (treatmentBMPsWhoseLocationChanged.Any())
            {
                var centralizedDelineationsToDelete = dbContext.Delineations.Where(x => x.DelineationTypeID == (int)DelineationTypeEnum.Centralized && treatmentBMPsWhoseLocationChanged.Contains(x.TreatmentBMPID)).ToList();
                dbContext.Delineations.RemoveRange(centralizedDelineationsToDelete);
            }

            dbContext.SaveChanges();
        }

        private static Geometry CreateLocationPoint4326FromLatLong(double latitude, double longitude)
        {
            return new Point(longitude, latitude) { SRID = 4326 };
        }

        public static TreatmentBMP TreatmentBMPFromUpsertDtoAndProject(HippocampDbContext dbContext, TreatmentBMPUpsertDto treatmentBMPUpsertDto, Project project)
        {
            
            var locationPointGeometry4326 = CreateLocationPoint4326FromLatLong(treatmentBMPUpsertDto.Latitude.Value, treatmentBMPUpsertDto.Longitude.Value);
            var locationPoint = locationPointGeometry4326.ProjectTo2771();

            var watershed = dbContext.Watersheds.FirstOrDefault(x => x.WatershedGeometry.Contains(locationPoint));


            var treatmentBMP = new TreatmentBMP()
            {
                TreatmentBMPName = treatmentBMPUpsertDto.TreatmentBMPName,
                TreatmentBMPTypeID = treatmentBMPUpsertDto.TreatmentBMPTypeID.Value,
                ProjectID = project.ProjectID,
                StormwaterJurisdictionID = project.StormwaterJurisdictionID,
                OwnerOrganizationID = project.OrganizationID,
                LocationPoint4326 = locationPointGeometry4326,
                LocationPoint = locationPoint,
                WatershedID = watershed?.WatershedID,
                Notes = treatmentBMPUpsertDto.Notes,
                InventoryIsVerified = false,
                TrashCaptureStatusTypeID = (int)TrashCaptureStatusTypeEnum.NotProvided,
                SizingBasisTypeID = (int)SizingBasisTypeEnum.NotProvided
            };

            if (treatmentBMPUpsertDto.TreatmentBMPID > 0)
            {
                treatmentBMP.TreatmentBMPID = treatmentBMPUpsertDto.TreatmentBMPID;
            }

            return treatmentBMP;
        }

        public static TreatmentBMPModelingAttribute TreatmentBMPModelingAttributeFromUpsertDto(TreatmentBMPUpsertDto treatmentBMPUpsertDto)
        {
            var treatmentBMPModelingAttribute = new TreatmentBMPModelingAttribute()
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
                StorageVolumeBelowLowestOutletElevation = treatmentBMPUpsertDto.StorageVolumeBelowLowestOutletElevation,
                SummerHarvestedWaterDemand = treatmentBMPUpsertDto.SummerHarvestedWaterDemand,
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

            var modelingTypeIDsWithoutAdditionalFields = new List<int>()
            {
                (int)TreatmentBMPModelingTypeEnum.HydrodynamicSeparator, (int)TreatmentBMPModelingTypeEnum.ProprietaryBiotreatment, (int)TreatmentBMPModelingTypeEnum.ProprietaryTreatmentControl, 
                (int)TreatmentBMPModelingTypeEnum.LowFlowDiversions, (int)TreatmentBMPModelingTypeEnum.DryWeatherTreatmentSystems
            };

            if (modelingTypeIDsWithoutAdditionalFields.Contains(treatmentBMPUpsertDto.TreatmentBMPModelingTypeID.Value))
            {
                return treatmentBMPModelingAttribute;
            }

            treatmentBMPModelingAttribute.RoutingConfigurationID = (int)RoutingConfigurationEnum.Online;
            treatmentBMPModelingAttribute.TimeOfConcentrationID = treatmentBMPUpsertDto.TimeOfConcentrationID;
            treatmentBMPModelingAttribute.UnderlyingHydrologicSoilGroupID = treatmentBMPUpsertDto.UnderlyingHydrologicSoilGroupID;

            return treatmentBMPModelingAttribute;
        }
    }
}
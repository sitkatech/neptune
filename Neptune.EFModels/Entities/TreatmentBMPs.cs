﻿/*-----------------------------------------------------------------------
<copyright file="TreatmentBMP.DatabaseContextExtensions.cs" company="Tahoe Regional Planning Agency">
Copyright (c) Tahoe Regional Planning Agency. All rights reserved.
<author>Sitka Technology Group</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/

using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;
using Neptune.Common.GeoSpatial;
using Neptune.Models.DataTransferObjects;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities
{
    public static class TreatmentBMPs
    {
        private static IQueryable<TreatmentBMP> GetTreatmentBMPsDisplayOnlyImpl(NeptuneDbContext dbContext, bool checkIsAnalyzedInModelingModule = true)
        {
            return dbContext.TreatmentBMPs
                .Include(x => x.TreatmentBMPType).Where(x => !checkIsAnalyzedInModelingModule || x.TreatmentBMPType.IsAnalyzedInModelingModule)
                .Include(x => x.Delineation)
                .Include(x => x.TreatmentBMPModelingAttributeTreatmentBMP)
                .Include(x => x.Project)
                .AsNoTracking();
        }

        public static List<TreatmentBMP> GetProvisionalTreatmentBMPs(NeptuneDbContext dbContext, Person currentPerson)
        {
            return GetNonPlanningModuleBMPs(dbContext).Where(x => x.InventoryIsVerified == false).ToList()
                .Where(x => x.CanView(currentPerson)).OrderBy(x => x.TreatmentBMPName).ToList();
        }

        public static IQueryable<TreatmentBMP> GetNonPlanningModuleBMPs(NeptuneDbContext dbContext)
        {
            return GetImpl(dbContext).AsNoTracking().Where(x => x.ProjectID == null);
        }

        private static IQueryable<TreatmentBMP> GetImpl(NeptuneDbContext dbContext)
        {
            return dbContext.TreatmentBMPs
                    .Include(x => x.TreatmentBMPType)
                    .Include(x => x.StormwaterJurisdiction)
                    .ThenInclude(x => x.Organization)
                    .Include(x => x.OwnerOrganization)
                    .Include(x => x.TreatmentBMPModelingAttributeTreatmentBMP)
                    .Include(x => x.UpstreamBMP)
                    .Include(x => x.InventoryVerifiedByPerson)
                    .Include(x => x.WaterQualityManagementPlan)
                ;
        }

        public static TreatmentBMP GetByIDWithChangeTracking(NeptuneDbContext dbContext, int treatmentBMPID)
        {
            var treatmentBMP = GetImpl(dbContext)
                .SingleOrDefault(x => x.TreatmentBMPID == treatmentBMPID);
            Check.RequireNotNull(treatmentBMP, $"TreatmentBMP with ID {treatmentBMPID} not found!");
            return treatmentBMP;
        }

        public static List<TreatmentBMPDisplayDto> ListByProjectIDsAsDisplayDto(NeptuneDbContext dbContext,
            List<int> projectIDs)
        {
            var treatmentBMPDisplayDtos = GetTreatmentBMPsDisplayOnlyImpl(dbContext)
                .Where(x => x.ProjectID.HasValue && projectIDs.Contains(x.ProjectID.Value))
                .Select(x => x.AsDisplayDto())
                .ToList();

            return treatmentBMPDisplayDtos;
        }

        private static FeatureCollection AsFeatureCollection(List<TreatmentBMP> treatmentBMPs)
        {
            var featureCollection = new FeatureCollection();
            foreach (var treatmentBMP in treatmentBMPs)
            {
                var attributesTable = new AttributesTable
                {
                    { "TreatmentBMPID", treatmentBMP.TreatmentBMPID },
                    { "TreatmentBMPName", treatmentBMP.TreatmentBMPName },
                    //{ "TreatmentBMPTypeID", treatmentBMP.TreatmentBMPTypeID },
                    { "TreatmentBMPTypeName", treatmentBMP.TreatmentBMPType.TreatmentBMPTypeName },
                    //{ "StormwaterJurisdictionID", treatmentBMP.StormwaterJurisdictionID },
                    //{ "Latitude", treatmentBMP.LocationPoint4326?.Coordinate.Y},
                    //{ "Longitude", treatmentBMP.LocationPoint4326?.Coordinate.Z},
                    { "FeatureColor", $"#{treatmentBMP.TrashCaptureStatusType.TrashCaptureStatusTypeColorCode}" },
                    { "TrashCaptureStatusTypeID", treatmentBMP.TrashCaptureStatusTypeID },
                    { "StormwaterJurisdictionID", treatmentBMP.StormwaterJurisdictionID }
                };
                var feature = new Feature(treatmentBMP.LocationPoint4326, attributesTable);
                featureCollection.Add(feature);
            }

            return featureCollection;
        }

        public static FeatureCollection ListInventoryIsVerifiedByPersonAsFeatureCollection(NeptuneDbContext dbContext, PersonDto person)
        {
            var treatmentBmps = ListByPerson(dbContext, person);
            return AsFeatureCollection(treatmentBmps.Where(x => x.ProjectID == null && x.InventoryIsVerified).ToList());
        }

        public static FeatureCollection ListInventoryIsVerifiedByPersonAndJurisdictionIDAsFeatureCollection(
            NeptuneDbContext dbContext,
            PersonDto person, int jurisdictionID)
        {
            var treatmentBmps = ListByPerson(dbContext, person, false);
            return AsFeatureCollection(treatmentBmps.Where(x => x.ProjectID == null && x.StormwaterJurisdictionID == jurisdictionID && x.InventoryIsVerified).ToList());
        }

        public static List<TreatmentBMPDisplayDto> ListWithProjectByPerson(NeptuneDbContext dbContext, PersonDto person)
        {
            var treatmentBmps = ListByPerson(dbContext, person);
            return treatmentBmps.Where(x => x.ProjectID != null).Select(x => x.AsDisplayDto()).ToList();
        }

        public static List<TreatmentBMPDisplayDto> ListWithOCTAM2Tier2GrantProgramByPerson(NeptuneDbContext dbContext,
            PersonDto person)
        {
            var treatmentBmps = ListByPerson(dbContext, person);
            return treatmentBmps.Where(x => x.Project is { ShareOCTAM2Tier2Scores: true }).Select(x => x.AsDisplayDto())
                .ToList();
        }

        private static List<TreatmentBMP> ListByPerson(NeptuneDbContext dbContext, PersonDto? person, bool checkIsAnalyzedInModelingModule = true)
        {
            List<TreatmentBMP> treatmentBmps;
            if (person == null || !(person.RoleID == (int)RoleEnum.Admin || person.RoleID == (int)RoleEnum.SitkaAdmin))
            {
                var jurisdictionIDs = StormwaterJurisdictionPeople.ListViewableStormwaterJurisdictionIDsByPersonIDForBMPs(dbContext, person?.PersonID);
                treatmentBmps = GetTreatmentBMPsDisplayOnlyImpl(dbContext, checkIsAnalyzedInModelingModule)
                    .Where(x => jurisdictionIDs.Contains(x.StormwaterJurisdictionID)).ToList();
            }
            else
            {
                treatmentBmps = GetTreatmentBMPsDisplayOnlyImpl(dbContext, checkIsAnalyzedInModelingModule).ToList();
            }

            return treatmentBmps;
        }

        public static List<TreatmentBMPUpsertDto> ListByProjectIDAsUpsertDto(NeptuneDbContext dbContext, int projectID)
        {
            var treatmentBMPs = dbContext.TreatmentBMPs
                .Include(x => x.StormwaterJurisdiction).ThenInclude(x => x.Organization)
                .Include(x => x.TreatmentBMPType)
                .Include(x => x.Watershed)
                .Include(x => x.OwnerOrganization)
                .Include(x => x.Delineation)
                .AsNoTracking()
                .Where(x => x.ProjectID == projectID).ToList();

            var treatmentBMPIDs = treatmentBMPs.Select(x => x.TreatmentBMPID).ToList();

            var treatmentBMPModelingAttributes = dbContext.TreatmentBMPModelingAttributes
                .Where(x => treatmentBMPIDs.Contains(x.TreatmentBMPID)).ToList();

            var treatmentBMPUpsertDtos = treatmentBMPs
                .GroupJoin(treatmentBMPModelingAttributes,
                    x => x.TreatmentBMPID,
                    y => y.TreatmentBMPID,
                    (x, y) => new { TreatmentBMP = x, TreatmentBmpModelingAttribute = y.SingleOrDefault() })
                .Select(x => x.TreatmentBMP.AsUpsertDtoWithModelingAttributes(x.TreatmentBmpModelingAttribute))
                .ToList();

            return treatmentBMPUpsertDtos;
        }

        public static List<TreatmentBMPDisplayDto> ListAsDisplayDto(NeptuneDbContext dbContext)
        {
            var treatmentBMPDisplayDtos = GetTreatmentBMPsDisplayOnlyImpl(dbContext)
                .Select(x => x.AsDisplayDto())
                .ToList();

            return treatmentBMPDisplayDtos;
        }

        public static List<TreatmentBMPDisplayDto> ListByPersonAsDisplayDto(NeptuneDbContext dbContext,
            PersonDto person)
        {
            var personID = person.PersonID;
            if (person.RoleID == (int)RoleEnum.Admin || person.RoleID == (int)RoleEnum.SitkaAdmin)
            {
                return ListAsDisplayDto(dbContext);
            }

            var jurisdictionIDs = People.ListStormwaterJurisdictionIDsByPersonID(dbContext, personID);

            var treatmentBMPDisplayDtos = GetTreatmentBMPsDisplayOnlyImpl(dbContext)
                .Where(x => jurisdictionIDs.Contains(x.StormwaterJurisdictionID))
                .Select(x => x.AsDisplayDto())
                .ToList();

            return treatmentBMPDisplayDtos;
        }

        public static List<TreatmentBMPTypeWithModelingAttributesDto> ListWithModelingAttributesAsDto(
            NeptuneDbContext dbContext)
        {
            var treatmentBMPTypeWithModelingAttributesDtos = dbContext.TreatmentBMPTypes.AsNoTracking()
                .OrderBy(x => x.TreatmentBMPTypeName)
                .Select(x =>
                    new TreatmentBMPTypeWithModelingAttributesDto()
                    {
                        TreatmentBMPTypeID = x.TreatmentBMPTypeID,
                        TreatmentBMPTypeName = x.TreatmentBMPTypeName,
                        TreatmentBMPModelingTypeID = x.TreatmentBMPModelingTypeID,
                        TreatmentBMPModelingAttributes = x.GetModelingAttributes()
                    }
                )
                .ToList();
            return treatmentBMPTypeWithModelingAttributesDtos;
        }

        public static TreatmentBMP GetByIDWithChangeTracking(NeptuneDbContext dbContext,
            TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            return GetByIDWithChangeTracking(dbContext, treatmentBMPPrimaryKey.PrimaryKeyValue);
        }

        public static TreatmentBMP GetByID(NeptuneDbContext dbContext, int treatmentBMPID)
        {
            var treatmentBMP = GetImpl(dbContext).AsNoTracking()
                .SingleOrDefault(x => x.TreatmentBMPID == treatmentBMPID);
            Check.RequireNotNull(treatmentBMP, $"TreatmentBMP with ID {treatmentBMPID} not found!");
            return treatmentBMP;
        }

        public static TreatmentBMP GetByID(NeptuneDbContext dbContext, TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            return GetByID(dbContext, treatmentBMPPrimaryKey.PrimaryKeyValue);
        }

        public static List<TreatmentBMP> List(NeptuneDbContext dbContext)
        {
            return GetImpl(dbContext).AsNoTracking().OrderBy(x => x.TreatmentBMPName).ToList();
        }

        public static List<TreatmentBMP> ListWithModelingAttributes(NeptuneDbContext dbContext)
        {
            return dbContext.TreatmentBMPs
                .Include(x => x.TreatmentBMPType)
                .Include(x => x.StormwaterJurisdiction)
                .ThenInclude(x => x.Organization)
                .Include(x => x.OwnerOrganization)
                .Include(x => x.TreatmentBMPModelingAttributeTreatmentBMP)
                .AsNoTracking().Where(x => x.TreatmentBMPType.IsAnalyzedInModelingModule)
                .OrderBy(x => x.TreatmentBMPName).ToList();
        }

        public static Dictionary<int, int> ListCountByTreatmentBMPType(NeptuneDbContext dbContext)
        {
            return dbContext.TreatmentBMPs.AsNoTracking().GroupBy(x => x.TreatmentBMPTypeID)
                .Select(x => new { x.Key, Count = x.Count() })
                .ToDictionary(x => x.Key, x => x.Count);
        }

        public static Dictionary<int, int> ListCountByStormwaterJurisdiction(NeptuneDbContext dbContext)
        {
            return dbContext.TreatmentBMPs.AsNoTracking().GroupBy(x => x.StormwaterJurisdictionID)
                .Select(x => new { x.Key, Count = x.Count() })
                .ToDictionary(x => x.Key, x => x.Count);
        }

        public static TreatmentBMP GetByIDForFeatureContextCheck(NeptuneDbContext dbContext, int treatmentBMPID)
        {
            var treatmentBMP = dbContext.TreatmentBMPs
                .Include(x => x.StormwaterJurisdiction)
                .ThenInclude(x => x.Organization).AsNoTracking()
                .SingleOrDefault(x => x.TreatmentBMPID == treatmentBMPID);
            Check.RequireNotNull(treatmentBMP, $"TreatmentBMP with ID {treatmentBMPID} not found!");
            return treatmentBMP;
        }

        public static List<TreatmentBMP> ListByStormwaterJurisdictionID(NeptuneDbContext dbContext,
            int stormwaterJurisdictionID)
        {
            return ListByStormwaterJurisdictionIDList(dbContext, new List<int> { stormwaterJurisdictionID });
        }

        public static List<TreatmentBMP> ListByStormwaterJurisdictionIDList(NeptuneDbContext dbContext,
            List<int> stormwaterJurisdictionIDList)
        {
            return GetImpl(dbContext).AsNoTracking()
                .Where(x => stormwaterJurisdictionIDList.Contains(x.StormwaterJurisdictionID)).ToList();
        }

        public static List<TreatmentBMP> ListByWaterQualityManagementPlanID(NeptuneDbContext dbContext,
            int waterQualityManagementPlanID)
        {
            return GetImpl(dbContext).AsNoTracking()
                .Where(x => x.WaterQualityManagementPlanID == waterQualityManagementPlanID).ToList();
        }

        public static List<TreatmentBMP> ListByWaterQualityManagementPlanIDWithChangeTracking(
            NeptuneDbContext dbContext, int waterQualityManagementPlanID)
        {
            return GetImpl(dbContext).Where(x => x.WaterQualityManagementPlanID == waterQualityManagementPlanID)
                .ToList();
        }

        public static List<TreatmentBMP> ListByTreatmentBMPIDList(NeptuneDbContext dbContext,
            List<int> treatmentBMPIDList)
        {
            return GetImpl(dbContext).AsNoTracking()
                .Where(x => treatmentBMPIDList.Contains(x.TreatmentBMPID)).ToList();
        }

        public static List<TreatmentBMP> ListByTreatmentBMPIDListWithChangeTracking(NeptuneDbContext dbContext,
            List<int> treatmentBMPIDList)
        {
            return GetImpl(dbContext).Where(x => treatmentBMPIDList.Contains(x.TreatmentBMPID)).ToList();
        }

        public static int? ChangeTreatmentBMPType(NeptuneDbContext dbContext, int treatmentBMPID, int treatmentBMPTypeID)
        {
            dbContext.Database.ExecuteSqlRaw(
                "EXECUTE dbo.pTreatmentBMPUpdateTreatmentBMPType @treatmentBMPID={0}, @treatmentBMPTypeID={1}",
                treatmentBMPID, treatmentBMPTypeID);
            var treatmentBMPModelingType = dbContext.TreatmentBMPTypes
                .SingleOrDefault(x => x.TreatmentBMPTypeID == treatmentBMPTypeID)?.TreatmentBMPModelingTypeID;
            return treatmentBMPModelingType;
        }

        public static TreatmentBMP? GetByTreatmentBMPID(NeptuneDbContext dbContext, int treatmentBMPID)
        {
            return dbContext.TreatmentBMPs.SingleOrDefault(x => x.TreatmentBMPID == treatmentBMPID);
        }

        public static List<TreatmentBMPModelingAttributeDropdownItemDto> GetModelingAttributeDropdownItemsAsDto(
            NeptuneDbContext dbContext)
        {
            var treatmentBMPModelingAttributeDropdownItemDtos =
                new List<TreatmentBMPModelingAttributeDropdownItemDto>();

            var timeOfConcentrationDropdownItemDtos = TimeOfConcentration.All.Select(x =>
                new TreatmentBMPModelingAttributeDropdownItemDto(x.TimeOfConcentrationID,
                    x.TimeOfConcentrationDisplayName, "TimeOfConcentrationID"));
            treatmentBMPModelingAttributeDropdownItemDtos.AddRange(timeOfConcentrationDropdownItemDtos);

            var monthsOfOperationDropdownItemDtos = MonthsOfOperation.All.Select(x =>
                new TreatmentBMPModelingAttributeDropdownItemDto(x.MonthsOfOperationID, x.MonthsOfOperationDisplayName,
                    "MonthsOfOperationID"));
            treatmentBMPModelingAttributeDropdownItemDtos.AddRange(monthsOfOperationDropdownItemDtos);

            var underlyingHydrologicSoilGroupsDropdownItemDtos = UnderlyingHydrologicSoilGroup.All.Select(x =>
                new TreatmentBMPModelingAttributeDropdownItemDto(x.UnderlyingHydrologicSoilGroupID,
                    x.UnderlyingHydrologicSoilGroupDisplayName, "UnderlyingHydrologicSoilGroupID"));
            treatmentBMPModelingAttributeDropdownItemDtos.AddRange(underlyingHydrologicSoilGroupsDropdownItemDtos);

            var dryWeatherFlowOverrideDropdownItemDtos = DryWeatherFlowOverride.All.Select(x =>
                new TreatmentBMPModelingAttributeDropdownItemDto(x.DryWeatherFlowOverrideID,
                    x.DryWeatherFlowOverrideDisplayName, "DryWeatherFlowOverrideID"));
            treatmentBMPModelingAttributeDropdownItemDtos.AddRange(dryWeatherFlowOverrideDropdownItemDtos);

            return treatmentBMPModelingAttributeDropdownItemDtos;
        }

        public static Geometry CreateLocationPoint4326FromLatLong(double latitude, double longitude)
        {
            return new Point(longitude, latitude) { SRID = 4326 };
        }

        public static TreatmentBMP TreatmentBMPFromUpsertDtoAndProject(NeptuneDbContext dbContext,
            TreatmentBMPUpsertDto treatmentBMPUpsertDto, Project project)
        {
            var locationPointGeometry4326 = CreateLocationPoint4326FromLatLong(treatmentBMPUpsertDto.Latitude.Value,
                treatmentBMPUpsertDto.Longitude.Value);
            var locationPoint = locationPointGeometry4326.ProjectTo2771();
            var treatmentBMP = new TreatmentBMP()
            {
                TreatmentBMPName = treatmentBMPUpsertDto.TreatmentBMPName,
                TreatmentBMPTypeID = treatmentBMPUpsertDto.TreatmentBMPTypeID.Value,
                ProjectID = project.ProjectID,
                StormwaterJurisdictionID = project.StormwaterJurisdictionID,
                OwnerOrganizationID = project.OrganizationID,
                LocationPoint4326 = locationPointGeometry4326,
                LocationPoint = locationPoint,
                Notes = treatmentBMPUpsertDto.Notes,
                InventoryIsVerified = false,
                TrashCaptureStatusTypeID = (int)TrashCaptureStatusTypeEnum.NotProvided,
                SizingBasisTypeID = (int)SizingBasisTypeEnum.NotProvided
            };

            treatmentBMP.SetTreatmentBMPPointInPolygonDataByLocationPoint(locationPoint, dbContext);

            if (treatmentBMPUpsertDto.TreatmentBMPID > 0)
            {
                treatmentBMP.TreatmentBMPID = treatmentBMPUpsertDto.TreatmentBMPID;
            }

            var treatmentBMPModelingAttribute = new TreatmentBMPModelingAttribute()
            {
                TreatmentBMPID = treatmentBMPUpsertDto.TreatmentBMPID,
                AverageDivertedFlowrate = treatmentBMPUpsertDto.AverageDivertedFlowrate,
                AverageTreatmentFlowrate = treatmentBMPUpsertDto.AverageTreatmentFlowrate,
                DesignDryWeatherTreatmentCapacity = treatmentBMPUpsertDto.DesignDryWeatherTreatmentCapacity,
                DesignLowFlowDiversionCapacity = treatmentBMPUpsertDto.DesignLowFlowDiversionCapacity,
                DesignMediaFiltrationRate = treatmentBMPUpsertDto.DesignMediaFiltrationRate,
                DiversionRate = treatmentBMPUpsertDto.DiversionRate,
                DrawdownTimeForWQDetentionVolume = treatmentBMPUpsertDto.DrawdownTimeForWQDetentionVolume,
                EffectiveFootprint = treatmentBMPUpsertDto.EffectiveFootprint,
                EffectiveRetentionDepth = treatmentBMPUpsertDto.EffectiveRetentionDepth,
                InfiltrationDischargeRate = treatmentBMPUpsertDto.InfiltrationDischargeRate,
                InfiltrationSurfaceArea = treatmentBMPUpsertDto.InfiltrationSurfaceArea,
                MediaBedFootprint = treatmentBMPUpsertDto.MediaBedFootprint,
                PermanentPoolOrWetlandVolume = treatmentBMPUpsertDto.PermanentPoolOrWetlandVolume,
                StorageVolumeBelowLowestOutletElevation = treatmentBMPUpsertDto.StorageVolumeBelowLowestOutletElevation,
                SummerHarvestedWaterDemand = treatmentBMPUpsertDto.SummerHarvestedWaterDemand,
                DrawdownTimeForDetentionVolume = treatmentBMPUpsertDto.DrawdownTimeForDetentionVolume,
                TimeOfConcentrationID = treatmentBMPUpsertDto.TimeOfConcentrationID,
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
                (int)TreatmentBMPModelingTypeEnum.HydrodynamicSeparator,
                (int)TreatmentBMPModelingTypeEnum.ProprietaryBiotreatment,
                (int)TreatmentBMPModelingTypeEnum.ProprietaryTreatmentControl,
                (int)TreatmentBMPModelingTypeEnum.LowFlowDiversions,
                (int)TreatmentBMPModelingTypeEnum.DryWeatherTreatmentSystems
            };

            if (treatmentBMPUpsertDto.TreatmentBMPModelingTypeID.HasValue &&
                !modelingTypeIDsWithoutAdditionalFields.Contains(treatmentBMPUpsertDto.TreatmentBMPModelingTypeID
                    .Value))
            {
                treatmentBMPModelingAttribute.RoutingConfigurationID = (int)RoutingConfigurationEnum.Online;
                treatmentBMPModelingAttribute.TimeOfConcentrationID = treatmentBMPUpsertDto.TimeOfConcentrationID;
                treatmentBMPModelingAttribute.UnderlyingHydrologicSoilGroupID =
                    treatmentBMPUpsertDto.UnderlyingHydrologicSoilGroupID;
            }

            treatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP = treatmentBMPModelingAttribute;
            return treatmentBMP;
        }

        public static List<TreatmentBMP> ListModelingTreatmentBMPs(NeptuneDbContext dbContext, int? projectID = null,
            List<int>? projectRSBIDs = null)
        {
            var toReturn = dbContext.TreatmentBMPs
                .Include(x => x.TreatmentBMPType)
                .Include(x => x.StormwaterJurisdiction)
                .ThenInclude(x => x.Organization)
                .Include(x => x.OwnerOrganization)
                .Include(x => x.TreatmentBMPModelingAttributeTreatmentBMP)
                .Include(x => x.UpstreamBMP)
                .Include(x => x.WaterQualityManagementPlan).AsNoTracking()
                .Where(x => x.RegionalSubbasinID != null && x.TreatmentBMPType.TreatmentBMPModelingTypeID != null &&
                            x.ModelBasinID != null).ToList();

            if (projectID != null && projectRSBIDs != null)
            {
                toReturn = toReturn.Where(x =>
                    projectRSBIDs.Contains(x.RegionalSubbasinID.Value) &&
                    (x.ProjectID == null || x.ProjectID == projectID)).ToList();
            }
            else
            {
                toReturn = toReturn.Where(x => x.ProjectID == null).ToList();
            }

            return toReturn;
        }
    }
}
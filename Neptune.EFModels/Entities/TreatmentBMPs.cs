/*-----------------------------------------------------------------------
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

namespace Neptune.EFModels.Entities;

public static class TreatmentBMPs
{

    #region Create

    public static async Task<List<ErrorMessage>> ValidateCreateAsync(NeptuneDbContext dbContext, TreatmentBMPCreateDto createDto)
    {
        var errors = new List<ErrorMessage>();

        // Validate basic shared fields
        errors.AddRange(await ValidateBasicInfoAsync(dbContext, createDto));

        // Validate Treatment BMP Type
        var hasValidType = await dbContext.TreatmentBMPTypes.AnyAsync(x => x.TreatmentBMPTypeID == createDto.TreatmentBMPTypeID);
        if (!hasValidType)
        {
            errors.Add(new ErrorMessage("TreatmentBMPTypeID", "Valid Treatment BMP Type is required."));
        }

        // Validate Stormwater Jurisdiction
        var hasValidJurisdiction = await dbContext.StormwaterJurisdictions.AnyAsync(x => x.StormwaterJurisdictionID == createDto.StormwaterJurisdictionID);
        if (!hasValidJurisdiction)
        {
            errors.Add(new ErrorMessage("StormwaterJurisdictionID", "Valid Stormwater Jurisdiction is required."));
        }

        return errors;
    }

    public static async Task<TreatmentBMPDto> CreateAsync(NeptuneDbContext dbContext, TreatmentBMPCreateDto createDto, PersonDto creatingPerson)
    {
        if (!createDto.OwnerOrganizationID.HasValue)
        {
            var stormwaterJurisdiction = await dbContext.StormwaterJurisdictions
                .AsNoTracking()
                .SingleAsync(x => x.StormwaterJurisdictionID == createDto.StormwaterJurisdictionID);

            createDto.OwnerOrganizationID = stormwaterJurisdiction.OrganizationID;
        }

        var treatmentBMP = new TreatmentBMP
        {
            TreatmentBMPName = createDto.TreatmentBMPName,
            TreatmentBMPTypeID = createDto.TreatmentBMPTypeID,
            StormwaterJurisdictionID = createDto.StormwaterJurisdictionID,
            OwnerOrganizationID = createDto.OwnerOrganizationID.Value,
            YearBuilt = createDto.YearBuilt,
            SystemOfRecordID = createDto.SystemOfRecordID,
            WaterQualityManagementPlanID = createDto.WaterQualityManagementPlanID,
            TreatmentBMPLifespanTypeID = createDto.TreatmentBMPLifespanTypeID,
            TreatmentBMPLifespanEndDate = createDto.TreatmentBMPLifespanTypeID == TreatmentBMPLifespanType.FixedEndDate.TreatmentBMPLifespanTypeID 
                ? createDto.TreatmentBMPLifespanEndDate 
                : null,
            SizingBasisTypeID = createDto.SizingBasisTypeID,
            TrashCaptureStatusTypeID = createDto.TrashCaptureStatusTypeID,
            TrashCaptureEffectiveness = createDto.TrashCaptureStatusTypeID == TrashCaptureStatusType.Partial.TrashCaptureStatusTypeID 
                ? createDto.TrashCaptureEffectiveness
                : null,
            RequiredFieldVisitsPerYear = createDto.RequiredFieldVisitsPerYear,
            RequiredPostStormFieldVisitsPerYear = createDto.RequiredPostStormFieldVisitsPerYear,
            Notes = createDto.Notes,
            InventoryIsVerified = false
        };

        if (createDto.Latitude.HasValue && createDto.Longitude.HasValue)
        {
            treatmentBMP.LocationPoint4326 = CreateLocationPoint4326FromLatLong(createDto.Latitude.Value, createDto.Longitude.Value);
            treatmentBMP.LocationPoint = treatmentBMP.LocationPoint4326.ProjectTo2771();
            treatmentBMP.SetTreatmentBMPPointInPolygonDataByLocationPoint(treatmentBMP.LocationPoint, dbContext);
        }

        await dbContext.TreatmentBMPs.AddAsync(treatmentBMP);
        await dbContext.SaveChangesAsync();
        await dbContext.Entry(treatmentBMP).ReloadAsync();

        var createdBMP = await GetByIDAsDtoAsync(dbContext, treatmentBMP.TreatmentBMPID);
        return createdBMP;
    }

    #endregion

    private static IQueryable<TreatmentBMP> GetTreatmentBMPsDisplayOnlyImpl(NeptuneDbContext dbContext, bool checkIsAnalyzedInModelingModule = true)
    {
        return dbContext.TreatmentBMPs
            .Include(x => x.TreatmentBMPType).Where(x => !checkIsAnalyzedInModelingModule || x.TreatmentBMPType.IsAnalyzedInModelingModule)
            .Include(x => x.CustomAttributes)
            .ThenInclude(x => x.CustomAttributeValues)
            .Include(x => x.Delineation)
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
            .Include(x => x.StormwaterJurisdiction).ThenInclude(x => x.Organization)
            .Include(x => x.OwnerOrganization)
            .Include(x => x.UpstreamBMP)
            .Include(x => x.InventoryVerifiedByPerson)
            .Include(x => x.WaterQualityManagementPlan)
            .Include(x => x.CustomAttributes).ThenInclude(x => x.CustomAttributeValues);
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
        var treatmentBMPs = GetTreatmentBMPsDisplayOnlyImpl(dbContext)
            .Where(x => x.ProjectID.HasValue && projectIDs.Contains(x.ProjectID.Value))
            .ToList();

        return GetDisplayDtos(dbContext, treatmentBMPs);
    }

    private static List<TreatmentBMPDisplayDto> GetDisplayDtos(NeptuneDbContext dbContext, List<TreatmentBMP> treatmentBMPs)
    {
        var treatmentBMPIDs = treatmentBMPs.Select(x => x.TreatmentBMPID).ToList();

        var treatmentBMPModelingAttributes = dbContext.vTreatmentBMPModelingAttributes.Where(x => treatmentBMPIDs.Contains(x.TreatmentBMPID)).ToList();

        var treatmentBMPDisplayDtos = treatmentBMPs
            .GroupJoin(treatmentBMPModelingAttributes,
                       x => x.TreatmentBMPID,
                       y => y.TreatmentBMPID,
                       (x, y) => new { TreatmentBMP = x, TreatmentBmpModelingAttribute = y.SingleOrDefault() })
            .Select(x => x.TreatmentBMP.AsDisplayDto(x.TreatmentBmpModelingAttribute))
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
        var treatmentBmps = ListByPerson(dbContext, person).Where(x => x.ProjectID != null).ToList();
        return GetDisplayDtos(dbContext, treatmentBmps);
    }

    public static List<TreatmentBMPDisplayDto> ListWithOCTAM2Tier2GrantProgramByPerson(NeptuneDbContext dbContext,
                                                                                       PersonDto person)
    {
        var treatmentBmps = ListByPerson(dbContext, person).Where(x => x.Project is { ShareOCTAM2Tier2Scores: true }).ToList();
        return GetDisplayDtos(dbContext, treatmentBmps);
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
            .Include(x => x.CustomAttributes)
            .ThenInclude(x => x.CustomAttributeValues)
            .Include(x => x.CustomAttributes)
            .ThenInclude(x => x.CustomAttributeType)
            .AsNoTracking()
            .Where(x => x.ProjectID == projectID).ToList();

        var treatmentBMPIDs = treatmentBMPs.Select(x => x.TreatmentBMPID).ToList();

        var treatmentBMPModelingAttributes = dbContext.vTreatmentBMPModelingAttributes.Where(x => treatmentBMPIDs.Contains(x.TreatmentBMPID)).ToList();

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
        var treatmentBmps = GetTreatmentBMPsDisplayOnlyImpl(dbContext).ToList();

        return GetDisplayDtos(dbContext, treatmentBmps);
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

        var treatmentBMPs = GetTreatmentBMPsDisplayOnlyImpl(dbContext)
            .Where(x => jurisdictionIDs.Contains(x.StormwaterJurisdictionID)).ToList();

        return GetDisplayDtos(dbContext, treatmentBMPs);
    }

    public static List<TreatmentBMPTypeWithModelingAttributesDto> ListWithModelingAttributesAsDto(
        NeptuneDbContext dbContext)
    {
        var treatmentBMPTypeWithModelingAttributesDtos = dbContext.TreatmentBMPTypes
            .Include(x => x.TreatmentBMPTypeCustomAttributeTypes)
            .ThenInclude(x => x.CustomAttributeType)
            .AsNoTracking()
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

    public static async Task<TreatmentBMPDto> GetByIDAsDtoAsync(NeptuneDbContext dbContext, int treatmentBMPID)
    {
        var treatmentBMP = await dbContext.TreatmentBMPs.AsNoTracking()
            .Include(x => x.TreatmentBMPType)
            .Include(x => x.StormwaterJurisdiction).ThenInclude(x => x.Organization)
            .Include(x => x.OwnerOrganization)
            .Include(x => x.WaterQualityManagementPlan)
            .Include(x => x.Delineation)
            .SingleAsync(x => x.TreatmentBMPID == treatmentBMPID);

        var dto = treatmentBMP.AsDto();
        return dto;
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

    public static TreatmentBMP? GetUpstreamestTreatmentBMP(NeptuneDbContext dbContext, int treatmentBMPID)
    {
        var treatmentBMPTree = dbContext.vTreatmentBMPUpstreams.AsNoTracking()
            .Single(x => x.TreatmentBMPID == treatmentBMPID);

        var upstreamestBMP = treatmentBMPTree.UpstreamBMPID.HasValue
            ? GetByID(dbContext, treatmentBMPTree.UpstreamBMPID)
            : null;

        return upstreamestBMP;
    }

    public static List<TreatmentBMP> List(NeptuneDbContext dbContext)
    {
        return GetImpl(dbContext).AsNoTracking().OrderBy(x => x.TreatmentBMPName).ToList();
    }

    public static List<TreatmentBMP> ListModeledOnly(NeptuneDbContext dbContext)
    {
        return dbContext.TreatmentBMPs
            .Include(x => x.TreatmentBMPType)
            .Include(x => x.StormwaterJurisdiction)
            .ThenInclude(x => x.Organization)
            .Include(x => x.OwnerOrganization)
            .Include(x => x.UpstreamBMP)
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

    public static async Task<List<TreatmentBMPModelingAttributesDto>> ListWithModelingAttributesAsync(NeptuneDbContext dbContext, List<int> stormwaterJurisdictionIDsPersonCanView)
    {
        var treatmentBmps = await dbContext.TreatmentBMPs
            .Include(x => x.TreatmentBMPType)
            .Include(x => x.StormwaterJurisdiction)
            .ThenInclude(x => x.Organization)
            .Include(x => x.UpstreamBMP)
            .ThenInclude(x => x.TreatmentBMPType)
            .Include(x => x.Watershed)
            .Include(x => x.Delineation)
            .AsNoTracking()
            .Where(x => x.TreatmentBMPType.IsAnalyzedInModelingModule &&
                        stormwaterJurisdictionIDsPersonCanView.Contains(x.StormwaterJurisdictionID))
            .ToListAsync();

        var delineations = vTreatmentBMPUpstreams.ListWithDelineationAsDictionaryIncludeTreatmentBMPType(dbContext);
        var watersheds = await dbContext.Watersheds.AsNoTracking().Select(x => new { x.WatershedID, x.WatershedName }).ToDictionaryAsync(x => x.WatershedID, x => x.WatershedName);
        var precipitationZones = await dbContext.PrecipitationZones.AsNoTracking()
            .Select(x => new { x.PrecipitationZoneID, x.DesignStormwaterDepthInInches })
            .ToDictionaryAsync(x => x.PrecipitationZoneID, x => x.DesignStormwaterDepthInInches);

        var modeledLandUseAreas = await dbContext.vTreatmentBMPModeledLandUseAreas.AsNoTracking().ToDictionaryAsync(x => x.TreatmentBMPID.Value, x => x.Area);
        var modelingAttributes = await dbContext.vTreatmentBMPModelingAttributes.AsNoTracking().ToDictionaryAsync(x => x.TreatmentBMPID, x => x);

        return treatmentBmps.Select(bmp =>
        {
            var delineation = bmp.Delineation ?? (delineations.GetValueOrDefault(bmp.TreatmentBMPID));
            var modeling = modelingAttributes.GetValueOrDefault(bmp.TreatmentBMPID);
            var watershedName = bmp.WatershedID.HasValue && watersheds.TryGetValue(bmp.WatershedID.Value, out var watershed) ? watershed : null;
            var precipitationDepth = bmp.PrecipitationZoneID.HasValue && precipitationZones.TryGetValue(bmp.PrecipitationZoneID.Value, out var zone) ? zone : (double?)null;
            var modeledArea = modeledLandUseAreas.GetValueOrDefault(bmp.TreatmentBMPID, null);
            return new TreatmentBMPModelingAttributesDto
            {
                TreatmentBMPID = bmp.TreatmentBMPID,
                TreatmentBMPName = bmp.TreatmentBMPName,
                TreatmentBMPTypeID = bmp.TreatmentBMPTypeID,
                TreatmentBMPTypeName = bmp.TreatmentBMPType?.TreatmentBMPTypeName,
                StormwaterJurisdictionID = bmp.StormwaterJurisdictionID,
                StormwaterJurisdictionName = bmp.StormwaterJurisdiction?.Organization?.OrganizationName,
                WatershedID = bmp.WatershedID,
                WatershedName = watershedName,
                PrecipitationZoneID = bmp.PrecipitationZoneID,
                DesignStormwaterDepthInInches = precipitationDepth,
                DelineationID = delineation?.DelineationID,
                DelineationTypeName = delineation?.DelineationType?.DelineationTypeDisplayName,
                DelineationStatus = delineation?.GetDelineationStatus(),
                DelineationAreaAcres = delineation?.GetDelineationArea(),
                ModeledLandUseAreaAcres = modeledArea,
                IsFullyParameterized = bmp.IsFullyParameterized(delineation, modeling),
                AverageDivertedFlowrate = modeling?.AverageDivertedFlowrate,
                AverageTreatmentFlowrate = modeling?.AverageTreatmentFlowrate,
                DesignDryWeatherTreatmentCapacity = modeling?.DesignDryWeatherTreatmentCapacity,
                DesignLowFlowDiversionCapacity = modeling?.DesignLowFlowDiversionCapacity,
                DesignMediaFiltrationRate = modeling?.DesignMediaFiltrationRate,
                DrawdownTimeForWQDetentionVolume = modeling?.DrawdownTimeForWQDetentionVolume,
                EffectiveFootprint = modeling?.EffectiveFootprint,
                EffectiveRetentionDepth = modeling?.EffectiveRetentionDepth,
                InfiltrationDischargeRate = modeling?.InfiltrationDischargeRate,
                InfiltrationSurfaceArea = modeling?.InfiltrationSurfaceArea,
                MediaBedFootprint = modeling?.MediaBedFootprint,
                MonthsOperational = modeling?.MonthsOperational,
                PermanentPoolOrWetlandVolume = modeling?.PermanentPoolOrWetlandVolume,
                StorageVolumeBelowLowestOutletElevation = modeling?.StorageVolumeBelowLowestOutletElevation,
                SummerHarvestedWaterDemand = modeling?.SummerHarvestedWaterDemand,
                TimeOfConcentration = modeling?.TimeOfConcentration,
                TotalEffectiveBMPVolume = modeling?.TotalEffectiveBMPVolume,
                TotalEffectiveDrywellBMPVolume = modeling?.TotalEffectiveDrywellBMPVolume,
                TreatmentRate = modeling?.TreatmentRate,
                UnderlyingHydrologicSoilGroup = modeling?.UnderlyingHydrologicSoilGroup,
                UnderlyingInfiltrationRate = modeling?.UnderlyingInfiltrationRate,
                ExtendedDetentionSurchargeVolume = modeling?.ExtendedDetentionSurchargeVolume,
                WettedFootprint = modeling?.WettedFootprint,
                WinterHarvestedWaterDemand = modeling?.WinterHarvestedWaterDemand,
                UpstreamBMPID = bmp.UpstreamBMPID,
                UpstreamBMPName = bmp.UpstreamBMP?.TreatmentBMPName,
                DownstreamOfNonModeledBMP = bmp is { UpstreamBMPID: not null, UpstreamBMP: not null } && !(bmp.UpstreamBMP.TreatmentBMPType?.IsAnalyzedInModelingModule ?? true),
                DryWeatherFlowOverride = modeling?.DryWeatherFlowOverride
            };
        }).ToList();
    }

    public static async Task<List<ErrorMessage>> ValidateUpdateBasicInfoAsync(NeptuneDbContext dbContext, int treatmentBMPID, TreatmentBMPBasicInfoUpdate updateDto)
    {
        var errors = await ValidateBasicInfoAsync(dbContext, updateDto, treatmentBMPID);
        return errors;
    }

    public static async Task<TreatmentBMPDto> UpdateBasicInfoAsync(NeptuneDbContext dbContext, int treatmentBMPID, TreatmentBMPBasicInfoUpdate updateDto, PersonDto callingUser)
    {
        var treatmentBMPToUpdate = dbContext.TreatmentBMPs
            .Include(x => x.StormwaterJurisdiction)
            .Single(x => x.TreatmentBMPID == treatmentBMPID);

        treatmentBMPToUpdate.TreatmentBMPName = updateDto.TreatmentBMPName;
        treatmentBMPToUpdate.OwnerOrganizationID = updateDto.OwnerOrganizationID ?? treatmentBMPToUpdate.StormwaterJurisdiction.OrganizationID;
        treatmentBMPToUpdate.YearBuilt = updateDto.YearBuilt;
        treatmentBMPToUpdate.SystemOfRecordID = updateDto.SystemOfRecordID;
        treatmentBMPToUpdate.WaterQualityManagementPlanID = updateDto.WaterQualityManagementPlanID;
        treatmentBMPToUpdate.TreatmentBMPLifespanTypeID = updateDto.TreatmentBMPLifespanTypeID;
        treatmentBMPToUpdate.TreatmentBMPLifespanEndDate = updateDto.TreatmentBMPLifespanTypeID == TreatmentBMPLifespanType.FixedEndDate.TreatmentBMPLifespanTypeID
            ? updateDto.TreatmentBMPLifespanEndDate
            : null;
        treatmentBMPToUpdate.SizingBasisTypeID = updateDto.SizingBasisTypeID;
        treatmentBMPToUpdate.TrashCaptureStatusTypeID = updateDto.TrashCaptureStatusTypeID;
        treatmentBMPToUpdate.TrashCaptureEffectiveness = updateDto.TrashCaptureStatusTypeID == TrashCaptureStatusType.Partial.TrashCaptureStatusTypeID 
            ? updateDto.TrashCaptureEffectiveness
            : null;
        treatmentBMPToUpdate.RequiredFieldVisitsPerYear = updateDto.RequiredFieldVisitsPerYear;
        treatmentBMPToUpdate.RequiredPostStormFieldVisitsPerYear = updateDto.RequiredPostStormFieldVisitsPerYear;
        treatmentBMPToUpdate.Notes = updateDto.Notes;

        await dbContext.SaveChangesAsync();
        await dbContext.Entry(treatmentBMPToUpdate).ReloadAsync();

        var updatedTreatmentBMPDto = await GetByIDAsDtoAsync(dbContext, treatmentBMPID);
        return updatedTreatmentBMPDto;
    } 
    
    // Shared validation for fields present on both Create and BasicInfo Update DTOs
    private static async Task<List<ErrorMessage>> ValidateBasicInfoAsync(NeptuneDbContext dbContext, IHaveTreatmentBMPBasicInfo dto, int? existingTreatmentBMPID = null)
    {
        var errors = new List<ErrorMessage>();

        // Validate Name Uniqueness (exclude existingTreatmentBMPID when updating)
        var hasUniqueName = await dbContext.TreatmentBMPs.AsNoTracking()
            .AllAsync(x => x.TreatmentBMPID == existingTreatmentBMPID || x.TreatmentBMPName != dto.TreatmentBMPName);

        if (!hasUniqueName)
        {
            errors.Add(new ErrorMessage("TreatmentBMPName", "Treatment BMP Name must be unique."));
        }

        // Owner organization (optional)
        if (dto.OwnerOrganizationID.HasValue)
        {
            var hasValidOwner = await dbContext.Organizations.AnyAsync(x => x.OrganizationID == dto.OwnerOrganizationID.Value);
            if (!hasValidOwner)
            {
                errors.Add(new ErrorMessage("OwnerOrganizationID", "Valid Owner Organization is required."));
            }
        }

        // Sizing basis
        var hasValidSizingBasis = SizingBasisType.All.Any(x => x.SizingBasisTypeID == dto.SizingBasisTypeID);
        if (!hasValidSizingBasis)
        {
            errors.Add(new ErrorMessage("SizingBasisTypeID", "Valid Sizing Basis Type is required."));
        }

        // Trash capture status
        var hasValidTrashCapture = TrashCaptureStatusType.All.Any(x => x.TrashCaptureStatusTypeID == dto.TrashCaptureStatusTypeID);
        if (!hasValidTrashCapture)
        {
            errors.Add(new ErrorMessage("TrashCaptureStatusTypeID", "Valid Trash Capture Status Type is required."));
        }

        // Lifespan type
        var hasValidLifespan = TreatmentBMPLifespanType.All.Any(x => x.TreatmentBMPLifespanTypeID == dto.TreatmentBMPLifespanTypeID);
        if (!hasValidLifespan)
        {
            errors.Add(new ErrorMessage("TreatmentBMPLifespanTypeID", "Valid Lifespan Type is required."));
        }

        // Lifespan end date required if type is Fixed End Date
        if (dto.TreatmentBMPLifespanTypeID == TreatmentBMPLifespanType.FixedEndDate.TreatmentBMPLifespanTypeID && !dto.TreatmentBMPLifespanEndDate.HasValue)
        {
            errors.Add(new ErrorMessage("LifespanEndDate", "The Lifespan End Date must be set if the Lifespan Type is Fixed End Date."));
        }

        // Water quality management plan
        var hasValidWQMP = await dbContext.WaterQualityManagementPlans.AnyAsync(x => x.WaterQualityManagementPlanID == dto.WaterQualityManagementPlanID);
        if (!hasValidWQMP)
        {
            errors.Add(new ErrorMessage("WaterQualityManagementPlanID", "Valid Water Quality Management Plan is required."));
        }

        return errors;
    }
}
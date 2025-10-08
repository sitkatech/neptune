using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;
using Neptune.Models.DataTransferObjects;
using Neptune.Models.DataTransferObjects.WaterQualityManagementPlan;

namespace Neptune.EFModels.Entities;

public static class WaterQualityManagementPlans
{
    public static IQueryable<WaterQualityManagementPlan> GetImpl(NeptuneDbContext dbContext)
    {
        return dbContext.WaterQualityManagementPlans
            .Include(x => x.StormwaterJurisdiction)
            .ThenInclude(x => x.Organization)
            ;
    }

    public static WaterQualityManagementPlan GetByIDWithChangeTracking(NeptuneDbContext dbContext,
        int waterQualityManagementPlanID)
    {
        var waterQualityManagementPlan = GetImpl(dbContext)
            .SingleOrDefault(x => x.WaterQualityManagementPlanID == waterQualityManagementPlanID);
        Check.RequireNotNull(waterQualityManagementPlan,
            $"WaterQualityManagementPlan with ID {waterQualityManagementPlanID} not found!");
        return waterQualityManagementPlan;
    }

    public static WaterQualityManagementPlan GetByIDWithChangeTracking(NeptuneDbContext dbContext,
        WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey)
    {
        return GetByIDWithChangeTracking(dbContext, waterQualityManagementPlanPrimaryKey.PrimaryKeyValue);
    }

    public static WaterQualityManagementPlan GetByID(NeptuneDbContext dbContext, int waterQualityManagementPlanID)
    {
        var waterQualityManagementPlan = GetImpl(dbContext)
            .Include(x => x.TreatmentBMPs)
            .ThenInclude(x => x.TreatmentBMPType)
            .Include(x => x.StormwaterJurisdiction)
            .ThenInclude(x => x.StormwaterJurisdictionGeometry)
            .Include(x => x.HydrologicSubarea)
            .AsNoTracking()
            .SingleOrDefault(x => x.WaterQualityManagementPlanID == waterQualityManagementPlanID);
        Check.RequireNotNull(waterQualityManagementPlan,
            $"WaterQualityManagementPlan with ID {waterQualityManagementPlanID} not found!");
        return waterQualityManagementPlan;
    }

    public static WaterQualityManagementPlan GetByID(NeptuneDbContext dbContext,
        WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey)
    {
        return GetByID(dbContext, waterQualityManagementPlanPrimaryKey.PrimaryKeyValue);
    }

    public static WaterQualityManagementPlan GetByIDForFeatureContextCheck(NeptuneDbContext dbContext, int waterQualityManagementPlanID)
    {
        var waterQualityManagementPlan = dbContext.WaterQualityManagementPlans
            .Include(x => x.StormwaterJurisdiction)
            .ThenInclude(x => x.Organization).AsNoTracking()
            .SingleOrDefault(x => x.WaterQualityManagementPlanID == waterQualityManagementPlanID);
        Check.RequireNotNull(waterQualityManagementPlan, $"WaterQualityManagementPlan with ID {waterQualityManagementPlanID} not found!");
        return waterQualityManagementPlan;
    }

    public static List<WaterQualityManagementPlan> ListViewableByPerson(NeptuneDbContext dbContext, Person person)
    {
        var stormwaterJurisdictionIDsPersonCanView = StormwaterJurisdictionPeople.ListViewableStormwaterJurisdictionIDsByPersonForWQMPs(dbContext, person);

        //These users can technically see all Jurisdictions, just potentially not the WQMPs inside them
        var waterQualityManagementPlans = GetImpl(dbContext)
            .AsNoTracking()
            .Where(x => stormwaterJurisdictionIDsPersonCanView.Contains(x.StormwaterJurisdictionID));
        if (person.IsAnonymousOrUnassigned())
        {
            var publicWaterQualityManagementPlans = waterQualityManagementPlans.Where(x =>
                x.WaterQualityManagementPlanStatusID ==
                (int)WaterQualityManagementPlanStatusEnum.Active ||
                x.WaterQualityManagementPlanStatusID ==
                (int)WaterQualityManagementPlanStatusEnum.Inactive &&
                x.StormwaterJurisdiction.StormwaterJurisdictionPublicWQMPVisibilityTypeID ==
                (int)StormwaterJurisdictionPublicWQMPVisibilityTypeEnum.ActiveAndInactive).ToList();
            return publicWaterQualityManagementPlans;
        }

        return waterQualityManagementPlans.ToList();
    }


    public static IEnumerable<WaterQualityManagementPlan> ListByStormwaterJurisdictionID(NeptuneDbContext dbContext, int stormwaterJurisdictionID)
    {
        return GetImpl(dbContext)
            .AsNoTracking()
            .Where(x => x.StormwaterJurisdictionID == stormwaterJurisdictionID).ToList();
    }

    public static async Task<List<WaterQualityManagementPlanDto>> ListAsDtoAsync(NeptuneDbContext dbContext)
    {
        var entities = await GetImpl(dbContext).AsNoTracking().ToListAsync();
        return entities.Select(x => x.AsDto()).ToList();
    }

    public static async Task<WaterQualityManagementPlanDto?> GetByIDAsDtoAsync(NeptuneDbContext dbContext, int waterQualityManagementPlanID)
    {
        var entity = await GetImpl(dbContext).AsNoTracking().FirstOrDefaultAsync(x => x.WaterQualityManagementPlanID == waterQualityManagementPlanID);
        return entity?.AsDto();
    }

    public static async Task<WaterQualityManagementPlanDto> CreateAsync(NeptuneDbContext dbContext, WaterQualityManagementPlanUpsertDto dto)
    {
        var entity = dto.AsEntity();
        dbContext.WaterQualityManagementPlans.Add(entity);
        await dbContext.SaveChangesAsync();
        return await GetByIDAsDtoAsync(dbContext, entity.WaterQualityManagementPlanID);
    }

    public static async Task<WaterQualityManagementPlanDto?> UpdateAsync(NeptuneDbContext dbContext, int waterQualityManagementPlanID, WaterQualityManagementPlanUpsertDto dto)
    {
        var entity = await dbContext.WaterQualityManagementPlans.FirstAsync(x => x.WaterQualityManagementPlanID == waterQualityManagementPlanID);
        entity.UpdateFromUpsertDto(dto);
        await dbContext.SaveChangesAsync();
        return await GetByIDAsDtoAsync(dbContext, entity.WaterQualityManagementPlanID);
    }

    public static async Task<bool> DeleteAsync(NeptuneDbContext dbContext, int waterQualityManagementPlanID)
    {
        var entity = await dbContext.WaterQualityManagementPlans.FirstOrDefaultAsync(x => x.WaterQualityManagementPlanID == waterQualityManagementPlanID);
        if (entity == null) return false;
        await entity.DeleteFull(dbContext);
        return true;
    }

    public static async Task<WaterQualityManagementPlanExtractDto?> GetByIDAsExtractDtoAsync(NeptuneDbContext dbContext, int waterQualityManagementPlanID)
    {
        var entity = await dbContext.WaterQualityManagementPlans
            .Include(x => x.StormwaterJurisdiction)
            .ThenInclude(x => x.Organization)
            .Include(x => x.TreatmentBMPs)
            .Include(x => x.QuickBMPs)
            .Include(x => x.SourceControlBMPs)
            .Include(x => x.WaterQualityManagementPlanParcels).ThenInclude(x => x.Parcel)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.WaterQualityManagementPlanID == waterQualityManagementPlanID);
        if (entity == null) return null;
        // You may need to implement AsExtractDto() on the entity
        return entity.AsExtractDto();
    }

    public static WaterQualityManagementPlanExtractDto AsExtractDto(this WaterQualityManagementPlan entity)
    {
        return new WaterQualityManagementPlanExtractDto
        {
            WaterQualityManagementPlanID = entity.WaterQualityManagementPlanID,
            WaterQualityManagementPlanName = entity.WaterQualityManagementPlanName,
            StormwaterJurisdictionID = entity.StormwaterJurisdictionID,
            WaterQualityManagementPlanLandUseID = entity.WaterQualityManagementPlanLandUseID,
            WaterQualityManagementPlanPriorityID = entity.WaterQualityManagementPlanPriorityID,
            WaterQualityManagementPlanStatusID = entity.WaterQualityManagementPlanStatusID,
            WaterQualityManagementPlanDevelopmentTypeID = entity.WaterQualityManagementPlanDevelopmentTypeID,
            ApprovalDate = entity.ApprovalDate,
            MaintenanceContactName = entity.MaintenanceContactName,
            MaintenanceContactOrganization = entity.MaintenanceContactOrganization,
            MaintenanceContactPhone = entity.MaintenanceContactPhone,
            MaintenanceContactAddress1 = entity.MaintenanceContactAddress1,
            MaintenanceContactAddress2 = entity.MaintenanceContactAddress2,
            MaintenanceContactCity = entity.MaintenanceContactCity,
            MaintenanceContactState = entity.MaintenanceContactState,
            MaintenanceContactZip = entity.MaintenanceContactZip,
            WaterQualityManagementPlanPermitTermID = entity.WaterQualityManagementPlanPermitTermID,
            HydromodificationAppliesTypeID = entity.HydromodificationAppliesTypeID,
            DateOfConstruction = entity.DateOfConstruction,
            HydrologicSubareaID = entity.HydrologicSubareaID,
            RecordNumber = entity.RecordNumber,
            RecordedWQMPAreaInAcres = entity.RecordedWQMPAreaInAcres,
            TrashCaptureStatusTypeID = entity.TrashCaptureStatusTypeID,
            TrashCaptureEffectiveness = entity.TrashCaptureEffectiveness,
            WaterQualityManagementPlanModelingApproachID = entity.WaterQualityManagementPlanModelingApproachID,
            WaterQualityManagementPlanBoundaryNotes = entity.WaterQualityManagementPlanBoundaryNotes,
            Parcels = entity.WaterQualityManagementPlanParcels?.Select(p => new WaterQualityManagementPlanParcelExtractDto
            {
                WaterQualityManagementPlanParcelID = p.WaterQualityManagementPlanParcelID,
                ParcelID = p.ParcelID
            }).ToList() ?? new List<WaterQualityManagementPlanParcelExtractDto>(),
            QuickBMPs = entity.QuickBMPs?.Select(q => new QuickBMPExtractDto
            {
                QuickBMPID = q.QuickBMPID,
                QuickBMPName = q.QuickBMPName,
                QuickBMPNote = q.QuickBMPNote,
                PercentOfSiteTreated = q.PercentOfSiteTreated,
                PercentCaptured = q.PercentCaptured,
                PercentRetained = q.PercentRetained,
                DryWeatherFlowOverrideID = q.DryWeatherFlowOverrideID,
                NumberOfIndividualBMPs = q.NumberOfIndividualBMPs,
                TreatmentBMPTypeID = q.TreatmentBMPTypeID
            }).ToList() ?? new List<QuickBMPExtractDto>(),
            TreatmentBMPs = entity.TreatmentBMPs?.Select(t => new TreatmentBMPExtractDto
            {
                TreatmentBMPID = t.TreatmentBMPID,
                TreatmentBMPName = t.TreatmentBMPName,
                TreatmentBMPTypeID = t.TreatmentBMPTypeID,
                LocationPointWkt = t.LocationPoint4326.AsText(),
                StormwaterJurisdictionID = t.StormwaterJurisdictionID,
                Notes = t.Notes,
                SystemOfRecordID = t.SystemOfRecordID,
                YearBuilt = t.YearBuilt,
                OwnerOrganizationID = t.OwnerOrganizationID,
                WaterQualityManagementPlanID = t.WaterQualityManagementPlanID,
                TreatmentBMPLifespanTypeID = t.TreatmentBMPLifespanTypeID,
                TreatmentBMPLifespanEndDate = t.TreatmentBMPLifespanEndDate,
                RequiredFieldVisitsPerYear = t.RequiredFieldVisitsPerYear,
                RequiredPostStormFieldVisitsPerYear = t.RequiredPostStormFieldVisitsPerYear,
                InventoryIsVerified = t.InventoryIsVerified,
                DateOfLastInventoryVerification = t.DateOfLastInventoryVerification,
                InventoryVerifiedByPersonID = t.InventoryVerifiedByPersonID,
                InventoryLastChangedDate = t.InventoryLastChangedDate,
                TrashCaptureStatusTypeID = t.TrashCaptureStatusTypeID,
                SizingBasisTypeID = t.SizingBasisTypeID,
                TrashCaptureEffectiveness = t.TrashCaptureEffectiveness,
                WatershedID = t.WatershedID,
                ModelBasinID = t.ModelBasinID,
                PrecipitationZoneID = t.PrecipitationZoneID,
                UpstreamBMPID = t.UpstreamBMPID,
                RegionalSubbasinID = t.RegionalSubbasinID,
                ProjectID = t.ProjectID,
                LastNereidLogID = t.LastNereidLogID
            }).ToList() ?? new List<TreatmentBMPExtractDto>(),
            SourceControlBMPs = entity.SourceControlBMPs?.Select(s => new SourceControlBMPExtractDto
            {
                SourceControlBMPID = s.SourceControlBMPID,
                SourceControlBMPAttributeID = s.SourceControlBMPAttributeID,
                IsPresent = s.IsPresent,
                SourceControlBMPNote = s.SourceControlBMPNote
            }).ToList() ?? new List<SourceControlBMPExtractDto>()
        };
    }
}
﻿using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;

namespace Neptune.EFModels.Entities;

public static class MaintenanceRecordObservations
{
    public static IQueryable<MaintenanceRecordObservation> GetImpl(NeptuneDbContext dbContext)
    {
        return dbContext.MaintenanceRecordObservations
                .Include(x => x.CustomAttributeType)
                .Include(x => x.MaintenanceRecordObservationValues)
                .Include(x => x.TreatmentBMPTypeCustomAttributeType)
                .ThenInclude(x => x.CustomAttributeType)
            ;
    }

    public static MaintenanceRecordObservation GetByIDWithChangeTracking(NeptuneDbContext dbContext,
        int maintenanceRecordObservationID)
    {
        var maintenanceRecordObservation = GetImpl(dbContext)
            .SingleOrDefault(x => x.MaintenanceRecordObservationID == maintenanceRecordObservationID);
        Check.RequireNotNull(maintenanceRecordObservation,
            $"MaintenanceRecordObservation with ID {maintenanceRecordObservationID} not found!");
        return maintenanceRecordObservation;
    }

    public static MaintenanceRecordObservation GetByIDWithChangeTracking(NeptuneDbContext dbContext,
        MaintenanceRecordObservationPrimaryKey maintenanceRecordObservationPrimaryKey)
    {
        return GetByIDWithChangeTracking(dbContext, maintenanceRecordObservationPrimaryKey.PrimaryKeyValue);
    }

    public static MaintenanceRecordObservation GetByID(NeptuneDbContext dbContext, int maintenanceRecordObservationID)
    {
        var maintenanceRecordObservation = GetImpl(dbContext).AsNoTracking()
            .SingleOrDefault(x => x.MaintenanceRecordObservationID == maintenanceRecordObservationID);
        Check.RequireNotNull(maintenanceRecordObservation,
            $"MaintenanceRecordObservation with ID {maintenanceRecordObservationID} not found!");
        return maintenanceRecordObservation;
    }

    public static MaintenanceRecordObservation GetByID(NeptuneDbContext dbContext,
        MaintenanceRecordObservationPrimaryKey maintenanceRecordObservationPrimaryKey)
    {
        return GetByID(dbContext, maintenanceRecordObservationPrimaryKey.PrimaryKeyValue);
    }

    public static List<MaintenanceRecordObservation> ListByCustomAttributeTypeID(NeptuneDbContext dbContext, int customAttributeTypeID)
    {
        return GetImpl(dbContext).AsNoTracking().Where(x => x.CustomAttributeTypeID == customAttributeTypeID).OrderBy(x => x.MaintenanceRecordObservationID).ToList();
    }
}
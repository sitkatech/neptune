using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;

namespace Neptune.EFModels.Entities;

public static class MaintenanceRecords
{
    private static IQueryable<MaintenanceRecord> GetImpl(NeptuneDbContext dbContext)
    {
        return dbContext.MaintenanceRecords
            .Include(x => x.FieldVisit)
            .Include(x => x.FieldVisit).ThenInclude(x => x.PerformedByPerson)
            .Include(x => x.TreatmentBMP)
            .ThenInclude(x => x.StormwaterJurisdiction)
            .ThenInclude(x => x.Organization)
            .Include(x => x.TreatmentBMPType)
            .Include(x => x.MaintenanceRecordObservations).ThenInclude(x => x.MaintenanceRecordObservationValues);
    }

    public static MaintenanceRecord GetByIDWithChangeTracking(NeptuneDbContext dbContext, int maintenanceRecordID)
    {
        var maintenanceRecord = GetImpl(dbContext)
            .SingleOrDefault(x => x.MaintenanceRecordID == maintenanceRecordID);
        Check.RequireNotNull(maintenanceRecord, $"MaintenanceRecord with ID {maintenanceRecordID} not found!");
        return maintenanceRecord;
    }

    public static MaintenanceRecord GetByIDWithChangeTracking(NeptuneDbContext dbContext, MaintenanceRecordPrimaryKey maintenanceRecordPrimaryKey)
    {
        return GetByIDWithChangeTracking(dbContext, maintenanceRecordPrimaryKey.PrimaryKeyValue);
    }

    public static MaintenanceRecord GetByID(NeptuneDbContext dbContext, int maintenanceRecordID)
    {
        var maintenanceRecord = GetImpl(dbContext).AsNoTracking()
            .SingleOrDefault(x => x.MaintenanceRecordID == maintenanceRecordID);
        Check.RequireNotNull(maintenanceRecord, $"MaintenanceRecord with ID {maintenanceRecordID} not found!");
        return maintenanceRecord;
    }

    public static MaintenanceRecord GetByID(NeptuneDbContext dbContext, MaintenanceRecordPrimaryKey maintenanceRecordPrimaryKey)
    {
        return GetByID(dbContext, maintenanceRecordPrimaryKey.PrimaryKeyValue);
    }

    public static List<MaintenanceRecord> List(NeptuneDbContext dbContext)
    {
        return GetImpl(dbContext).AsNoTracking().ToList();
    }
}
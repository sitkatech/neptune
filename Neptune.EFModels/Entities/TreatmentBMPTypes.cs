﻿using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;

namespace Neptune.EFModels.Entities;

public static class TreatmentBMPTypes
{
    private static IQueryable<TreatmentBMPType> GetImpl(NeptuneDbContext dbContext)
    {
        return dbContext.TreatmentBMPTypes
            .Include(x => x.TreatmentBMPTypeCustomAttributeTypes)
            .ThenInclude(x => x.CustomAttributeType);
    }

    public static TreatmentBMPType GetByIDWithChangeTracking(NeptuneDbContext dbContext, int treatmentBMPTypeID)
    {
        var treatmentBMPType = GetImpl(dbContext)
            .SingleOrDefault(x => x.TreatmentBMPTypeID == treatmentBMPTypeID);
        Check.RequireNotNull(treatmentBMPType, $"TreatmentBMPType with ID {treatmentBMPTypeID} not found!");
        return treatmentBMPType;
    }

    public static TreatmentBMPType GetByIDWithChangeTracking(NeptuneDbContext dbContext, TreatmentBMPTypePrimaryKey treatmentBMPTypePrimaryKey)
    {
        return GetByIDWithChangeTracking(dbContext, treatmentBMPTypePrimaryKey.PrimaryKeyValue);
    }

    public static TreatmentBMPType GetByID(NeptuneDbContext dbContext, int treatmentBMPTypeID)
    {
        var treatmentBMPType = GetImpl(dbContext).AsNoTracking()
            .SingleOrDefault(x => x.TreatmentBMPTypeID == treatmentBMPTypeID);
        Check.RequireNotNull(treatmentBMPType, $"TreatmentBMPType with ID {treatmentBMPTypeID} not found!");
        return treatmentBMPType;
    }

    public static TreatmentBMPType GetByID(NeptuneDbContext dbContext, TreatmentBMPTypePrimaryKey treatmentBMPTypePrimaryKey)
    {
        return GetByID(dbContext, treatmentBMPTypePrimaryKey.PrimaryKeyValue);
    }

    public static List<TreatmentBMPType> List(NeptuneDbContext dbContext)
    {
        return GetImpl(dbContext).AsNoTracking().OrderBy(x => x.TreatmentBMPTypeName).ToList();
    }
}
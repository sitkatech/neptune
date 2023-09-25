using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;

namespace Neptune.EFModels.Entities;

public static class SourceControlBMPAttributes
{
    public static IQueryable<SourceControlBMPAttribute> GetImpl(NeptuneDbContext dbContext)
    {
        return dbContext.SourceControlBMPAttributes;
    }

    public static SourceControlBMPAttribute GetByIDWithChangeTracking(NeptuneDbContext dbContext,
        int sourceControlBMPAttributeID)
    {
        var sourceControlBMPAttribute = GetImpl(dbContext)
            .SingleOrDefault(x => x.SourceControlBMPAttributeID == sourceControlBMPAttributeID);
        Check.RequireNotNull(sourceControlBMPAttribute,
            $"SourceControlBMPAttribute with ID {sourceControlBMPAttributeID} not found!");
        return sourceControlBMPAttribute;
    }

    public static SourceControlBMPAttribute GetByIDWithChangeTracking(NeptuneDbContext dbContext,
        SourceControlBMPAttributePrimaryKey sourceControlBMPAttributePrimaryKey)
    {
        return GetByIDWithChangeTracking(dbContext, sourceControlBMPAttributePrimaryKey.PrimaryKeyValue);
    }

    public static SourceControlBMPAttribute GetByID(NeptuneDbContext dbContext, int sourceControlBMPAttributeID)
    {
        var sourceControlBMPAttribute = GetImpl(dbContext).AsNoTracking()
            .SingleOrDefault(x => x.SourceControlBMPAttributeID == sourceControlBMPAttributeID);
        Check.RequireNotNull(sourceControlBMPAttribute,
            $"SourceControlBMPAttribute with ID {sourceControlBMPAttributeID} not found!");
        return sourceControlBMPAttribute;
    }

    public static SourceControlBMPAttribute GetByID(NeptuneDbContext dbContext,
        SourceControlBMPAttributePrimaryKey sourceControlBMPAttributePrimaryKey)
    {
        return GetByID(dbContext, sourceControlBMPAttributePrimaryKey.PrimaryKeyValue);
    }

    public static List<SourceControlBMPAttribute> List(NeptuneDbContext dbContext)
    {
        return GetImpl(dbContext).AsNoTracking().ToList();
    }
}
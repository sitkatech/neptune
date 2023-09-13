using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;

namespace Neptune.EFModels.Entities;

public static class CustomAttributes
{
    public static IQueryable<CustomAttribute> GetImpl(NeptuneDbContext dbContext)
    {
        return dbContext.CustomAttributes
                .Include(x => x.CustomAttributeType)
                .Include(x => x.CustomAttributeValues)
            ;
    }

    public static CustomAttribute GetByIDWithChangeTracking(NeptuneDbContext dbContext,
        int customAttributeID)
    {
        var customAttribute = GetImpl(dbContext)
            .SingleOrDefault(x => x.CustomAttributeID == customAttributeID);
        Check.RequireNotNull(customAttribute,
            $"CustomAttribute with ID {customAttributeID} not found!");
        return customAttribute;
    }

    public static CustomAttribute GetByIDWithChangeTracking(NeptuneDbContext dbContext,
        CustomAttributePrimaryKey customAttributePrimaryKey)
    {
        return GetByIDWithChangeTracking(dbContext, customAttributePrimaryKey.PrimaryKeyValue);
    }

    public static CustomAttribute GetByID(NeptuneDbContext dbContext, int customAttributeID)
    {
        var customAttribute = GetImpl(dbContext).AsNoTracking()
            .SingleOrDefault(x => x.CustomAttributeID == customAttributeID);
        Check.RequireNotNull(customAttribute,
            $"CustomAttribute with ID {customAttributeID} not found!");
        return customAttribute;
    }

    public static CustomAttribute GetByID(NeptuneDbContext dbContext,
        CustomAttributePrimaryKey customAttributePrimaryKey)
    {
        return GetByID(dbContext, customAttributePrimaryKey.PrimaryKeyValue);
    }

    public static List<CustomAttribute> List(NeptuneDbContext dbContext)
    {
        return GetImpl(dbContext).AsNoTracking().OrderBy(x => x.CustomAttributeType.CustomAttributeTypeName).ToList();
    }

    public static List<CustomAttribute> ListByTreatmentBMPID(NeptuneDbContext dbContext, int treatmentBMPID)
    {
        return GetImpl(dbContext).AsNoTracking().Where(x => x.TreatmentBMPID == treatmentBMPID).OrderBy(x => x.CustomAttributeType.CustomAttributeTypeName).ToList();
    }

    public static List<CustomAttribute> ListByTreatmentBMPIDWithChangeTracking(NeptuneDbContext dbContext, int treatmentBMPID)
    {
        return GetImpl(dbContext).Where(x => x.TreatmentBMPID == treatmentBMPID).OrderBy(x => x.CustomAttributeType.CustomAttributeTypeName).ToList();
    }

    public static List<CustomAttribute> ListByTreatmentBMPIDAndPurposes(NeptuneDbContext dbContext, int treatmentBMPID, CustomAttributeTypePurposeEnum customAttributeTypePurposeEnum)
    {
        return GetImpl(dbContext).AsNoTracking().Where(x => x.TreatmentBMPID == treatmentBMPID && x.CustomAttributeType.CustomAttributeTypePurposeID == (int) customAttributeTypePurposeEnum).OrderBy(x => x.CustomAttributeType.CustomAttributeTypeName).ToList();
    }

    public static List<CustomAttribute> ListByTreatmentBMPIDAndPurposesWithChangeTracking(NeptuneDbContext dbContext, int treatmentBMPID, CustomAttributeTypePurposeEnum customAttributeTypePurposeEnum)
    {
        return GetImpl(dbContext).Where(x => x.TreatmentBMPID == treatmentBMPID && x.CustomAttributeType.CustomAttributeTypePurposeID == (int) customAttributeTypePurposeEnum).OrderBy(x => x.CustomAttributeType.CustomAttributeTypeName).ToList();
    }
}
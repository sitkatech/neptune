using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

public static class vTreatmentBMPModelingAttributes
{
    public static vTreatmentBMPModelingAttribute GetByTreatmentBMPID(NeptuneDbContext dbContext, int treatmentBMPID)
    {
        return dbContext.vTreatmentBMPModelingAttributes.AsNoTracking()
            .FirstOrDefault(x => x.TreatmentBMPID == treatmentBMPID);
    }
    public static Dictionary<int, vTreatmentBMPModelingAttribute> ListAsDictionary(NeptuneDbContext dbContext)
    {
        return dbContext.vTreatmentBMPModelingAttributes.AsNoTracking()
            .ToDictionary(x => x.TreatmentBMPID);
    }
}
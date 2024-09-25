using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

public static class TreatmentBMPNereidLogs
{
    public static TreatmentBMPNereidLog? GetByTreatmentBMPID(NeptuneDbContext dbContext, int treatmentBMPID)
    {
        return dbContext.TreatmentBMPNereidLogs.AsNoTracking()
            .SingleOrDefault(x => x.TreatmentBMPID == treatmentBMPID && x.LastRequestDate != null);
    }
}
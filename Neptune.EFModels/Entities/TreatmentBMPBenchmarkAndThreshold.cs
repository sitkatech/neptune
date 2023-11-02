using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

public partial class TreatmentBMPBenchmarkAndThreshold
{
    public async Task DeleteFull(NeptuneDbContext dbContext)
    {
        await dbContext.TreatmentBMPBenchmarkAndThresholds
            .Where(x => x.TreatmentBMPBenchmarkAndThresholdID == TreatmentBMPBenchmarkAndThresholdID).ExecuteDeleteAsync();
    }
}
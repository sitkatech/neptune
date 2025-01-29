using Microsoft.EntityFrameworkCore;
using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static class LandUseBlocks
{
    public static List<LandUseBlockGridDto> List(NeptuneDbContext dbContext)
    {
        var landUseBlock = dbContext.LandUseBlocks.AsNoTracking()
            .Include(x => x.StormwaterJurisdiction)
                .ThenInclude(x => x.Organization)
            .Include(x => x.TrashGeneratingUnits)
            .Select(x => x.AsGridDto()).ToList();
        return landUseBlock;
    }
}
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

public partial class CustomAttribute
{
    public async Task DeleteFull(NeptuneDbContext dbContext)
    {
        await dbContext.CustomAttributeValues.Where(x => x.CustomAttributeID == CustomAttributeID).ExecuteDeleteAsync();
        await dbContext.CustomAttributes.Where(x => x.CustomAttributeID == CustomAttributeID).ExecuteDeleteAsync();
    }
}
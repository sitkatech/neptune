using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

public partial class CustomAttribute
{
    public void DeleteFull(NeptuneDbContext dbContext)
    {
        dbContext.CustomAttributeValues.Where(x => x.CustomAttributeID == CustomAttributeID).ExecuteDelete();
        dbContext.CustomAttributes.Where(x => x.CustomAttributeID == CustomAttributeID).ExecuteDelete();
    }
}
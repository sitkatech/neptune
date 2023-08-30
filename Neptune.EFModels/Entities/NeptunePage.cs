using Neptune.Common.DesignByContract;

namespace Neptune.EFModels.Entities;

public partial class NeptunePage
{
    public bool HasNeptunePageContent()
    {
        return !string.IsNullOrWhiteSpace(NeptunePageContent);
    }

    public static NeptunePage GetNeptunePageByPageType(NeptuneDbContext dbContext, NeptunePageType neptunePageType)
    {
        var neptunePage = dbContext.NeptunePages.SingleOrDefault(x => x.NeptunePageTypeID == neptunePageType.NeptunePageTypeID);
        Check.RequireNotNull(neptunePage);
        return neptunePage;
    }
}
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;

namespace Neptune.Web.Views.LandUseBlock
{
    public class IndexViewData : NeptuneViewData
    {
        public LandUseBlockGridSpec GridSpec { get; }
        public string GridName { get; }
        public string GridDataUrl { get; }
        public IndexViewData(Person currentPerson) : base (currentPerson, NeptuneArea.OCStormwaterTools)
        {
            EntityName = "Land Use Block";
            PageTitle = "Index";
            GridSpec = new LandUseBlockGridSpec() { ObjectNameSingular = "Land Use Block", ObjectNamePlural = "Land Use Blocks", SaveFiltersInCookie = true };
            GridName = "landUseBlockGrid";
            GridDataUrl = SitkaRoute<LandUseBlockController>.BuildUrlFromExpression(j => j.LandUseBlockGridJsonData());
        }
    }
}
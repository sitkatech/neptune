using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.Shared;

namespace Neptune.Web.Views.LandUseBlock
{
    public class IndexViewData : NeptuneViewData
    {
        public LandUseBlockGridSpec GridSpec { get; }
        public string GridName { get; }
        public string GridDataUrl { get; }
        public bool HasManagePermission { get; }

        public string LandUseBlockBulkUploadURL { get; } 

        public IndexViewData(Person currentPerson, Models.NeptunePage neptunePage, string landUseBlockBulkUploadURL) : base (currentPerson, neptunePage, NeptuneArea.OCStormwaterTools)
        {
            EntityName = "Land Use Block";
            PageTitle = "Index";
            GridSpec = new LandUseBlockGridSpec() { ObjectNameSingular = "Land Use Block", ObjectNamePlural = "Land Use Blocks", SaveFiltersInCookie = true };
            GridName = "landUseBlockGrid";
            GridDataUrl = SitkaRoute<LandUseBlockController>.BuildUrlFromExpression(j => j.LandUseBlockGridJsonData());
            HasManagePermission = new JurisdictionManageFeature().HasPermissionByPerson(currentPerson);
            LandUseBlockBulkUploadURL = landUseBlockBulkUploadURL;
        }
    }
}
using Neptune.Web.Areas.Trash.Controllers;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;

namespace Neptune.Web.Areas.Trash.Views.LandUseBlock
{
    public class IndexViewData : TrashModuleViewData
    {
        public LandUseBlockGridSpec GridSpec { get; }
        public string GridName { get; }
        public string GridDataUrl { get; }
        public bool HasManagePermission { get; }

        public string LandUseBlockBulkUploadUrl { get; } 

        public IndexViewData(Person currentPerson, Models.NeptunePage neptunePage, string landUseBlockBulkUploadUrl) : base (currentPerson, neptunePage)
        {
            EntityName = "Land Use Block";
            PageTitle = "Index";
            GridSpec = new LandUseBlockGridSpec() { ObjectNameSingular = "Land Use Block", ObjectNamePlural = "Land Use Blocks", SaveFiltersInCookie = true };
            GridName = "landUseBlockGrid";
            GridDataUrl = SitkaRoute<LandUseBlockController>.BuildUrlFromExpression(j => j.LandUseBlockGridJsonData());
            HasManagePermission = new JurisdictionManageFeature().HasPermissionByPerson(currentPerson);
            LandUseBlockBulkUploadUrl = landUseBlockBulkUploadUrl;
        }
    }
}
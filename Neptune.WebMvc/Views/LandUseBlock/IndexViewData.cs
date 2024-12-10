using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Security;

namespace Neptune.WebMvc.Views.LandUseBlock
{
    public class IndexViewData : TrashModuleViewData
    {
        public LandUseBlockGridSpec GridSpec { get; }
        public string GridName { get; }
        public string GridDataUrl { get; }
        public bool HasManagePermission { get; }

        public string LandUseBlockBulkUploadUrl { get; } 
        public string LandUseBlockDownloadUrl { get; } 

        public IndexViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, WebConfiguration webConfiguration, EFModels.Entities.NeptunePage neptunePage, string landUseBlockBulkUploadUrl, string landUseBlockDownloadUrl) : base (httpContext, linkGenerator, currentPerson, webConfiguration, neptunePage)
        {
            EntityName = "Land Use Block";
            PageTitle = "Index";
            GridSpec = new LandUseBlockGridSpec(linkGenerator) { ObjectNameSingular = "Land Use Block", ObjectNamePlural = "Land Use Blocks", SaveFiltersInCookie = true };
            GridName = "landUseBlockGrid";
            GridDataUrl = SitkaRoute<LandUseBlockController>.BuildUrlFromExpression(linkGenerator, x => x.LandUseBlockGridJsonData());
            HasManagePermission = new JurisdictionManageFeature().HasPermissionByPerson(currentPerson);
            LandUseBlockBulkUploadUrl = landUseBlockBulkUploadUrl;
            LandUseBlockDownloadUrl = landUseBlockDownloadUrl;
        }
    }
}
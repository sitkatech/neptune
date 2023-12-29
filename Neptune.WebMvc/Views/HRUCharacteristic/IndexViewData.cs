using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Security;

namespace Neptune.WebMvc.Views.HRUCharacteristic
{
    public class IndexViewData : NeptuneViewData
    {
        public HRUCharacteristicGridSpec GridSpec { get; }
        public string GridName { get; }
        public string GridDataUrl { get; }
        public bool HasAdminPermissions { get; }
        public string RefreshUrl { get; }

        public IndexViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson,
            EFModels.Entities.NeptunePage neptunePage) : base(httpContext, linkGenerator, currentPerson, neptunePage, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            RefreshUrl =
                SitkaRoute<HRUCharacteristicController>.BuildUrlFromExpression(linkGenerator, x => x.RefreshHRUCharacteristics());
            HasAdminPermissions = new NeptuneAdminFeature().HasPermissionByPerson(currentPerson);
            EntityName = "HRU Characteristics";
            PageTitle = "Index";
            GridSpec = new HRUCharacteristicGridSpec(linkGenerator) { ObjectNameSingular = "HRU Characteristic", ObjectNamePlural = "HRU Characteristics", SaveFiltersInCookie = true };
            GridName = "HRUCharacteristics";
            GridDataUrl = SitkaRoute<HRUCharacteristicController>.BuildUrlFromExpression(linkGenerator, x => x.HRUCharacteristicGridJsonData());
        }
    }
}
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;

namespace Neptune.WebMvc.Views.Help
{
    public class BulkUploadRequestViewData : NeptuneViewData
    {
        public BulkUploadRequestViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson,
            EFModels.Entities.NeptunePage neptunePage) : base(httpContext, linkGenerator, currentPerson, neptunePage, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            PageTitle = "Bulk Upload Request";
            EntityName = "Stormwater Tools";
        }
    }
}
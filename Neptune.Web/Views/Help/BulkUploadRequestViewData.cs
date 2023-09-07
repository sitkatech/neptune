using Neptune.EFModels.Entities;

namespace Neptune.Web.Views.Help
{
    public class BulkUploadRequestViewData : NeptuneViewData
    {
        public BulkUploadRequestViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson,
            NeptunePage neptunePage) : base(httpContext, linkGenerator, currentPerson, neptunePage, NeptuneArea.OCStormwaterTools)
        {
            PageTitle = "Bulk Upload Request";
            EntityName = "Stormwater Tools";
        }
    }
}
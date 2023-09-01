using Neptune.EFModels.Entities;

namespace Neptune.Web.Views.Help
{
    public class BulkUploadRequestViewData : NeptuneViewData
    {
        public BulkUploadRequestViewData(Person currentPerson, EFModels.Entities.NeptunePage neptunePage, LinkGenerator linkGenerator, HttpContext httpContext) : base(currentPerson, neptunePage, NeptuneArea.OCStormwaterTools, linkGenerator, httpContext)
        {
            PageTitle = "Bulk Upload Request";
            EntityName = "Stormwater Tools";
        }
    }
}
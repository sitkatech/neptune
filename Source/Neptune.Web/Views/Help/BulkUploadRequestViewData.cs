using Neptune.Web.Models;
using Neptune.Web.Views;

namespace Neptune.Web.Views.Help
{
    public class BulkUploadRequestViewData : NeptuneViewData
    {
        public BulkUploadRequestViewData(Person currentPerson, Models.NeptunePage neptunePage) : base(currentPerson, neptunePage, NeptuneArea.OCStormwaterTools)
        {
            PageTitle = "Bulk Upload Request";
            EntityName = "Stormwater Tools";
        }
    }
}
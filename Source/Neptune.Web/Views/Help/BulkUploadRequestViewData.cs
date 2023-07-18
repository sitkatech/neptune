using Neptune.Web.Models;

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
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Security;
using Neptune.WebMvc.Views.Assessment;
using Neptune.WebMvc.Views.FieldVisit;
using Neptune.WebMvc.Views.MaintenanceRecord;

namespace Neptune.WebMvc.Views.DataHub
{
    public class IndexViewData : NeptuneViewData
    {

        public IndexViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson)
            : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            PageTitle = "Data Hub";
            EntityName = "Field Records";
        }
    }
}
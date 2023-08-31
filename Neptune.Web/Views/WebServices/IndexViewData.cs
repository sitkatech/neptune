using Neptune.Web.Models;
using Neptune.EFModels.Entities;

namespace Neptune.Web.Views.WebServices
{
    public class IndexViewData : NeptuneViewData
    {
        public readonly WebServiceToken WebServiceAccessToken;
        public readonly List<WebServiceDocumentation> ServiceDocumentationList;

        public IndexViewData(Person currentPerson, WebServiceToken webServiceAccessToken, List<WebServiceDocumentation> serviceDocumentationList, LinkGenerator linkGenerator, HttpContext httpContext) : base(currentPerson, NeptuneArea.OCStormwaterTools, linkGenerator, httpContext)
        {
            WebServiceAccessToken = webServiceAccessToken;
            ServiceDocumentationList = serviceDocumentationList;
            EntityName = "Web Services";
            PageTitle = "Web Services";
        }
    }
}
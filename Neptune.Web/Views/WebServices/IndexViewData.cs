using Neptune.Web.Models;
using Neptune.EFModels.Entities;

namespace Neptune.Web.Views.WebServices
{
    public class IndexViewData : NeptuneViewData
    {
        public readonly WebServiceToken WebServiceAccessToken;
        public readonly List<WebServiceDocumentation> ServiceDocumentationList;

        public IndexViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson,
            WebServiceToken webServiceAccessToken, List<WebServiceDocumentation> serviceDocumentationList) : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools)
        {
            WebServiceAccessToken = webServiceAccessToken;
            ServiceDocumentationList = serviceDocumentationList;
            EntityName = "Web Services";
            PageTitle = "Web Services";
        }
    }
}
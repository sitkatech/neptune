using Neptune.Web.Models;
using System.Collections.Generic;

namespace Neptune.Web.Views.WebServices
{
    public class IndexViewData : NeptuneViewData
    {
        public readonly WebServiceToken WebServiceAccessToken;
        public readonly List<WebServiceDocumentation> ServiceDocumentationList;

        public IndexViewData(Person currentPerson, WebServiceToken webServiceAccessToken, List<WebServiceDocumentation> serviceDocumentationList) : base(currentPerson, NeptuneArea.OCStormwaterTools)
        {
            WebServiceAccessToken = webServiceAccessToken;
            ServiceDocumentationList = serviceDocumentationList;
            EntityName = "Web Services";
            PageTitle = "Web Services";
        }
    }
}
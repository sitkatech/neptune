using Neptune.Web.Models;
using Neptune.Web.Security;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Spreadsheet;

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
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;
using System;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Neptune.Web.Views.WebServices
{
    public class IndexViewData : NeptuneViewData
    {
        public readonly Guid? WebServiceAccessToken;
        public readonly string WebServicesListUrl;
        public readonly string GetWebServiceAccessTokenUrl;

        public IndexViewData(Person currentPerson, Guid? webServiceAccessToken, string webServicesListUrl, string getWebServiceAccessTokenUrl) : base(currentPerson, NeptuneArea.OCStormwaterTools)
        {
            WebServiceAccessToken = webServiceAccessToken;
            WebServicesListUrl = webServicesListUrl;
            GetWebServiceAccessTokenUrl = getWebServiceAccessTokenUrl;
            EntityName = "Web Services";
            PageTitle = "Web Services";
        }
    }
}
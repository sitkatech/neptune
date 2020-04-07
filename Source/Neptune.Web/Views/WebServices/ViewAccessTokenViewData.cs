using System;
using Neptune.Web.Views;

namespace Neptune.Web.Views.WebServices
{
    public class ViewAccessTokenViewData : NeptuneUserControlViewData
    {
        public readonly Guid WebServiceAccessToken;
        public readonly string WebServicesListUrl;

        public ViewAccessTokenViewData(Guid webServiceAccessToken, string webServicesListUrl)
        {
            WebServiceAccessToken = webServiceAccessToken;
            WebServicesListUrl = webServicesListUrl;
        }
    }
}
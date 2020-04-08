using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DocumentFormat.OpenXml.Spreadsheet;
using LtInfo.Common.DesignByContract;

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

    public class WebServiceDocumentation
    {
        public string Name;
        private readonly string Url;
        private readonly List<string> Parameters;
        public string Documentation;

        public string GetReplacedUrl(WebServiceToken userToken)
        {
            if (String.IsNullOrEmpty(Url))
            {
                return String.Empty;
            }

            return Url.Replace(WebServiceToken.WebServiceTokenGuidForParameterizedReplacement.ToString(),
                userToken.ToString());
        }

        public string GetParameters(WebServiceToken userToken)
        {
            if (!Parameters.Any())
            {
                return String.Empty;
            }

            return string.Join(", ", Parameters);
        }

        public WebServiceDocumentation(MethodInfo methodInfo)
        {
            var attribs = methodInfo.GetCustomAttributes(typeof(WebServiceDocumentationAttribute), false);
            Check.Require(attribs.Length == 1, "Expected 1 and only 1 WebServiceDocumentation attribute on found Web Methods.");

            var attrib = (WebServiceDocumentationAttribute)attribs[0];

            Documentation = attrib.Documentation;
            Name = methodInfo.Name;
            var webServiceRouteMap = Service.WebServices.GetWebServiceRouteMap();

            var routeMap = webServiceRouteMap.FirstOrDefault(x => x.MethodName == methodInfo.Name);
            Url = routeMap.Route.BuildUrlFromExpression();
            Parameters = routeMap.Parameters;
        }
    }
}
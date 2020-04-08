using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using ClosedXML.Excel;
using LtInfo.Common.DesignByContract;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using WebGrease.Extensions;

namespace Neptune.Web.Views.WebServices
{
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

            return String.Join(", ", Parameters);
        }

        public WebServiceDocumentation(MethodInfo methodInfo)
        {
            var attribs = methodInfo.GetCustomAttributes(typeof(WebServiceDocumentationAttribute), false);
            Check.Require(attribs.Length == 1, "Expected 1 and only 1 WebServiceDocumentation attribute on found Web Methods.");

            var attrib = (WebServiceDocumentationAttribute)attribs[0];
            
            Documentation = attrib.Documentation;
            Name = methodInfo.Name;

            var parameters = methodInfo.GetParameters();
            if (parameters.Length > 0)
            {
                Parameters = new List<string>();
                parameters.ForEach(x =>
                {
                    var paramAttribs = x.GetCustomAttributes(typeof(ParameterDescriptionAttribute), false);
                    if (paramAttribs.Length > 0)
                    {
                        var paramAttrib = (ParameterDescriptionAttribute) paramAttribs[0];
                        Parameters.Add(paramAttrib.Description);
                    }
                    else
                    {
                        Parameters.Add(x.Name);
                    }
                });
            }

            

            var webServiceRouteMap = GetWebServiceRouteMap();

            var routeMap = webServiceRouteMap.FirstOrDefault(x => x.MethodName == methodInfo.Name);
            Url = routeMap.Route;
            
        }

        public class SampleRouteEntry
        {
            public readonly string MethodName;
            public readonly string Route;
            public SampleRouteEntry(string methodName, string route)
            {
                MethodName = methodName;
                Route = route;
            }
        }

        public static List<SampleRouteEntry> GetWebServiceRouteMap()
        {
            Check.EnsureNotNull(WebServiceToken.WebServiceTokenForParameterizedReplacements);

            var webServiceRouteMap = new List<SampleRouteEntry>
            {
                new SampleRouteEntry(MethodNameFromExpression(c =>
                        c.TreatmentBMPAttributeSummary(WebServiceToken.WebServiceTokenForParameterizedReplacements)),
                    new SitkaRoute<PowerBIController>(c =>
                        c.TreatmentBMPAttributeSummary(WebServiceToken.WebServiceTokenForParameterizedReplacements)).BuildUrlFromExpression()
                ),
                new SampleRouteEntry(MethodNameFromExpression(c =>
                        c.WaterQualityManagementPlanAttributeSummary(WebServiceToken.WebServiceTokenForParameterizedReplacements)),
                    new SitkaRoute<PowerBIController>(c =>
                        c.WaterQualityManagementPlanAttributeSummary(WebServiceToken.WebServiceTokenForParameterizedReplacements)).BuildUrlFromExpression()
                ),
                new SampleRouteEntry(MethodNameFromExpression(c =>
                        c.LandUseStatistics(WebServiceToken.WebServiceTokenForParameterizedReplacements)),
                    new SitkaRoute<PowerBIController>(c =>
                        c.LandUseStatistics(WebServiceToken.WebServiceTokenForParameterizedReplacements)).BuildUrlFromExpression()
                ),
                new SampleRouteEntry(MethodNameFromExpression(c =>
                        c.TreatmentBMPParameterizationSummary(WebServiceToken.WebServiceTokenForParameterizedReplacements)),
                    new SitkaRoute<PowerBIController>(c =>
                        c.TreatmentBMPParameterizationSummary(WebServiceToken.WebServiceTokenForParameterizedReplacements)).BuildUrlFromExpression()
                ),
                new SampleRouteEntry(MethodNameFromExpression(c =>
                        c.CentralizedBMPLoadGeneratingUnitMapping(WebServiceToken.WebServiceTokenForParameterizedReplacements)),
                    new SitkaRoute<PowerBIController>(c =>
                        c.CentralizedBMPLoadGeneratingUnitMapping(WebServiceToken.WebServiceTokenForParameterizedReplacements)).BuildUrlFromExpression()
                )
            };
            return webServiceRouteMap;
        }

        public static string MethodNameFromExpression(Expression<Action<PowerBIController>> expression)
        {
            return ((MethodCallExpression)expression.Body).Method.Name;
        }
    }
}
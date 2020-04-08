using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
        private readonly string[] Parameters;
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
            var attribs = methodInfo.GetCustomAttributes(typeof(WebServiceNameAndParametersAttribute), false);
            Check.Require(attribs.Length == 1, "Expected 1 and only 1 WebServiceDocumentation attribute on found Web Methods.");

            var attrib = (WebServiceNameAndParametersAttribute)attribs[0];

            Documentation = attrib.Name;
            Name = methodInfo.Name;
            Parameters = attrib.Parameters;

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
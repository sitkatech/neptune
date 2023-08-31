using System.Linq.Expressions;
using System.Reflection;
using ApprovalUtilities.Utilities;
using Neptune.Common.DesignByContract;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;

namespace Neptune.Web.Views.WebServices
{
    public class WebServiceDocumentation
    {
        public string Name;
        private readonly string Url;
        private readonly List<string> Parameters;
        public string Description;
        public string DescriptionHeader;

        public string GetReplacedUrl(WebServiceToken userToken)
        {
            if (string.IsNullOrEmpty(Url))
            {
                return string.Empty;
            }

            return Url.Replace(WebServiceToken.WebServiceTokenGuidForParameterizedReplacement.ToString(),
                userToken.ToString());
        }

        public string GetParameters(WebServiceToken userToken)
        {
            if (!Parameters.Any())
            {
                return string.Empty;
            }

            return string.Join(", ", Parameters);
        }

        public WebServiceDocumentation(MethodInfo methodInfo, NeptuneDbContext dbContext, LinkGenerator linkGenerator)
        {
            var attribs = methodInfo.GetCustomAttributes(typeof(WebServiceNameAndDescriptionAttribute), false);
            Check.Require(attribs.Length == 1, "Expected 1 and only 1 WebServiceDocumentation attribute on found Web Methods.");

            var attrib = (WebServiceNameAndDescriptionAttribute)attribs[0];
            
            Description = attrib.Description;
            DescriptionHeader = attrib.Name;
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

            var webServiceRouteMap = GetWebServiceRouteMap(dbContext, linkGenerator);

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

        public static List<SampleRouteEntry> GetWebServiceRouteMap(NeptuneDbContext dbContext, LinkGenerator linkGenerator)
        {
            var webServiceToken = new WebServiceToken(dbContext);
            Check.EnsureNotNull(webServiceToken.WebServiceTokenForParameterizedReplacements);

            var webServiceRouteMap = new List<SampleRouteEntry>
            {
                new (MethodNameFromExpression(c =>
                        c.TreatmentBMPAttributeSummary(webServiceToken.WebServiceTokenForParameterizedReplacements)),
                    new SitkaRoute<PowerBIController>(c =>
                        c.TreatmentBMPAttributeSummary(webServiceToken.WebServiceTokenForParameterizedReplacements), SitkaRouteSecurity.SSL, linkGenerator).BuildUrlFromExpression()
                ),
                new (MethodNameFromExpression(c =>
                        c.WaterQualityManagementPlanAttributeSummary(webServiceToken.WebServiceTokenForParameterizedReplacements)),
                    new SitkaRoute<PowerBIController>(c =>
                        c.WaterQualityManagementPlanAttributeSummary(webServiceToken.WebServiceTokenForParameterizedReplacements), SitkaRouteSecurity.SSL, linkGenerator).BuildUrlFromExpression()
                ),
                new (MethodNameFromExpression(c =>
                        c.LandUseStatistics(webServiceToken.WebServiceTokenForParameterizedReplacements)),
                    new SitkaRoute<PowerBIController>(c =>
                        c.LandUseStatistics(webServiceToken.WebServiceTokenForParameterizedReplacements), SitkaRouteSecurity.SSL, linkGenerator).BuildUrlFromExpression()
                ),
                new (MethodNameFromExpression(c =>
                        c.TreatmentBMPParameterizationSummary(webServiceToken.WebServiceTokenForParameterizedReplacements)),
                    new SitkaRoute<PowerBIController>(c =>
                        c.TreatmentBMPParameterizationSummary(webServiceToken.WebServiceTokenForParameterizedReplacements), SitkaRouteSecurity.SSL, linkGenerator).BuildUrlFromExpression()
                ),
                new (MethodNameFromExpression(c =>
                        c.CentralizedBMPLoadGeneratingUnitMapping(webServiceToken.WebServiceTokenForParameterizedReplacements)),
                    new SitkaRoute<PowerBIController>(c =>
                        c.CentralizedBMPLoadGeneratingUnitMapping(webServiceToken.WebServiceTokenForParameterizedReplacements), SitkaRouteSecurity.SSL, linkGenerator).BuildUrlFromExpression()
                ),
                new (MethodNameFromExpression(c =>
                        c.ModelResults(webServiceToken.WebServiceTokenForParameterizedReplacements)),
                    new SitkaRoute<PowerBIController>(c =>
                        c.ModelResults(webServiceToken.WebServiceTokenForParameterizedReplacements), SitkaRouteSecurity.SSL, linkGenerator).BuildUrlFromExpression()
                ),
                new (MethodNameFromExpression(c =>
                        c.BaselineModelResults(webServiceToken.WebServiceTokenForParameterizedReplacements)),
                    new SitkaRoute<PowerBIController>(c =>
                        c.BaselineModelResults(webServiceToken.WebServiceTokenForParameterizedReplacements), SitkaRouteSecurity.SSL, linkGenerator).BuildUrlFromExpression()
                ),
                new (MethodNameFromExpression(c =>
                        c.WaterQualityManagementPlanOAndMVerifications(webServiceToken.WebServiceTokenForParameterizedReplacements)),
                    new SitkaRoute<PowerBIController>(c =>
                        c.WaterQualityManagementPlanOAndMVerifications(webServiceToken.WebServiceTokenForParameterizedReplacements), SitkaRouteSecurity.SSL, linkGenerator).BuildUrlFromExpression()
                ),
            };
            return webServiceRouteMap;
        }

        public static string MethodNameFromExpression(Expression<Action<PowerBIController>> expression)
        {
            return ((MethodCallExpression)expression.Body).Method.Name;
        }
    }
}
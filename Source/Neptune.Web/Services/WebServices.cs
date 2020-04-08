/*-----------------------------------------------------------------------
<copyright file="WebServices.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
<author>Sitka Technology Group</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Neptune.Web.Controllers;
using LtInfo.Common;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Mvc;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Service
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class WebServices : IWebServices
    {
        private static T CommandWrapper<T>(string allegedWebServiceTokenString, Func<T> func)
        {
            return CommandWrapper(allegedWebServiceTokenString, x => func.Invoke());
        }

        private static T CommandWrapper<T>(string allegedWebServiceTokenString, Func<WebServiceToken, T> func)
        {
            var webServiceToken = new WebServiceToken(allegedWebServiceTokenString);

            // To prevent code from doing Notifications and any other stuff for production bits only, temporarily mark this thread here to be in Unit Test Mode
            if (webServiceToken.IsWebServiceTokenForUnitTests)
            {
                BrowserAutomationCookie.SetIsRunningUnderWebBrowserAutomation();
            }

            return func.Invoke(webServiceToken);
        }

        public void TreatmentBMPAttributeSummary(string webServiceToken)
        { }

        public void WaterQualityManagementPlanAttributeSummary(string webServiceToken)
        { }

        public void LandUseStatistics(string webServiceToken)
        { }

        public void TreatmentBMPParameterizationSummary(string webServiceToken)
        { }

        public void CentralizedBMPLoadGeneratingUnitMapping(string webServiceToken)
        { }

        public void GetHRUCharacteristicsForPowerBI(string webServiceToken)
        { }

        public class SampleRouteEntry
        {
            public readonly string MethodName;
            public readonly SitkaRoute<PowerBIController> Route;
            public readonly List<string> Parameters;
            public SampleRouteEntry(string methodName, SitkaRoute<PowerBIController> route, List<string> parameters)
            {
                MethodName = methodName;
                Route = route;
                Parameters = parameters;
            }
        }

        public static List<SampleRouteEntry> GetWebServiceRouteMap()
        {

            Check.EnsureNotNull(WebServiceToken.WebServiceTokenGuidForParameterizedReplacement);
            Check.EnsureNotNull(WebServiceToken.WebServiceTokenForParameterizedReplacements);

            var webServiceRouteMap = new List<SampleRouteEntry>
            {
                new SampleRouteEntry(
                    MethodNameFromExpression(c =>
                        c.TreatmentBMPAttributeSummary(WebServiceToken.WebServiceTokenGuidForParameterizedReplacement.ToString())),
                    new SitkaRoute<PowerBIController>(c =>
                        c.TreatmentBMPAttributeSummary(WebServiceToken.WebServiceTokenForParameterizedReplacements))
                    , new List<string> {"Authorization Token"}
                ),
                new SampleRouteEntry(
                    MethodNameFromExpression(c =>
                        c.WaterQualityManagementPlanAttributeSummary(WebServiceToken.WebServiceTokenGuidForParameterizedReplacement.ToString())),
                    new SitkaRoute<PowerBIController>(c =>
                        c.WaterQualityManagementPlanAttributeSummary(WebServiceToken.WebServiceTokenForParameterizedReplacements))
                    , new List<string> {"Authorization Token"}
                ),
                new SampleRouteEntry(
                    MethodNameFromExpression(c =>
                        c.LandUseStatistics(WebServiceToken.WebServiceTokenGuidForParameterizedReplacement.ToString())),
                    new SitkaRoute<PowerBIController>(c =>
                        c.LandUseStatistics(WebServiceToken.WebServiceTokenForParameterizedReplacements))
                    , new List<string> {"Authorization Token"}
                ),
                new SampleRouteEntry(
                    MethodNameFromExpression(c =>
                        c.TreatmentBMPParameterizationSummary(WebServiceToken.WebServiceTokenGuidForParameterizedReplacement.ToString())),
                    new SitkaRoute<PowerBIController>(c =>
                        c.TreatmentBMPParameterizationSummary(WebServiceToken.WebServiceTokenForParameterizedReplacements))
                    , new List<string> {"Authorization Token"}
                ),
                new SampleRouteEntry(
                    MethodNameFromExpression(c =>
                        c.CentralizedBMPLoadGeneratingUnitMapping(WebServiceToken.WebServiceTokenGuidForParameterizedReplacement.ToString())),
                    new SitkaRoute<PowerBIController>(c =>
                        c.CentralizedBMPLoadGeneratingUnitMapping(WebServiceToken.WebServiceTokenForParameterizedReplacements))
                    , new List<string> {"Authorization Token"}
                ),
                new SampleRouteEntry(
                    MethodNameFromExpression(c =>
                        c.GetHRUCharacteristicsForPowerBI(WebServiceToken.WebServiceTokenGuidForParameterizedReplacement.ToString())),
                    new SitkaRoute<PowerBIController>(c =>
                        c.GetHRUCharacteristicsForPowerBI(WebServiceToken.WebServiceTokenForParameterizedReplacements))
                    , new List<string> {"Authorization Token"}
                )
            };
            return webServiceRouteMap;
        }

        public static string MethodNameFromExpression(Expression<Action<IWebServices>> expression)
        {
            return ((MethodCallExpression)expression.Body).Method.Name;
        }

        
    }
}
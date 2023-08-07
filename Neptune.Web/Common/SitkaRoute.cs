/*-----------------------------------------------------------------------
<copyright file="SitkaRoute.cs" company="Sitka Technology Group">
Copyright (c) Sitka Technology Group. All rights reserved.
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

using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Neptune.Web.Common.DesignByContract;

namespace Neptune.Web.Common
{
    public enum SitkaRouteSecurity
    {
        Unsecured,
        SSL
    }

    public class SitkaRoute<T> where T : Controller
    {
        private readonly LinkGenerator _linkGenerator;
        public string ControllerName { get; private set; }
        public string ActionName { get; private set; }
        public MethodCallExpression Body { get; private set; }

        public Expression<Action<T>> RouteExpression { get; private set; }

        public SitkaRouteSecurity RouteSecurity { get; set; }


        public SitkaRoute(Expression<Action<T>> routeExpression, SitkaRouteSecurity routeSecurity, HttpContext httpContext) : this(routeExpression, httpContext)
        {
            RouteSecurity = routeSecurity;
        }

        public SitkaRoute(Expression<Action<T>> routeExpression, HttpContext httpContext)
        {
            _linkGenerator = httpContext.RequestServices.GetRequiredService<LinkGenerator>();

            RouteExpression = routeExpression;
            ControllerName = SitkaController.ControllerTypeToControllerName(typeof(T));
            Body = GetRouteExpressionBody(routeExpression);

            var actionName = Body.Method.Name;
            var attributes = Body.Method.GetCustomAttributes(typeof(ActionNameAttribute), false);
            if (attributes.Length > 0)
            {
                var actionNameAttr = (ActionNameAttribute)attributes[0];
                actionName = actionNameAttr.Name;
            }

            ActionName = actionName;
        }

        public string BuildUrlFromExpression()
        {
            var actionParameters = Body.Method.GetParameters();
            var objectBody = new Dictionary<string, dynamic>();
            for (var i = 0; i < actionParameters.Length; i++)
            {
                var parameterValue = (Body.Arguments[i] as ConstantExpression)?.Value;
                objectBody.Add(actionParameters[i].Name, parameterValue);
            }
            var relativePath = _linkGenerator.GetPathByAction(ActionName, ControllerName, objectBody);
            return relativePath;
        }

        public static string BuildLinkFromUrl(string url, string linkText)
        {
            return $"<a href=\"{url}\">{linkText}</a>";
        }

        public static string BuildLinkFromUrl(string url, string linkText, Dictionary<string, string> attributeDict)
        {
            return $"<a href=\"{url}\" {BuildAttributeString(attributeDict)}>{linkText}</a>";
        }

        public static string BuildLinkFromUrl(string url, string linkText, string titleText)
        {
            return $"<a href=\"{url}\" title=\"{titleText}\">{linkText}</a>";
        }

        public static string BuildExternalLinkFromUrl(string baseExternalUrl, string linkText)
        {
            return $"<a href=\"http://{baseExternalUrl}\">{linkText}</a>";
        }

        public static MethodCallExpression GetRouteExpressionBody(Expression<Action<T>> routeExpression)
        {
            var body = routeExpression.Body as MethodCallExpression;

            Check.RequireNotNull(body, new ArgumentException("Expression must be a method call."));
            // ReSharper disable PossibleNullReferenceException
            Check.Require(body.Object == routeExpression.Parameters[0], new ArgumentException("Method call must target lambda argument."));
            // ReSharper restore PossibleNullReferenceException
            return body;
        }

        #region Private Helper Methods

        private static string BuildAttributeString(Dictionary<string, string> attributeDict)
        {
            return attributeDict.Aggregate(" ", (current, attribute) =>
                $"{current} {attribute.Key}=\"{attribute.Value}\"");
        }

        #endregion
    }
}

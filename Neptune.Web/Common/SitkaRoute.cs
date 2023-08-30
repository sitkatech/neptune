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
using Neptune.Common.DesignByContract;

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
        public string ControllerName { get; }
        public string ActionName { get; }
        public MethodCallExpression Body { get; }

        public Expression<Action<T>> RouteExpression { get; }

        public SitkaRouteSecurity RouteSecurity { get; set; }

        public SitkaRoute(Expression<Action<T>> routeExpression, SitkaRouteSecurity routeSecurity, LinkGenerator linkGenerator) : this(linkGenerator, routeExpression)
        {
            RouteSecurity = routeSecurity;
        }

        //public SitkaRoute(Expression<Action<T>> routeExpression, SitkaRouteSecurity routeSecurity, HttpContext httpContext) : this(routeExpression, httpContext)
        //{
        //    RouteSecurity = routeSecurity;
        //}

        //public SitkaRoute(Expression<Action<T>> routeExpression, HttpContext httpContext)
        //{
        //    _linkGenerator = httpContext.RequestServices.GetRequiredService<LinkGenerator>();

        //    RouteExpression = routeExpression;
        //    ControllerName = SitkaController.ControllerTypeToControllerName(typeof(T));
        //    Body = GetRouteExpressionBody(routeExpression);

        //    var actionName = Body.Method.Name;
        //    var attributes = Body.Method.GetCustomAttributes(typeof(ActionNameAttribute), false);
        //    if (attributes.Length > 0)
        //    {
        //        var actionNameAttr = (ActionNameAttribute)attributes[0];
        //        actionName = actionNameAttr.Name;
        //    }

        //    ActionName = actionName;
        //}

        public SitkaRoute(LinkGenerator linkGenerator, Expression<Action<T>> routeExpression)
        {
            _linkGenerator = linkGenerator;

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
            return BuildUrlFromExpressionImpl(_linkGenerator, Body, ActionName, ControllerName);
        }

        public static string BuildUrlFromExpression(LinkGenerator linkGenerator, Expression<Action<T>> routeExpression)
        {
            var controllerName = SitkaController.ControllerTypeToControllerName(typeof(T));
            var body = GetRouteExpressionBody(routeExpression);

            var actionName = body.Method.Name;
            var attributes = body.Method.GetCustomAttributes(typeof(ActionNameAttribute), false);
            if (attributes.Length > 0)
            {
                var actionNameAttr = (ActionNameAttribute)attributes[0];
                actionName = actionNameAttr.Name;
            }

            return BuildUrlFromExpressionImpl(linkGenerator, body, actionName, controllerName);
        }

        public static string BuildAbsoluteUrlHttpsFromExpression(LinkGenerator linkGenerator,
            string canonicalHostName,
            Expression<Action<T>> routeExpression)
        {
            return BuildAbsoluteUrlFromExpressionImpl(linkGenerator, canonicalHostName, routeExpression, "https");
        }

        public static string BuildAbsoluteUrlFromExpression(LinkGenerator linkGenerator,
            string canonicalHostName, Expression<Action<T>> routeExpression)
        {
            return BuildAbsoluteUrlFromExpressionImpl(linkGenerator, canonicalHostName, routeExpression, "http");
        }


        private static string BuildAbsoluteUrlFromExpressionImpl(LinkGenerator linkGenerator, string canonicalHostName,
            Expression<Action<T>> routeExpression, string protocol)
        {
            var relativeUrl = BuildUrlFromExpression(linkGenerator, routeExpression);
            return BuildAbsoluteUrlFromRelativeUrl(canonicalHostName, protocol, relativeUrl);
        }

        public static string BuildAbsoluteUrlFromRelativeUrl(string canonicalHostName, string relativeUrl)
        {
            return BuildAbsoluteUrlFromRelativeUrl(canonicalHostName, "http", relativeUrl);
        }

        public static string BuildAbsoluteUrlFromRelativeUrl(string canonicalHostName, string protocol, string relativeUrl)
        {
            return $"{protocol}://{canonicalHostName}{relativeUrl}";
        }


        private static string BuildUrlFromExpressionImpl(LinkGenerator linkGenerator, MethodCallExpression body,
            string actionName, string controllerName)
        {
            var actionParameters = body.Method.GetParameters();
            var objectBody = new Dictionary<string, dynamic>();
            for (var i = 0; i < actionParameters.Length; i++)
            {
                var parameterValue = GetArgumentValue(body.Arguments[i]);
                objectBody.Add(actionParameters[i].Name, parameterValue);
            }

            var relativePath = linkGenerator.GetPathByAction(actionName, controllerName, objectBody);
            return relativePath;
        }

        private static object GetArgumentValue(Expression element)
        {
            if (element is ConstantExpression expression)
            {
                return expression.Value;
            }

            return Expression.Lambda(Expression.Convert(element, element.Type)).Compile().DynamicInvoke();
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
